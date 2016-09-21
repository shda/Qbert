﻿using System.Collections;
using System.Linq;
using Scripts.GameScene.MapLoader;
using Scripts.Utils;
using UnityEngine;

namespace Scripts.GameScene
{
    public class CubeCreateAnimator : MonoBehaviour
    {
        public MapFieldGenerator MapFieldGenerator;
        public float offset;
        public float duration;

        public void StartAnimateShow()
        {
            /*
            PositionCube centerPoint = new PositionCube( MapFieldGenerator.startPos , MapFieldGenerator.startLine);

            var workCubes = MapFieldGenerator.map.Where(x => x.currentPosition != centerPoint).ToArray();

            for (int c = 0; c < workCubes.Count(); c++)
            {
                var currentCube = workCubes[c];

                currentCube.gameObject.SetActive(false);

                StartCoroutine(StartCubeShow(currentCube, offset, duration, c * (duration * 0.5f)));
            }
            */
        }

        IEnumerator StartCubeShow(Cube cube , float offset , float duration , float delayStart)
        {
            yield return new WaitForSeconds(delayStart);

            cube.gameObject.SetActive(true);

            Vector3 endMovePositino = cube.transform.position;
            cube.transform.position -= new Vector3(0,offset , 0);

            yield return StartCoroutine(
                this.MovingTransformTo(cube.transform, endMovePositino, duration));
        }

        void Start () 
        {
	
        }
	
        void Update () 
        {
	
        }
    }
}
