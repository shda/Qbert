using UnityEngine;

namespace Assets.Qbert.Scripts.GUI.GUISettings
{
    public class CoinBuyModels : MonoBehaviour 
    {
        public float price;
        public int  coinsAdd;
        public bool isWatchAd;

        [Multiline]
        public string description;
    }
}
