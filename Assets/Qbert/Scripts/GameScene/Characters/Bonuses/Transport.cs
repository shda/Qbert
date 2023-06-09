﻿using System.Collections;
using System.Linq;
using Assets.Qbert.Scripts.GameScene.Levels;
using Assets.Qbert.Scripts.Utils;
using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.Characters.Bonuses
{
    public class Transport : GameplayObject
    {
        [Header("Transport")]
        public float durationMoveToEndPoint = 2.0f;
        public float offsetDrop = 1.0f;

        public Vector3 offsetToStartPoint;

        private Cube cubeJumpAfterMove;
        private Vector3 transportMoveToPoint;

        public override Type typeObject
        {
            get { return Type.Transport; }
        }

        public override bool CanJumpToMy
        {
            get { return true; }
        }

        public override bool IsFlashPlaceDownObject
        {
            get { return false; }
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
                    if (!levelController.gameplayObjects.GetGameplayObjectInPoint(pos.currentPoint))
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

        public override bool OnProcessingQbertCollision(Qbert qbert)
        {
            if (!isMoving)
            {
                qbert.isFrize = true;
                qbert.checkCollision = CollisionCheck.No;
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
            var offset = Mathf.Abs(qbert.positionMove.y - cubeJumpAfterMove.currentPosition.y);
            var offsetF = offset*0.5f;
            var move = qbert.root.position + new Vector3(0, offsetF, -offsetF);

            if(levelController.cameraFallowToCharacter != null)
                levelController.cameraFallowToCharacter.SetTarget(qbert.root);

            yield return StartCoroutine(MoveTwo(qbert.root, transform, move, 1.0f));
            yield return StartCoroutine(MoveTwo(qbert.root, transform , transportMoveToPoint , durationMoveToEndPoint) );

            yield return new WaitForSeconds(0.5f);

            gameObject.SetActive(false);
            
            qbert.MoveToCube(cubeJumpAfterMove.currentPosition , () =>
            {
                qbert.isFrize = false;
                qbert.checkCollision = CollisionCheck.All;


                if (cubeJumpAfterMove.nodes != null && cubeJumpAfterMove.nodes.Count > 0)
                {
                    var first = cubeJumpAfterMove.nodes.First();
                    qbert.StartCoroutine(qbert.RotateToCube(first));
                }
                


                if (levelController.cameraFallowToCharacter != null)
                    levelController.cameraFallowToCharacter.SetTarget(null);
            });

            OnStartDestroy();
        }

        IEnumerator MoveTwo(Transform one, Transform two , Vector3 moveTo , float speed)
        {
            StartCoroutine(this.MovingTransformTo(one, moveTo, speed));
            yield return StartCoroutine(this.MovingTransformTo(two, moveTo, speed));
        }
    }
}
