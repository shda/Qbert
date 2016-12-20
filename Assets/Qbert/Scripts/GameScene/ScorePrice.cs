using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene
{
    [System.Serializable]
    public class ScorePrice 
    {
        [Header("Add score if:")]
        public float pressCubeNeedColor = 1;
        public float pressCubeMediumColor = 0.5f;
        public float getClueCube = 4;
        public float getColorCube = 10;
        public float dropPurpeCube = 20;
        public float dontUsingTransport = 2;

        [Header("Add gold if:")]
        public int addCoinsGoldToCoin = 1;
        public int addCoinsRedToCoin = 5;
        public int addCoinsWhiteToCoin = 10;
    }
}
