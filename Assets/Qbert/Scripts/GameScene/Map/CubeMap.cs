using System.Collections.Generic;
using System.Linq;
using Assets.Qbert.Scripts.GameScene.Characters;
using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.Map
{
    [System.Serializable]
    public class CubeMap
    {
        [System.Serializable]
        public class CubeInMap
        {
            public int x;
            public int y;
            public Transform cubePattern;
            public bool isEnable = false;

            public List<Character.Type> listTypeObjectsStartPoint;
            public List<GameplayObject.Type> listTypeObjectsEndPoint;

            public void Set(CubeInMap value)
            {
                x = value.x;
                y = value.y;
                cubePattern = value.cubePattern;
                isEnable = value.isEnable;
                listTypeObjectsStartPoint = value.listTypeObjectsStartPoint;
                listTypeObjectsEndPoint = value.listTypeObjectsEndPoint;
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
            UnityEngine.Debug.Log("RemakeMap");

            int offset = 0;

            var newArray = new List<CubeInMap>();
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

        public void Clear()
        {
            foreach (var cubeInMap in cubeArray)
            {
                cubeInMap.isEnable = false;
                cubeInMap.cubePattern = null;
                cubeInMap.listTypeObjectsEndPoint = null;
                cubeInMap.listTypeObjectsStartPoint = null;
            }
        }
    }
}
