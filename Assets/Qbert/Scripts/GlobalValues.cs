using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Qbert.Scripts.Utils.Save;
using UnityEngine;

namespace Assets.Qbert.Scripts
{
    public class GlobalValues : SaveInPlayerPref<GlobalValues>
    {
        public static int currentLevel = 0;
        public static int currentRound = 0;
        public static bool isBonusLevel = false;
        public static float score = 0;
        public static int countLive = 1;

        public const int countGoldToGift = 50;
        public const int countCoinsToEarnVideo = 20;
  
        public static int[] castCoinsInvest = new[] { 25, 50, 100 };
        public static int[] timeInGameGift = new[] { 6, 40, 360 };

        [SaveFieldAttribute]
        public static string codeNamesModelsOpens = "";

       // [SaveFieldAttribute]
        public static bool isShowInfoWindowToBonusLevel = true;

        //Show or hide button double gift by watch video
        [SaveFieldAttribute]
        public static bool isShowGiftDoubleFromVideo = true;

        [SaveFieldAttribute]
        public static int giftTimeIndex = 0;

        [SaveFieldAttribute]
        public static float timeInGameSecond = 10;

        [SaveFieldAttribute]
        public static string currentModel = "";

        [SaveFieldAttribute]
        public static float bestScore = 0;

        [SaveFieldAttribute]
        public static int coins = 150;

        [SaveFieldAttribute]
        public static bool appIsRate = false;

        [SaveFieldAttribute]
        public static bool isShowHelpHandToSelectCharacter = true;

        [SaveFieldAttribute]
        public static bool isCointsByWatchAdIsBeenViewed = false;

        public static bool isShowSkipButtonLevel = true;

        public static string[] GetCodeNamesCharactersOpen()
        {
            return codeNamesModelsOpens.Split(',');
        }

        public static bool IsModelBuyed(string codeName)
        {
            string name = codeName.Trim().ToLower();
            return GetCodeNamesCharactersOpen().Any(x => x.Contains(name));
        } 

        public static void AddBuyModel(string codeName)
        {
            string name = codeName.Trim().ToLower();

            List<string> models = new List<string>(GetCodeNamesCharactersOpen());
            if (!models.Any(x => x.Contains(name)))
            {
                models.Add(name);
                codeNamesModelsOpens = string.Join(",", models.ToArray());
                Save();
            }
        }

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

        public static int RemoveCoins(int counRemoveCoins)
        {
            coins -= counRemoveCoins;
            Mathf.Clamp(coins, 0, int.MaxValue);
            Save();
            return coins;
        }

        public static int AddCoins(int countAddCoins)
        {
            coins += countAddCoins;
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


        public static void ReturnQbertLives()
        {
            countLive += 3;
        }


        static GlobalValues()
        {
            Load();
        }

        
    }
}