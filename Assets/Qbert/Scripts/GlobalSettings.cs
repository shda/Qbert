using Assets.Qbert.Scripts.Utils.Save;

namespace Assets.Qbert.Scripts
{
    public class GlobalSettings : SaveInPlayerPref<GlobalSettings>
    {
        public static int currentLevel = 0;
        public static int currentRound = 0;
        public static float score = 0;
        public static int countLive = 1;

        [SaveFieldAttribute]
        public static string currentModel;

        [SaveFieldAttribute]
        public static float oldMaxScore = 0;

        [SaveFieldAttribute]
        public static int coins = 0;

        static GlobalSettings()
        {
            Load();
        }
    }
}