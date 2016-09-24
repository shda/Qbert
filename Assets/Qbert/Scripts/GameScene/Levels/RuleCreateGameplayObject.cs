using Assets.Qbert.Scripts.GameScene.Characters;
using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.Levels
{
    [System.Serializable]
    public class RuleCreateGameplayObject
    {
        private ITimeScale timeScale;
        [Header("Тип объекта")]
        public GameplayObject.Type createType;
        [Header("Максимум за весь раунд(-1 без лимита)")]
        public int maxToRound;
        [Header("Максимум одновременно")]
        public int maxOneTime;
        [Header("Задержка появления после старта")]
        public float delayToStart;
        [Header("Задержка между появлениями")]
        public float delayBetweenCreate;
        public float oldTimeCreateObject { get; set; }
        public float counterCreatedObjects { get; set; }

        private bool isFirstStart = true;

        public void SetTimeScale(ITimeScale timescale)
        {
            this.timeScale = timescale;
        }
        public void Reset()
        {
            oldTimeCreateObject = 0;
            counterCreatedObjects = 0;
            isFirstStart = true;
        }

        public void CheckCreateObject(Round round)
        {
            int countInScene = round.GetCountObjectToScene(createType);

            if (countInScene < maxOneTime)
            {
                if (round.timeToStartRound > delayToStart)
                {
                    if (round.timeToStartRound > oldTimeCreateObject + delayBetweenCreate || isFirstStart)
                    {
                        if (countInScene < maxOneTime && CheckMaxToRound(round))
                        {
                            if (CreateObject(round))
                            {
                                isFirstStart = false;
                            }
                        }
                    }
                }
            }
            else
            {
                oldTimeCreateObject = round.timeToStartRound;
            }
        }

        private bool CheckMaxToRound(Round round)
        {
            return counterCreatedObjects < maxToRound || maxToRound == -1;
        }

        private bool CreateObject(Round round)
        {
            var addObj = round.levelController.gameplayObjects.AddGameplayObjectToGame(createType);

            if (addObj != null)
            {
                counterCreatedObjects++;
                oldTimeCreateObject = round.timeToStartRound;

                return true;
            }

            return false;
        }
    }
}
