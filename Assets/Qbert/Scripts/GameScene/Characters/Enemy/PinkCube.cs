using System;
using System.Collections;
using Assets.Qbert.Scripts.GameScene.Levels;
using Assets.Qbert.Scripts.Utils;
using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.Characters.Enemy
{
    public class PinkCube : RedCube 
    {
        public enum SideCube
        {
            Left,
            Right,
        }

        public SideCube sideCube;
        public Transform jumpRoot;
        public Vector3 jumpVector;
        public Transform rootModel;

        public PositionCube stuckPosition;

        public float maxStuckTime = 4.0f;

        private bool stuck = false;
        private float currentStuckTimer;

        public override Type typeObject
        {
            get { return Type.PinkCube; }
        }

        public override GameplayObject TryInitializeObject(Transform root, LevelController levelController)
        {
            var startPos = levelController.mapField.mapGenerator.
                GetPointOutsideFieldStartPointToType(typeObject);

            var positions = startPos.Mix();

            foreach (var positionCube in positions)
            {
                var neighbors = levelController.mapField.mapGenerator.
                    GetNeighborsCubes(positionCube.currentPoint);

                if (neighbors.Count > 0)
                {
                    foreach (var neighbor in neighbors)
                    {
                        var gaToPoint = levelController.gameplayObjects.GetGamplayObjectInPoint(neighbor.currentPosition);
                        if (gaToPoint == null || gaToPoint.CanJumpToMy())
                        {
                            sideCube = IsLeft(neighbor.currentPosition, positionCube.currentPoint) ?
                                SideCube.Left : SideCube.Right;

                            currentPosition = neighbor.currentPosition;

                            var obj = SetObject(root, levelController, currentPosition);

                            if (sideCube == SideCube.Right)
                            {
                                rootModel.localRotation = Quaternion.Euler(0,0,0);
                            }
                            else
                            {
                                rootModel.localRotation = Quaternion.Euler(0, 180, 0);
                            }

                            return obj;
                        }
                    }
                }
            }

            return null;
        }

        public bool IsLeft(PositionCube one , PositionCube two)
        {
            if (two == new PositionCube(one.x - 1 , one.y - 1) ||
                two == new PositionCube(one.x , one.y + 1))
            {
                return true;
            }

            return false;
        }

        public override void SetStartPosition(PositionCube point)
        {
            StopAllCoroutines();

            stuckPosition = point;
            positionMove = point;

            var startCube = levelController.mapField.GetCube(point);
            if (startCube)
            {
                if (sideCube == SideCube.Left)
                {
                    root.position = startCube.leftSide.position;
                }
                else
                {
                    root.position = startCube.rightSide.position;
                }

                root.rotation = startCube.rightSide.rotation;
            }
            moveCoroutine = null;
        }

        protected override IEnumerator DropToCube()
        {
            var targetPosition = root.position;

            var cube = levelController.mapField.GetCube(stuckPosition);


            if (sideCube == SideCube.Left)
            {
                root.position += Vector3.left*heightDrop;
                root.rotation = cube.leftSide.rotation;
            }
            else
            {
                root.position += Vector3.right * heightDrop;
                root.rotation = cube.rightSide.rotation;
            }

            yield return new WaitForSeconds(0.2f);
            yield return StartCoroutine(this.MovingTransformTo(root, targetPosition, 0.5f, iTimeScaler));
            yield return null;
        }

        protected override IEnumerator WorkThead()
        {
            yield return StartCoroutine(DropToCube());

            while (true)
            {
                yield return new WaitForSeconds(0.2f);

                if (!isMoving)
                {
                    Cube cubeTarget = null;// GetNextCube();

                    if (GetMoveCube(ref cubeTarget))
                    {
                        stuck = false;

                        if (cubeTarget)
                        {
                            MoveToCube(cubeTarget);
                            yield return StartCoroutine(this.WaitForSecondITime(1.0f, iTimeScaler));
                        }
                        else
                        {
                            yield return StartCoroutine(ReachedLowerLevel());
                            yield break;
                        }
                    }
                    else
                    {
                        stuck = true;
                    }
                }
            }

            yield return null;
        }

        protected virtual IEnumerator ReachedLowerLevel()
        {
            Vector3 newPos = root.position - jumpVector;
            // root.position = newPos;

            yield return StartCoroutine(JumpAndMoveToPoint(newPos));
            yield return StartCoroutine(DropDown());
            OnStartDestroy();
        }

        protected override IEnumerator DropDown()
        {
            Vector3 dropDownPoint = root.position - new Vector3(dropDownHeight * 0.5f, 0, -dropDownHeight * 0.5f);

            if (sideCube == SideCube.Left)
            {
                dropDownPoint = root.position + new Vector3(dropDownHeight * 0.5f, 0, dropDownHeight * 0.5f);
            }

            yield return StartCoroutine(this.MovingTransformTo(root, dropDownPoint, timeDropDown, iTimeScaler));
        }

        public override Vector3 GetRotationToCube(Cube cube)
        {
            if (sideCube == SideCube.Left)
            {
                return cube.leftSide.rotation.eulerAngles;
            }

            return cube.rightSide.rotation.eulerAngles;
        }

        protected override IEnumerator RotateToCube(Cube cube)
        {
            var rotateTo = GetRotationToCube(cube);
            yield return StartCoroutine(RotateTo(rotateTo));
        }

        protected override IEnumerator JumpAndMove(Cube cube)
        {
            Vector3 cubeSide = GetSideCube(cube);

            jumpVector = root.position - cubeSide;

            yield return StartCoroutine(JumpAndMoveToPoint(GetSideCube(cube)));
            yield return null;
        }

        public override IEnumerator MoveToCubeAnimation(Cube cube, Action<Character> OnEnd = null)
        {
            if (!isMoving)
            {
                positionMove = GetHungPosition(cube.currentPosition);
                yield return StartCoroutine(RotateToCube(cube));
                yield return StartCoroutine(JumpAndMove(cube));
                stuckPosition = cube.currentPosition;
                positionMove = new PositionCube(-10,-10);

                currentPosition = GetHungPosition(stuckPosition);

                cube.OnPressMy(this);
            }

            moveCoroutine = null;

            if (OnEnd != null)
            {
                OnEnd(this);
            }
        }

        public PositionCube GetHungPosition(PositionCube stuckPosition)
        {
            if (sideCube == SideCube.Left)
            {
                return new PositionCube( stuckPosition.x , stuckPosition.y + 1);
            }

            return new PositionCube( stuckPosition.x + 1 , stuckPosition.y + 1);
        }

        protected override IEnumerator JumpAndMoveToPoint(Vector3 point)
        {
            float t = 0;
            var movingTo = point;
            var startTo = root.position;

            while (t < 1)
            {
                t += CoroutinesHalpers.GetTimeDeltatimeScale(iTimeScaler) / timeMove;
                t = Mathf.Clamp01(t);
                Vector3 pos = Vector3.Lerp(startTo, movingTo, t);
                JumpOffset(t);
                SetTimeAnimationJump(t);
                root.position = pos;
                yield return null;
            }

            root.position = movingTo;
        }

        protected virtual void JumpOffset(float t)
        {
            float lerp = GetOffsetLerp(t);
            jumpRoot.localPosition = new Vector3(0, 0, -lerp);
        }

        protected override float GetOffsetLerp(float t)
        {
            float retFloat = jumpAmplitude;

            if (t < 0.5f)
            {
                retFloat = jumpAmplitude * t;
            }
            else
            {
                retFloat = jumpAmplitude - (jumpAmplitude * t);
            }

            return retFloat;
        }

        public GameplayObject GetGamplayObjectInPoint(PositionCube point)
        {
            foreach (var gameplayObject in levelController.gameplayObjects.gameplayObjectsList)
            {
                if (gameplayObject.currentPosition == point || gameplayObject.positionMove == point && gameplayObject != this)
                {
                    return gameplayObject;
                }
            }

            return null;
        }

        private bool GetMoveCube(ref Cube retCube)
        {
            retCube = GetNextCube();

            if (retCube && currentStuckTimer < maxStuckTime)
            {
                //var gObject     = GetGamplayObjectInPoint(retCube.cubePosition);
                var hungObject  = GetGamplayObjectInPoint(GetHungPosition(retCube.currentPosition));

                if (/*gObject ||*/ hungObject)
                {
                    return false;
                }
            }

            currentStuckTimer = 0;

            return true;
        }

        private Cube GetNextCube()
        {
            PositionCube[] position = null;

            if (sideCube == SideCube.Left)
            {
                position = new[]
                {
                    new PositionCube( stuckPosition.x , stuckPosition.y - 1 ),
                    new PositionCube( stuckPosition.x + 1 , stuckPosition.y ),
                };
            }
            else
            {
                position = new[]
                {
                    new PositionCube(stuckPosition.x -1 , stuckPosition.y - 1 ),
                    new PositionCube(stuckPosition.x - 1 , stuckPosition.y ),
                };
            }

            position = position.Mix().ToArray();

            foreach (var positionCube in position)
            {
                Cube ret = levelController.mapField.GetCube(positionCube);

                if (ret != null)
                {
                    return ret;
                }
            }

            return null;
        }

        public Vector3 GetSideCube(Cube cude)
        {
            if (sideCube == SideCube.Left)
            {
                return cude.leftSide.position;
            }

            return cude.rightSide.position;
        }


        void Update()
        {
            if (stuck)
            {
                currentStuckTimer += timeScale*Time.deltaTime;
            }
            else
            {
                currentStuckTimer = 0;
            }
        }

    }
}
