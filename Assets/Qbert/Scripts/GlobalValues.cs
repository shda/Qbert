using Assets.Qbert.Scripts.Utils.Save;

namespace Assets.Qbert.Scripts
{
    public class GlobalValues : SaveInPlayerPref<GlobalValues>
    {
        public static int currentLevel = 0;
        public static int currentRound = 0;
        public static float score = 0;
        public static int countLive = 1;

        public const int countGoldToGift = 50;
        public const int countCoinsToEarnVideo = 20;
        public const int countCoinsToUnlockChar = 100;

        public static int[] castCoinsInvest = new[] { 25, 50, 100 };
        public static int[] timeInGameGift = new[] { 6, 40, 360 };

        //Show or hide button double gift by watch video
        [SaveFieldAttribute]
        public static bool isShowGiftDoubleFromVideo = true;

        [SaveFieldAttribute]
        public static int giftTimeIndex = 0;

       // [SaveFieldAttribute]
        public static float timeInGame = 32;

        [SaveFieldAttribute]
        public static string currentModel = "";

        [SaveFieldAttribute]
        public static float bestScore = 0;

        [SaveFieldAttribute]
        public static int coins = 0;

        [SaveFieldAttribute]
        public static bool appIsRate = false;

        [SaveFieldAttribute]
        public static bool isShowHelpHandToSelectCharacter = true;


        public static string ConvertMinutesToString(int minutes)
        {
            if (minutes < 60)
            {
                return string.Format("{0}m", minutes);
            }

            return string.Format("{0}h", (int)minutes / 60);
        }

        public static int AddGiftDoubleByWatchVideo()
        {
            coins += countGoldToGift;
            Save();
            return coins;
        }

        public static int AddGiftGold()
        {
            coins += countGoldToGift;
            Save();
            return coins;
        }

        public static int AddEarnVideo()
        {
            coins += countCoinsToEarnVideo;
            Save();
            return coins;
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