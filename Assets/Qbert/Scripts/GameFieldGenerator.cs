using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[ExecuteInEditMode]
public class GameFieldGenerator : MonoBehaviour
{
    public float offsetX = 1.0f;
    public float offsetY = 1.0f;

    public int startLine = 0;
    public int startPos = 0;

    public int levels = 7;
    public Transform root;
    public Transform pattern;

    [HideInInspector]
    public List<Cube> map; 

    private float oldOffset;
    private int oldLevels = -1;

    public void CreateMap()
    {
        if (root && pattern)
        {
            DestroyOldMap();

            root.localPosition = new Vector3();

            map = new List<Cube>();

            for (int level = 0; level < levels; level++)
            {
                GameObject lineRoot = new GameObject("Line" + level);
                lineRoot.transform.SetParent(root);
                lineRoot.transform.localRotation = Quaternion.identity;

                for (int line = 0; line < level + 1; line++)
                {
                    float offsetSet = offsetX*line;
                    Transform createPattern = CreatePattern(
                        new Vector3(offsetSet , 0 , offsetSet) , lineRoot.transform ,
                        level , line);

                    Cube cube = createPattern.GetComponent<Cube>();
                    if (cube)
                    {
                        cube.cubePosition = new PositionCube(level , level - line);
                        map.Add(cube);
                    }
                }

                lineRoot.transform.localPosition = new Vector3(-level , -level * offsetY);
            }
        }
    }

    public void CreateMapCenter()
    {
        if (root && pattern)
        {
            CreateMap();

            Cube cube = FindCubeToPoint(new PositionCube(startLine, startPos));
            Vector3 posUpSide = cube.upSide.position;
            root.localPosition -= posUpSide;
        }
    }

    public Cube FindCubeToPoint(PositionCube point)
    {
        return map.FirstOrDefault(x => x.cubePosition == point);
    }

    private Transform CreatePattern(Vector3 position , Transform rootLine , int line , int pos)
    {
        Transform createPattern = Instantiate(pattern) as Transform;
        createPattern.SetParent(rootLine);
        createPattern.transform.localRotation = Quaternion.identity;
        createPattern.localPosition = position;
        createPattern.localScale = new Vector3(offsetX , offsetY , offsetX);
        createPattern.name = string.Format("Cube_{0}_{1}", line, pos);
        return createPattern;
    }

    private void DestroyOldMap()
    {
        var list = root.Cast<Transform>().ToArray();

        foreach (var child in list)
        {
            //Debug.Log(child.name);
            child.gameObject.SetActive(false);
            DestroyImmediate(child.gameObject , true);
        }
    }

    void Start()
    {

    }
}
