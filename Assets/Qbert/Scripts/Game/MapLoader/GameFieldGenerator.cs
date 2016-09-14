using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameFieldGenerator : MonoBehaviour 
{
    public float offsetX = 1.0f;
    public float offsetY = 1.0f;

    public Transform root;
    public Transform pattern;

    public int levels = 7;

    public int startLine = 0;
    public int startPos = 0;

    public MapAsset mapAsset;

    [HideInInspector]
    public List<Cube> map;

    public MultiValueDictionary<int, FieldPoint> fieldPoints;

    public void CreateMap()
    {
        if (root && pattern)
        {
            DestroyOldMap();

            var mapCreate = mapAsset.map;

            map = new List<Cube>();
            fieldPoints = new MultiValueDictionary<int, FieldPoint>();

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
                        float offsetSet = offsetX * x - (offsetX * y * 0.5f ) - (offsetX * y %2 * 0.5f);

                        if (cubeInMap.isEnable)
                        {
                            Transform createPattern = CreatePattern(
                            new Vector3(offsetSet, 0, offsetSet), lineRoot.transform,
                            cubeInMap.y, cubeInMap.x);

                            Cube cube = createPattern.GetComponent<Cube>();
                            if (cube)
                            {
                                cube.cubePosition = new PositionCube(cubeInMap.y, cubeInMap.x);
                                cube.cubeInMap = cubeInMap;
                                map.Add(cube);
                            }
                        }
                        

                        
                    }
                }

                lineRoot.transform.localPosition = new Vector3(y * offsetY , -y * offsetY);

                
            }
            MoveToCenter();
            CreateTree();
        }
    }

    public void MoveToCenter()
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


    public void CreateTree()
    {
        foreach (var cube in map)
        {
            var currentPos = cube.cubePosition;

            PositionCube[] pos = new[]
            {
                new PositionCube( currentPos.line - 1 , currentPos.position - 1),
                new PositionCube( currentPos.line - 1 , currentPos.position),
                new PositionCube( currentPos.line + 1 , currentPos.position),
                new PositionCube( currentPos.line + 1 , currentPos.position + 1),
            };

            foreach (var positionCube in pos)
            {
                var findCube = map.FirstOrDefault(x => x.cubePosition == positionCube);
                if (findCube != null)
                {
                    cube.nodes.Add(findCube);
                }
            }

        }
    }

    public Cube FindCubeToPoint(PositionCube point)
    {
        return map.FirstOrDefault(x => x.cubePosition == point);
    }

    private Transform CreatePattern(Vector3 position, Transform rootLine, int line, int pos)
    {
        Transform createPattern = Instantiate(pattern) as Transform;
        createPattern.SetParent(rootLine);
        createPattern.transform.localRotation = Quaternion.identity;
        createPattern.localPosition = position;
        createPattern.localScale = new Vector3(offsetX, offsetY, offsetX);
        createPattern.name = string.Format("Cube_{0}_{1}", line, pos);
        return createPattern;
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
        CreateMap();
    }

    void Start()
    {

    }

    public Cube GetCubeById(int id)
    {
        return map.FirstOrDefault(x => x.cubeInMap.id == id);
    }

    public Cube[] GetCubesById(int id)
    {
        return map.Where(x => x.cubeInMap.id == id).ToArray();
    }
}
