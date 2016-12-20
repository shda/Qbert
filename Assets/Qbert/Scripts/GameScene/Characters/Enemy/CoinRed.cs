using System.Collections;
using System.Linq;
using Assets.Qbert.Scripts.GameScene.Levels;
using Assets.Qbert.Scripts.Utils;
using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.Characters.Enemy
{
    public class CoinRed : CoinCube
    {
        public override Type typeObject
        {
            get { return Type.CoinRed; }
        }

        public override bool OnColisionToQbert(Qbert qbert)
        {
            AddCoins(levelController.globalConfiguraion.scoprePrice.addCoinsRedToCoin * qbert.cointMultiplier);
            OnStartDestroy();
            return true;
        }
    }
}
