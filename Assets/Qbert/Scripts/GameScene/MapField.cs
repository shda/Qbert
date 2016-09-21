using System;
using System.Collections;
using System.Linq;
using Scripts.GameScene.Characters;
using Scripts.GameScene.MapLoader;
using UnityEngine;

namespace Scripts.GameScene
{
    public class MapField : MonoBehaviour
    {
        public MapFieldGenerator mapGenerator;
        public Action<Cube, Character> OnPressCubeEvents; 
        public Cube[] field;


        public void FlashGameFiels(Action OnEndFlash)
        {
            StartCoroutine(FlashField(OnEndFlash));
        }

        private IEnumerator FlashField(Action OnEndFlash)
        {
            yield return null;

            for (int i = 0; i < 4; i++)
            {
                foreach (var cube in field)
                {
                    cube.SetFlashColorOne();
                }

                yield return new WaitForSeconds(0.3f);

                foreach (var cube in field)
                {
                    cube.SetFlashColorTwo();
                }

                yield return new WaitForSeconds(0.3f);
            }

            if (OnEndFlash != null)
            {
                OnEndFlash();
            }
        }

        public void Init()
        {
            ParseMap();
            ConnectEvents();
        }

        public void ConnectEvents()
        {
            foreach (var cube in field)
            {
                cube.OnPressEvents = OnPressEvents;
            }
        }

        private void OnPressEvents(Cube cube, Character character)
        {
            if (OnPressCubeEvents != null)
            {
                OnPressCubeEvents(cube , character);
            }
        }

        public void ParseMap()
        {
            field = mapGenerator.GetComponentsInChildren<Cube>();
            ConnectEvents();
        }

        public Cube GetCube(PositionCube cube)
        {
            return field.FirstOrDefault(x => x.currentPosition == cube);
        }

        public Cube GetCubeDirection(DirectionMove.Direction buttonType , PositionCube point)
        {
            return GetCube(GetPointCubeDirection(buttonType , point));
        }

        public PositionCube GetPointCubeDirection(DirectionMove.Direction buttonType, PositionCube point)
        {
            int findLevel = -1;
            int findNumber = -1;

            if (buttonType == DirectionMove.Direction.DownLeft)
            {
                findLevel = point.y + 1;
                findNumber = point.x;
            }
            else if (buttonType == DirectionMove.Direction.UpRight)
            {
                findLevel = point.y - 1;
                findNumber = point.x;
            }
            else if (buttonType == DirectionMove.Direction.DownRight)
            {
                findLevel = point.y + 1;
                findNumber = point.x + 1;
            }
            else if (buttonType == DirectionMove.Direction.UpLeft)
            {
                findLevel = point.y - 1;
                findNumber = point.x - 1;
            }

            return new PositionCube(findNumber , findLevel);
        }

        public Vector3 GetOffsetDirection(DirectionMove.Direction direction)
        {
            float x = 0, y = 0;

            if (direction == DirectionMove.Direction.UpRight)
            {
                x += 1;
                y += 1;
            }
            else
                if (direction == DirectionMove.Direction.UpLeft)
                {
                    x += -1;
                    y += 1;
                }
                else
                    if (direction == DirectionMove.Direction.DownRight)
                    {
                        x += 1;
                        y += -1;
                    }
                    else
                        if (direction == DirectionMove.Direction.DownLeft)
                        {
                            x += -1;
                            y += -1;
                        }


            return new Vector3(x * mapGenerator.offsetX, 0,
                y * mapGenerator.offsetX);
        }

        public Vector3 GetOffset(PositionCube start, PositionCube end)
        {
            int x = start.y - end.y;
            int y = start.x - end.x;

            return new Vector3(x * mapGenerator.offsetX, y * mapGenerator.offsetX,
                0);
        }
    }
}
