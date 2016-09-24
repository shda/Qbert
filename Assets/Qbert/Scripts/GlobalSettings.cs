namespace Scripts
{
    public class GlobalSettings : SaveInPlayerPref<GlobalSettings>
    {
        public static int currentLevel = 0;
        public static int currentRound = 0;
        public static float score = 0;
        public static int countLive = 1;

        [SaveField]
        public static int coins = 0;

        static GlobalSettings()
        {
            Load();
        }
    }
}