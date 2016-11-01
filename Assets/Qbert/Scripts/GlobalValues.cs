using Assets.Qbert.Scripts.Utils.Save;

namespace Assets.Qbert.Scripts
{
    public class GlobalValues : SaveInPlayerPref<GlobalValues>
    {
        public static int currentLevel = 0;
        public static int currentRound = 0;
        public static float score = 0;
        public static int countLive = 1;

        private const int countGoldToGift = 50;
        public const int countCoinsToUnlockChar = 100;

        public static int[] castCoinsInvest = new[] { 25, 50, 100 };
        public static int[] timeInGameGift = new[] { 6, 40, 360 };

        //Show or hide button double gift by watch video
        [SaveFieldAttribute]
        public static bool isShowGiftDoubleFromVideo;

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


        public static void AddGiftDoubleByWatchVideo()
        {
            coins += countGoldToGift;
        }

        public static void AddGiftGold()
        {
            coins += countGoldToGift;
        }

        public static int  GetNextTimeGift()
        {
            return 4;
        }

        public static void UpdateBestScore()
        {
            if (score > bestScore)
            {
                bestScore = score;
                Save();
            }
        }


        static GlobalValues()
        {
            Load();
        }
    }
}