using Assets.Qbert.Scripts.GameScene.Characters;
using Assets.Qbert.Scripts.GameScene.GameAssets;
using UnityEngine;
using Random = System.Random;

namespace Assets.Qbert.Scripts.GameScene.Levels
{
    public class LevelBonus : LevelLogic
    {
        public float startRedCoinChancePercent = 20;
        public float startWhiteCoinChancePercent = 10;

        public float incrementByLevelRed = 2;
        public float incrementByLevelWhite = 1;

        public override LevelLogic.Type type
        {
            get { return LevelLogic.Type.LevelBonus; ; }
        }

        public override bool OnQbertPressToCube(Cube cube, Characters.Qbert qbert)
        {
            return false;
        }

        public override bool isPlayerCanFallOffCube
        {
            get { return false; }
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

            for (int i = 0; i < levelController.mapField.field.Length; i++)
            {
                CreateCoin();
            }
            

            /*
            for (int i = 0; i < 10; i++)
            {
                levelController.gameplayObjects.AddGameplayObjectToGame(Character.Type.CoinGold);
            }
            */
        }

        private void CreateCoin()
        {
            int inLelvel = GlobalValues.currentLevel + 1;

            float whitePercent = startWhiteCoinChancePercent + incrementByLevelWhite * inLelvel;
            float redPercent = startRedCoinChancePercent + incrementByLevelRed * inLelvel;

            whitePercent = Mathf.Clamp(whitePercent, 0, 100);
            redPercent = Mathf.Clamp(redPercent, 0, 100);

            int randValue = UnityEngine.Random.Range(0, 100);

            var type = Character.Type.CoinGold;

            if (randValue < whitePercent)
            {
                //white
                type = Character.Type.CoinRed;
            }
            else if (randValue < whitePercent + redPercent)
            {
                //red
                type = Character.Type.CoinWhite;
            }
            else
            {
                //gold

            }

            levelController.gameplayObjects.AddGameplayObjectToGame(type);
        }
    }
}
