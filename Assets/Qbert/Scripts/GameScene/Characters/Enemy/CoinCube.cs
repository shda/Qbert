using System.Collections;
using System.Linq;
using Assets.Qbert.Scripts.GameScene.Levels;
using Assets.Qbert.Scripts.GameScene.Sound;
using Assets.Qbert.Scripts.Utils;
using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.Characters.Enemy
{
    public class CoinCube : RedCube
    {
        public override Type typeObject
        {
            get { return Type.CoinGold; }
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
            Cube cubeSet = null;

            var points = levelController.mapField.mapGenerator.map.Where(
                x => x.cubeInMap.listTypeObjectsStartPoint != null &&
                     x.cubeInMap.listTypeObjectsStartPoint.Contains(typeObject));

            var ieList = points;

            if (points.Any())
            {
                ieList = points.Mix();
            }
            else
            {
                ieList = levelController.mapField.mapGenerator.map;
            }

            foreach (var value in ieList)
            {
                var gaToPoint = levelController.gameplayObjects.GetGameplayObjectInPoint(value.currentPosition);
                if (gaToPoint == null)
                {
                    return SetObject(root, levelController, value.currentPosition);
                }
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

        public override bool OnProcessingQbertCollision(Qbert qbert)
        {
            if (qbert.checkCollision == CollisionCheck.All ||
                qbert.checkCollision == CollisionCheck.OnlyBonus)
            {
                AddCoins(levelController.globalConfiguraion.scoprePrice.addCoinsGoldToCoin * qbert.cointMultiplier);
                PlaySound();
                OnStartDestroy();
            }

            return true;
        }

        protected void PlaySound()
        {
            GameSound.PlayCoinUp();
        }
    }
}
