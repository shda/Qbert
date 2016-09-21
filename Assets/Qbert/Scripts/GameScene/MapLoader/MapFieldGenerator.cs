using System.Collections.Generic;
using System.Linq;
using Scripts.GameScene.Characters;
using Scripts.GameScene.GameAssets;
using UnityEngine;

namespace Scripts.GameScene.MapLoader
{
    public class MapFieldGenerator : MonoBehaviour 
    {
        public float offsetX = 1.0f;
        public float offsetY = 1.0f;

        public Transform root;

        public bool isDebug = false;

        public int startLine = 0;
        public int startPos = 0;

        public MapAsset mapAsset;

        [HideInInspector]
        public List<Cube> map;
        public List<PointOutsideField> fieldPoints;

        public PointOutsideField GetPointOutsideFieldToPosition(PositionCube position)
        {
            foreach (var fieldPoint in fieldPoints)
            {
                if (fieldPoint.currentPoint == position)
                    return fieldPoint;
            }

            return null;
        }

        public List<PointOutsideField> GetPointOutsideFieldStartPointToType(GameplayObject.Type type)
        {
            List<PointOutsideField> ll = new List<PointOutsideField>();

            foreach (var fieldPoint in fieldPoints)
            {
                if(fieldPoint.cubeInMap.listTypeObjectsStartPoint.Contains(type))
                    ll.Add(fieldPoint);
            }

            return ll;
        }

        public List<PointOutsideField> GetPointsOutsideFieldEndPointToType(GameplayObject.Type type)
        {
            List<PointOutsideField> ll = new List<PointOutsideField>();

            foreach (var fieldPoint in fieldPoints)
            {
                if (fieldPoint.cubeInMap.listTypeObjectsEndPoint.Contains(type))
                    ll.Add(fieldPoint);
            }

            return ll;
        }

        public void CreateMap()
        {
            if (root)
            {
                DestroyOldMap();

                var mapCreate = mapAsset.map;

                map = new List<Cube>();
                fieldPoints = new List<PointOutsideField>();

                for (int y = 0; y < mapCreate.hight; y++)
                {
                    GameObject lineRoot = new GameObject("Line" + y);
                    lineRoot.transform.SetParent(root);
                    lineRoot.transform.localRotation = Quaternion.identity;

                    for (int x = 0; x < mapCreate.width; x++)
                    {
                        var cubeInMap = mapCreate.GetCubeInMap(x, y);

                        if (cubeInMap != null)
                        {
                            float offsetSet = offsetX*x - (offsetX * (y%2) * 0.5f);

                            var point = new Vector3(offsetSet, -offsetY*y, offsetSet);

                            if (cubeInMap.isEnable)
                            {
                                Transform createPattern = CreatePattern(cubeInMap.cubePattern,
                                    point, lineRoot.transform,
                                    cubeInMap.y, cubeInMap.x);

                                Cube cube = createPattern.GetComponent<Cube>();
                                if (cube)
                                {
                                    cube.isDebudLabel = isDebug;
                                    cube.currentPosition = new PositionCube(cubeInMap.y, cubeInMap.x);
                                    cube.cubeInMap = cubeInMap;
                                    map.Add(cube);
                                }
                            }
                            else if ( (cubeInMap.listTypeObjectsStartPoint != null && cubeInMap.listTypeObjectsStartPoint.Count > 0) ||
                                      (cubeInMap.listTypeObjectsEndPoint != null && cubeInMap.listTypeObjectsEndPoint.Count > 0) )
                            {
                                Transform createPattern = CreateFieldPoint(point, 
                                    lineRoot.transform,
                                    cubeInMap.y, cubeInMap.x);

                                PointOutsideField fieldPoint = createPattern.gameObject.AddComponent<PointOutsideField>();
                                fieldPoint.currentPoint = new PositionCube(cubeInMap.y, cubeInMap.x);
                                fieldPoint.cubeInMap = cubeInMap;

                                fieldPoints.Add(fieldPoint);
                            }
                        }
                    }

                    float offsetLocalX = y*offsetX*0.5f;
                    lineRoot.transform.localPosition = new Vector3(offsetLocalX, 0 , -offsetLocalX);
                }
                MoveToCenterMap();
                ConnectNodesInCubes();
            }
        }

        public void MoveToCenterMap()
        {
            root.localPosition = Vector3.zero;

            Vector3 center = Vector3.zero;
            foreach (var cube in map)
            {
                center += cube.transform.position;
            }

            center = center/(map.Count);

            var localCenter = root.InverseTransformPoint(center);
            root.localPosition = root.localPosition - localCenter;
        }


        public void ConnectNodesInCubes()
        {
            foreach (var cube in map)
            {
                var currentPos = cube.currentPosition;
                cube.nodes.AddRange(GetNeighborsCubes(currentPos));
            }
        }

        public List<Cube> GetNeighborsCubes(PositionCube point)
        {
            List<Cube> list = new List<Cube>();

            PositionCube[] pos = new[]
            {
                new PositionCube( point.line - 1 , point.position - 1),
                new PositionCube( point.line - 1 , point.position),
                new PositionCube( point.line + 1 , point.position),
                new PositionCube( point.line + 1 , point.position + 1),
            };

            foreach (var positionCube in pos)
            {
                var findCube = map.FirstOrDefault(x => x.currentPosition == positionCube);
                if (findCube != null)
                {
                    list.Add(findCube);
                }
            }

            return list;
        } 

        public Cube FindCubeToPoint(PositionCube point)
        {
            return map.FirstOrDefault(x => x.currentPosition == point);
        }

        private Transform CreatePattern(Transform cubePatern, Vector3 position, Transform rootLine, int line, int pos)
        {
            Transform createPattern = Instantiate(cubePatern) as Transform;
            createPattern.SetParent(rootLine);
            createPattern.transform.localRotation = Quaternion.identity;
            createPattern.localPosition = position;
            createPattern.localScale = new Vector3(offsetX, offsetY, offsetX);
            createPattern.name = string.Format("Cube_{0}_{1}", line, pos);
            return createPattern;
        }

        private Transform CreateFieldPoint(Vector3 position, Transform rootLine, int line, int pos)
        {
            GameObject fieldPoint = new GameObject();
            fieldPoint.transform.SetParent(rootLine);
            fieldPoint.transform.localRotation = Quaternion.identity;
            fieldPoint.transform.localPosition = position;
            fieldPoint.transform.localScale = new Vector3(offsetX, offsetY, offsetX);
            fieldPoint.transform.name = string.Format("Field_{0}_{1}", line, pos);
            return fieldPoint.transform;
        }

        private void DestroyOldMap()
        {
            var list = root.Cast<Transform>().ToArray();

            foreach (var child in list)
            {
                child.gameObject.SetActive(false);
                DestroyImmediate(child.gameObject, true);
            }
        }

        void Awake()
        {
            DestroyOldMap();
            //CreateMap();
        }

        void Start()
        {

        }

        public Cube GetCubeStartByType(GameplayObject.Type type)
        {
            return map.FirstOrDefault(x => x.cubeInMap.listTypeObjectsStartPoint.Contains(type));
        }

        public Cube[] GetCubesStartByType(GameplayObject.Type type)
        {
            return map.Where(x => x.cubeInMap.listTypeObjectsStartPoint.Contains(type)).ToArray();
        }

        public Cube GetCubeEndByType(GameplayObject.Type type)
        {
            return map.FirstOrDefault(x => x.cubeInMap.listTypeObjectsEndPoint.Contains(type));
        }

        public Cube[] GetCubesEndByType(GameplayObject.Type type)
        {
            return map.Where(x => x.cubeInMap.listTypeObjectsEndPoint.Contains(type)).ToArray();
        }
    }
}
