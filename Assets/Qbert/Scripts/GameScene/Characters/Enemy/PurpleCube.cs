using System.Collections;
using System.Collections.Generic;
using Assets.Qbert.Scripts.Utils;
using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.Characters.Enemy
{
    public class PurpleCube : RedCube
    {
        [Header("PurpleCube")]
        public AnimationToTime.AnimationToTime rebornAnimation;
        public PathFinder pathFinder;

        public Cube myCube;
        public Cube qbertCube;

        public override Type typeObject
        {
            get { return Type.PurpleCube; }
        }

        public override void Run()
        {
            StartCoroutine(WorkThead());
        }

        protected override IEnumerator ReachedLowerLevel(DirectionMove.Direction direction)
        {
            qbertCube = levelController.mapField.GetCube(levelController.qbert.currentPosition);
            yield return StartCoroutine(RebornToEnemy());
            yield return StartCoroutine(FallowToQbert());
        }

        public IEnumerator RebornToEnemy()
        {
            yield return StartCoroutine(rebornAnimation.PlayToTime(2.0f , iTimeScaler));
        }

        public IEnumerator FallowToQbert()
        {
            while (true)
            {
                Qbert qbert = levelController.qbert;
                Cube cube = FindPathToQberd(qbert);
                if (cube)
                {
                    jumpAnimationToTime = null;
                    yield return StartCoroutine(MoveToCubeAnimation(cube));
                }

                yield return StartCoroutine(this.WaitForSecondITime(0.5f, iTimeScaler));
            }

            yield return null;
        }

        void Start()
        {
            pathFinder = new PathFinder();
        }

        private Cube FindPathToQberd(Qbert qbert)
        {
            PositionCube qbertPoint = qbert.currentPosition;
            PositionCube myPoint = currentPosition;

            if (qbertPoint == myPoint)
            {
                return null;
            }

            var qbertCubePath = levelController.mapField.GetCube(qbertPoint);
            if (qbertCubePath == null && qbertCube == null)
            {
                return null;
            }

            PositionCube step = GetNeighborPoint(myPoint , qbertPoint);
            var targetCube = levelController.gameplayObjects.GetGamplayObjectInPoint(step);
            if (targetCube && !targetCube.CanJumpToMy())
            {
                return null;
            }

            return levelController.mapField.GetCube(step);
        }

        public List<Cube> findPath; 

        public PositionCube GetNeighborPoint(PositionCube myPoint, PositionCube qbertPoint)
        {
            var myCubePath      = levelController.mapField.GetCube(myPoint);
            var qbertCubePath   = levelController.mapField.GetCube(qbertPoint);

            if (qbertCubePath == null)
            {
                qbertCubePath = qbertCube;

                if (myCubePath == qbertCubePath)
                {
                    Drop(myPoint, qbertPoint);
                    return qbertPoint;
                }
            }

            qbertCube = qbertCubePath;
            findPath = pathFinder.FindPath(myCubePath,qbertCubePath);

            if (findPath != null && findPath.Count >= 2)
            {
                return findPath[findPath.Count - 2].currentPosition;
            }

            return qbertPoint;
        }

        void Update()
        {

        }

        public void Drop(PositionCube start , PositionCube end)
        {
            var jumpTo = levelController.mapField.mapGenerator.GetPointOutsideFieldToPosition(end);

            AddScore(ScorePrice.dropPurpeCube);

            Vector3 newPos = jumpTo.transform.position;
            MoveToPointAndDropDown(newPos, character =>
            {
                OnStartDestroy();
            });
        }

        public int GetDistance(PositionCube start, PositionCube end)
        {
            return Mathf.Abs(start.y - end.y) + Mathf.Abs(start.x - end.x);
        }

        public override bool OnColisionToQbert(Qbert qbert)
        {
            return true;
        }
    }
}

