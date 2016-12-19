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

        public override bool OnColisionToQbert(Qbert qbert)
        {
            //if (qbert.isCheckColision)
            {
                AddCoins(ScorePrice.addCoinsToCoin);

                OnStartDestroy();
                return true;
            }

            // return true;
        }
    }
}
