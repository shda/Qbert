using System;
using Assets.Qbert.Scripts.GameScene.Characters;
using Assets.Qbert.Scripts.GameScene.GameAssets;

namespace Assets.Qbert.Scripts.GameScene.Levels
{
    public class LevelBonus : LevelLogic
    {
        public override LevelLogic.Type type
        {
            get { return LevelLogic.Type.LevelBonus; ; }
        }

        public override bool OnQbertPressToCube(Cube cube, Characters.Qbert qbert)
        {
            return false;
        }

        public override MapAsset GetMapAssetFromCurrentRound()
        {
            var randomRound = rounds[UnityEngine.Random.Range(0, rounds.Length)];
            return randomRound.customMap;
        }
        public override void InitLevel()
        {
            base.InitLevel();
        }

        public override void StartLevel(int round)
        {
            base.StartLevel(round);
            for (int i = 0; i < 10; i++)
            {
                levelController.gameplayObjects.AddGameplayObjectToGame(Character.Type.CoinCube);
            }
        }
    }
}
