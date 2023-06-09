﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Qbert.Scripts.GameScene.Levels;
using Assets.Qbert.Scripts.Utils;
using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.Characters.Enemy
{
    public class RedCube : GameplayObject
    {
        [Header("RedCube")]
        public float heightDrop = 3.0f;

        public float pauseTimeAfterJump = 1.0f;

        [SerializeField]
        protected float maxStuckTime = 4.0f;
        protected bool  isStuck = false;
        protected float currentStuckTimer;

        public bool isTimeStuckIsDone
        {
            get { return currentStuckTimer >= maxStuckTime; }
        }

        public override Type typeObject
        {
            get { return Type.RedCube; }
        }

        public override GameplayObject TryInitializeObject(Transform root, LevelController levelController)
        {
            var positions = levelController.mapField.mapGenerator.GetCubesStartByType(typeObject).ToList();
            positions = positions.Mix();

            foreach (var positionCube in positions)
            {
                var gaToPoint = levelController.gameplayObjects.GetGameplayObjectInPoint(positionCube.currentPosition);
                if (gaToPoint == null || gaToPoint.CanJumpToMy)
                {
                    return SetObject(root, levelController, positionCube.currentPosition);
                }
            }

            return null;
        }

        public virtual GameplayObject SetObject(Transform root, LevelController levelController , PositionCube startPosition)
        {
            var gObject = base.TryInitializeObject(root, levelController);
            gObject.SetStartPosition(startPosition);
            gObject.transform.rotation *= Quaternion.Euler(0, 45, 0);
            return gObject;
        }

        public override void Run()
        {
            StartCoroutine(WorkThead());
        }

        protected virtual IEnumerator DropToCube()
        {
            var targetPosition = root.position;
            root.position = new Vector3(root.position.x, root.position.y + heightDrop, root.position.z);
            yield return new WaitForSeconds(0.2f);
            yield return StartCoroutine(this.MovingTransformTo(root, targetPosition, 0.2f , iTimeScaler));
            yield return null;
        }

        protected virtual IEnumerator WorkThead()
        {
            yield return StartCoroutine(DropToCube());

            while (true)
            {
                yield return new WaitForSeconds(0.2f);

                if (!isMoving)
                {
                    Cube cubeTarget = null;
                    DirectionMove.Direction direction = DirectionMove.Direction.DownLeft;;

                    if (GetMoveCube(ref cubeTarget , ref direction))
                    {
                        if (cubeTarget)
                        {
                            MoveToCube(cubeTarget);
                            yield return StartCoroutine(
                                this.WaitForSecondITime(pauseTimeAfterJump, iTimeScaler));
                        }
                        else if (!IsEndLine())
                        {
                            yield return StartCoroutine(ReachedLowerLevel(direction));
                            yield break;
                        }
                    }

                }
            }

            yield return null;
        }

        private bool IsEndLine()
        {
            var cube = levelController.mapField.GetCube(currentPosition);
            return cube.nodes.Any(x => x.currentPosition.y > currentPosition.y);
        }

        protected virtual IEnumerator ReachedLowerLevel(DirectionMove.Direction direction)
        {
            Vector3 newPos = root.position + levelController.mapField.GetOffsetDirection(direction);
            MoveToPointAndDropDown(newPos, character =>
            {
                OnStartDestroy();
            });

            yield break;
        }
        protected virtual bool GetMoveCube(ref Cube refCube, ref DirectionMove.Direction refDirection)
        {
            List<DirectionMove.Direction> directions = new List<DirectionMove.Direction>();
            directions.Add(DirectionMove.Direction.DownLeft);
            directions.Add(DirectionMove.Direction.DownRight);

            directions = directions.Mix();

            foreach (var direction in directions)
            {
                var cubeTarget = levelController.mapField.GetCubeDirection(direction, currentPosition);
                if (cubeTarget)
                {
                    if (CanJumpToThisCube(cubeTarget.currentPosition))
                    {
                        refCube = cubeTarget;
                        refDirection = direction;
                        return true;
                    }
                }
            }

            return true;
        }

        protected virtual bool CanJumpToThisCube(PositionCube positionCube)
        {
            var targetCube = levelController.gameplayObjects.GetGameplayObjectInPoint(positionCube);

            if (targetCube == null || targetCube.CanJumpToMy || isTimeStuckIsDone)
            {
                isStuck = false;
                currentStuckTimer = 0;
                return true;
            }

            isStuck = true;
            return false;
        }

        protected virtual void Update()
        {
            if (isStuck)
            {
                currentStuckTimer += timeScale * Time.deltaTime;
            }
            else
            {
                currentStuckTimer = 0;
            }
        }

        public override bool OnProcessingQbertCollision(Qbert qbert)
        {
            if (qbert.checkCollision == CollisionCheck.All)
            {
                return false;
            }

            return true;
        }
    }
}