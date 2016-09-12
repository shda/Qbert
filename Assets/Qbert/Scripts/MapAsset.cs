using UnityEngine;
using System.Collections;

public class MapAsset : ScriptableObject
{
    public int width;
    public int hight;
    public CubeMap map;

    public void UpdateFromInspector()
    {
        map.UpdateFromInspector(width , hight);
    }
}
