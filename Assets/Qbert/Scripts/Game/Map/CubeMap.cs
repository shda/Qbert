using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

[System.Serializable]
public class CubeMap
{
    [System.Serializable]
    public class CubeInMap
    {
        public int x;
        public int y;
        public int id = -1;
        public Transform cubePatern;
        public bool isEnable = false;

        public void Set(CubeInMap value)
        {
            x = value.x;
            y = value.y;
            id = value.id;
            cubePatern = value.cubePatern;
            isEnable = value.isEnable;
        }
    }

    public List<CubeInMap> cubeArray = new List<CubeInMap>();

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

    private void RemakeMap(int offsetX = 0, int offsetY = 0)
    {
        Debug.Log("RemakeMap");

        int offset = 0;

        var newArray = new List<CubeInMap>();

        //cubeArray = new List<CubeInMap>();
        for (int posY = 0; posY < hight; ++posY)
        {
            if (posY % 2 == 0)
            {
                offset += 1;
            }

            for (int posX = 0; posX < width; ++posX)
            {
                int xSet =  posX + offset;
                int ySet =  posY;

                newArray.Add(new CubeInMap() {x = xSet , y = ySet});
            } 
        }

        CopyToNewArray(newArray , offsetX , offsetY);
    }

    private void CopyToNewArray(List<CubeInMap> newArray , int offsetX , int offsetY)
    {
        foreach (var cubeInMap in cubeArray)
        {
            //var find = newArray.FirstOrDefault(x => x.x == cubeInMap.x && x.y == cubeInMap.y);
            var offset = newArray.FirstOrDefault(
                x => x.x == cubeInMap.x + offsetX && x.y == cubeInMap.y + offsetY);

            if (offset != null)
            {
                offset.Set(cubeInMap);
                offset.x += offsetX;
                offset.y += offsetY;
            }
        }

        cubeArray = newArray;
    }

    public void Move(int offsetX, int offsetY)
    {
        RemakeMap(offsetX, offsetY);
    }
}
