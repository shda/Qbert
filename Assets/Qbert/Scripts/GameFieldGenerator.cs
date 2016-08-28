using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[ExecuteInEditMode]
public class GameFieldGenerator : MonoBehaviour
{
    public float offsetX = 1.0f;
    public float offsetY = 1.0f;
    public int levels = 7;
    public Transform root;
    public Transform pattern;

    public List<List<Transform>> map; 

    private float oldOffset;
    private int oldLevels = -1;

    public void CreateMap()
    {
        if (root && pattern)
        {
            DestroyOldMap();
            
            map = new List<List<Transform>>();

            for (int level = 0; level < levels; level++)
            {
                List <Transform> lineTransform = new List<Transform>();

                GameObject lineRoot = new GameObject("Line" + level);
                lineRoot.transform.SetParent(root);
                lineRoot.transform.localRotation = Quaternion.identity;

                for (int line = 0; line < level + 1; line++)
                {
                    float offsetSet = offsetX*line;
                    Transform createPattern = CreatePattern(new Vector3(offsetSet , 0 , offsetSet) , lineRoot.transform);
                    lineTransform.Add(createPattern);

                    Cube cube = createPattern.GetComponent<Cube>();
                    if (cube)
                    {
                        cube.cubePosition = new PositionCube(level , level - line);
                    }
                }

                lineRoot.transform.localPosition = new Vector3(-level , -level * offsetY);

                map.Add(lineTransform);
            }
            
        }
    }

    public Vector3 CalculatePosition(PositionCube point)
    {
        return root.TransformDirection(new Vector3(point.line * offsetX, point.position * offsetY));
    }

    private Transform CreatePattern(Vector3 position , Transform rootLine)
    {
        Transform createPattern = Instantiate(pattern) as Transform;
        createPattern.SetParent(rootLine);
        createPattern.transform.localRotation = Quaternion.identity;
        createPattern.localPosition = position;

        return createPattern;
    }

    private void DestroyOldMap()
    {
        var list = root.Cast<Transform>().ToArray();

        foreach (var child in list)
        {
            Debug.Log(child.name);
            child.gameObject.SetActive(false);
            DestroyImmediate(child.gameObject , true);
        }
    }

    void Start ()
    {
	
	}
	
	void Update ()
    {

	}
}
