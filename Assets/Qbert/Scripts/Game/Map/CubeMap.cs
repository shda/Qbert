using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CubeMap
{
    [System.Serializable]
    public class CubeInMap
    {
        public int x;
        public int y;

        public bool isEnable = false;
    }

    public List<CubeInMap> cubeArray;

    public int width;
    public int hight;

    

    public CubeInMap GetCubeInMap(int x , int y)
    {
        return cubeArray[ (width * y) + x ];
    }

    public void UpdateFromInspector(int width ,int hight)
    {
        SetSize(width , hight);
    }

    public void SetSize(int width, int hight)
    {
        if (this.width != width || this.hight != hight)
        {
            this.width = width;
            this.hight = hight;

            RemakeMap();
        }
    }

    private void RemakeMap()
    {
        cubeArray = new List<CubeInMap>();
        for (int posY = 0; posY < hight; ++posY)
        {
            for (int posX = 0; posX < width; ++posX)
            {
                cubeArray.Add(new CubeInMap() { x = posX, y = posY });
            }
        }
    }
}
