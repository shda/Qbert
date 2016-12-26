using System.Collections;
using System.Linq;
using Assets.Qbert.Scripts.GameScene.Levels;
using Assets.Qbert.Scripts.Utils;
using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.Characters.Enemy
{
    public class CoinWhite : CoinCube
    {
        public override Type typeObject
        {
            get { return Type.CoinWhite; }
        }

        public override bool OnProcessingQbertCollision(Qbert qbert)
        {
            AddCoins(levelController.globalConfiguraion.scoprePrice.addCoinsWhiteToCoin * qbert.cointMultiplier);
            OnStartDestroy();
            return true;
        }
    }
}
