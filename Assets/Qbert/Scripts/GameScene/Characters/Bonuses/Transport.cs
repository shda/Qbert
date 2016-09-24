﻿using System.Collections;
using Assets.Qbert.Scripts.GameScene.Levels;
using Assets.Qbert.Scripts.Utils;
using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.Characters.Bonuses
{
    public class Transport : GameplayObject
    {
        [Header("Transport")]
        public float speedMovingToPoint = 1.0f;
        public float offsetDrop = 1.0f;

        public Vector3 offsetToStartPoint;

        private Cube cubeJumpAfterMove;
        private Vector3 transportMoveToPoint;

        public override Type typeObject
        {
            get { return Type.Transport; }
        }

        public override GameplayObject TryInitializeObject(Transform root, LevelController levelController)
        {
            var transport = base.TryInitializeObject(root, levelController) as Transport;
            return transport;
        }

        public override void Init()
        {
            var startPos =  levelController.mapField.mapGenerator.
                GetPointOutsideFieldStartPointToType(Type.Transport);

            if(startPos != null)
            {
                foreach (var pos in startPos.Mix())
                {
                    if (!levelController.gameplayObjects.GetGamplayObjectInPoint(pos.currentPoint))
                    {
                        SetPosition(pos);
                    }
                }
            }
            else
            {
                UnityEngine.Debug.Log("Don't find start transport positions.");
            }
        }

        public void SetPosition(PointOutsideField setPosition)
        {
            currentPosition = setPosition.currentPoint;

            cubeJumpAfterMove = levelController.mapField.mapGenerator.
                GetCubeEndByType(Type.Transport);

            if (cubeJumpAfterMove != null)
            {
                positionMove = cubeJumpAfterMove.currentPosition;
                transportMoveToPoint = cubeJumpAfterMove.upSide.position + new Vector3(0 , offsetDrop ,0);
            }
            else
            {
                var movePosOut = levelController.mapField.mapGenerator.
                    GetPointsOutsideFieldEndPointToType(Type.Transport);

                if (movePosOut != null && movePosOut.Count > 0)
                {
                    var neighbors = levelController.mapField.mapGenerator.
                        GetNeighborsCubes(movePosOut[0].currentPoint);

                    transportMoveToPoint = movePosOut[0].transform.position + new Vector3(0, offsetDrop, 0);

                    if (neighbors.Count > 0)
                    {
                        cubeJumpAfterMove = neighbors[0];
                    }
                }
                else
                {
                    UnityEngine.Debug.LogError("Dont set end point position");
                }
            }
            
            transform.position = setPosition.transform.position + offsetToStartPoint;
            transform.rotation = setPosition.transform.rotation * Quaternion.Euler(new Vector3(0, -90, 0));
        }

        public override bool OnColisionToQbert(Qbert qbert)
        {
            if (!isMoving)
            {
                qbert.isFrize = true;
                qbert.isCheckColision = false;
                qbert.currentPosition = currentPosition;
                qbert.StopAllCoroutines();
                qbert.root.position = root.position;

                StartCoroutine(MoveTransport(qbert));

                return true;
            }

            return true;
        }

        public IEnumerator MoveTransport(Qbert qbert)
        {
            //StartCoroutine(this.MovingSpeedTransformTo(qbert.root, transportMoveToPoint, speedMovingToPoint));
            //yield return StartCoroutine(this.MovingSpeedTransformTo(transform, transportMoveToPoint, speedMovingToPoint));

            //var cp = currentPosition;
            //var movePos = positionMove;

            //if()

            var offset = Mathf.Abs(qbert.positionMove.y - cubeJumpAfterMove.currentPosition.y);

            var offsetF = offset*0.5f;

            var move = qbert.root.position + new Vector3(0, offsetF, -offsetF);

            yield return StartCoroutine(MoveTwo(qbert.root, transform, move, 10.0f));
            yield return StartCoroutine(MoveTwo(qbert.root , transform , transportMoveToPoint , speedMovingToPoint) );

            yield return new WaitForSeconds(0.5f);

            gameObject.SetActive(false);
            qbert.isFrize = false;
            qbert.isCheckColision = true;
            qbert.MoveToCube(cubeJumpAfterMove.currentPosition);

            OnStartDestroy();
        }

        IEnumerator MoveTwo(Transform one, Transform two , Vector3 moveTo , float speed)
        {
            StartCoroutine(this.MovingSpeedTransformTo(one, moveTo, speed));
            yield return StartCoroutine(this.MovingSpeedTransformTo(two, moveTo, speed));
        }
        public override bool CanJumpToMy()
        {
            return true;
        }

    }
}
