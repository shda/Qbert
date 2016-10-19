using Assets.Qbert.Scripts.Utils.Save;

namespace Assets.Qbert.Scripts
{
    public class GlobalValues : SaveInPlayerPref<GlobalValues>
    {
        public static int currentLevel = 0;
        public static int currentRound = 0;
        public static float score = 0;
        public static int countLive = 1;

        public static int[] castCoinsInvest = new[] { 25, 50, 100 };

        public const int countCoinsToUnlockChar = 100;

        public static int[] timeInGameGift = new[] { 6, 40, 360 };

        [SaveFieldAttribute]
        public static int giftTimeIndex = 0;

        [SaveFieldAttribute]
        public static float timeInGame = 0;

        [SaveFieldAttribute]
        public static string currentModel = "";

        [SaveFieldAttribute]
        public static float bestScore = 0;

        [SaveFieldAttribute]
        public static int coins = 0;

        [SaveFieldAttribute]
        public static bool isShowHelpHandToSelectCharacter = true;

        public static void UpdateBestScore()
        {
            if (score > bestScore)
            {
                bestScore = score;
            }
        }


        static GlobalValues()
        {
            Load();
        }
    }
}