using UnityEngine;
using System.Collections;

public class MapAsset : ScriptableObject
{
    public int mapWidth;
    public int mapHight;
    public Transform[] cubePaterns;
    public CubeMap map;

    public void UpdateFromInspector()
    {
        map.UpdateFromInspector(mapWidth, mapHight);
    }
}
