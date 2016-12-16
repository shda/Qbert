using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.GameAssets
{
    public class CyclicLevelsAsset : ScriptableObject
    {
        [System.Serializable]
        public class StepCyclic
        {
            public bool isCyclic;
            public float startSpeed;
            public float upSpeedAfterCyclic;

            public LevelConfigAsset[] levelsAssets;
        }

        [Header("Bonus")]
        public float timeStart;
        public float timeStepAllLevels;
        public MapsAsset bonusMapsAsset;

        [Header("Cyclic levels")]
        public StepCyclic[] steps;

        public bool GetStepCyclicByLevel(int level ,
            ref float timeScale, 
            out StepCyclic findStepCyclic ,
            out LevelConfigAsset levelConfigAsset)
        {
            if (steps != null && steps.Length > 0)
            {
                int offset = 0;

                foreach (var stepCyclic in steps)
                {
                    int count = stepCyclic.levelsAssets.Length;

                    int index = level - offset;

                    if (count > level - offset)
                    {
                        findStepCyclic = stepCyclic;
                        levelConfigAsset = stepCyclic.levelsAssets[level - offset];
                        timeScale = findStepCyclic.startSpeed;

                        return true;
                    }
                    else if(stepCyclic.isCyclic)
                    {
                        int timeMultiplier = index / count;
                        int offsetIndex = index - (count*timeMultiplier);

                        findStepCyclic = stepCyclic;
                        levelConfigAsset = stepCyclic.levelsAssets[offsetIndex];

                        timeScale = findStepCyclic.startSpeed + 
                                    findStepCyclic.upSpeedAfterCyclic * timeMultiplier;

                        return true;
                    }

                    offset += count;
                }
            }

            findStepCyclic = null;
            levelConfigAsset = null;

            return false;
        }

        public MapAsset GetBonusMapByLevel(int level)
        {
            return bonusMapsAsset.GetValue();
        }

        public float GetTimeScaleToBonusMapByLevel(int level)
        {
            return timeStart + (level - 1)*timeStepAllLevels;
        }
    }
}
