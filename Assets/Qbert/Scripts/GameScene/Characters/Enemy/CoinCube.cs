﻿using System.Collections;
using System.Linq;
using Scripts.GameScene.Levels;
using Scripts.Utils;
using UnityEngine;

namespace Scripts.GameScene.Characters.Enemy
{
    public class CoinCube : RedCube
    {
        public override Type typeObject
        {
            get { return Type.CoinCube; }
        }

        public override GameplayObject TryInitializeObject(Transform root, LevelController levelController)
        {
            Cube cubeSet = null;

            var points = levelController.mapField.mapGenerator.map.Where(
                x => x.cubeInMap.listTypeObjectsStartPoint != null &&
                     x.cubeInMap.listTypeObjectsStartPoint.Contains(typeObject));

            if (points.Any())
            {
                cubeSet = points.Mix().First();
            }
            else
            {
                var map = levelController.mapField.mapGenerator.map.ToArray();
                cubeSet = map.Mix().First();
            }

            var gaToPoint = levelController.gameplayObjects.GetGamplayObjectInPoint(cubeSet.currentPosition);
            if (gaToPoint == null)
            {
                return SetObject(root, levelController, cubeSet.currentPosition);
            }

            return null;
        }

        public override void Run()
        {
            StartCoroutine(WorkThead());
        }

        protected override IEnumerator WorkThead()
        {
            yield return StartCoroutine(DropToCube());
        }

        public override bool OnColisionToQbert(Qbert qbert)
        {
            if (qbert.isCheckColision)
            {
                AddCoins(ScorePrice.addCoinsToCoin);

                OnStartDestroy();
                return true;
            }

            return true;
        }

        public override bool CanJumpToMy()
        {
            return true;
        }
    }
}