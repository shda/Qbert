﻿using Scripts.GameScene.Characters;
using Scripts.GameScene.GameAssets;
using Scripts.Utils;
using UnityEngine;

namespace Scripts.GameScene.Levels
{
    public abstract class LevelLogic : MonoBehaviour
    {
        public enum Type
        {
            LevelType1,
            LevelType2,
            LevelType3,
            LevelType4,
            LevelType5,
        }

        public virtual Type type
        {
            get { return Type.LevelType1; ; }
        }

        public LevelConfigAsset configurationAsset;
        public int roundCurrent;
        public Round[] rounds
        {
            get { return configurationAsset.rounds; }
        }
        public Round currentRoundConfig
        {
            get { return rounds[roundCurrent]; }
        }

        [Header("Цвета по умолчанию")]
        public Color[] globalLevelColors;

        private float currentTime;
        private bool isLevelRun = false;

        protected LevelController levelController;

        public MapAsset GetMapAssetFromCurrentRound()
        {
            if (currentRoundConfig.customMap != null)
            {
                return currentRoundConfig.customMap;
            }

            return configurationAsset.globalMap;
        }

        public virtual void InitLevel()
        {
       
        }
        public virtual void NextRound()
        {
            if (roundCurrent < rounds.Length - 1)
            {
                roundCurrent++;
                levelController.RestartLevel();
            }
            else
            {
                levelController.NextLevel();
            }
        }

        public virtual void OnCharacterPressToCube(Cube cube, Character character)
        {
            if (character is Characters.Qbert)
            {
                if ( OnQbertPressToCube( cube , (Characters.Qbert) character) )
                {
                    return;
                }
            }

            if (!character.OnPressCube(cube))
            {
                if (CheckToWin())
                {
                    levelController.OnRoundCubesInWin();
                }
            }
        }
        public virtual bool OnQbertPressToCube(Cube cube , Characters.Qbert qbert)
        {
            return false;
        }

        public virtual void ResetLevel()
        {
            currentRoundConfig.ResetRound();

            Cube cubeQbertStart = levelController.mapField.mapGenerator.GetCubeStartByType(Character.Type.Qbert);

            levelController.qbert.StopAllCoroutines();
            levelController.qbert.levelController = levelController;
            levelController.qbert.SetStartPosition(cubeQbertStart.currentPosition);

            foreach (var cube in levelController.mapField.field)
            {
                if (currentRoundConfig.customColors != null && currentRoundConfig.customColors.Length > 0)
                {
                    cube.SetColors(currentRoundConfig.customColors );
                }
                else if (configurationAsset.globalLevelColors != null)
                {
                    cube.SetColors(configurationAsset.globalLevelColors);
                }
                else
                {
                    cube.SetColors(globalLevelColors);
                }

                cube.Reset();
            }

            levelController.DestroyAllEnemies();
        }

        public virtual bool CheckToWin()
        {
            foreach (var cube in levelController.mapField.field)
            {
                if (!cube.isSet)
                {
                    return false;
                }
            }

            return true;
        }
        public virtual void OnCollisionCharacters(Character character1, Character character2)
        {
            if (character1 is GameplayObject && character2 is Characters.Qbert)
            {
                OnCollisionQbertToGameplayObject((GameplayObject) character1 , (Characters.Qbert) character2);
            }
        }
        public virtual void SetTimeScaleGameplayObjects(float scale)
        {
            currentRoundConfig.timeScale = scale;
        }
        public void StartRound(int round)
        {
            roundCurrent = round;
            currentRoundConfig.Init(levelController);
            currentRoundConfig.Run();
        }
        public virtual void OnCollisionQbertToGameplayObject(GameplayObject gameplayObject , Characters.Qbert qbert)
        {
            if (!gameplayObject.OnColisionToQbert(qbert))
            {
                qbert.OnEnemyAttack();
            }
        }
        public virtual void StartLevel(int round)
        {
            levelController.gameplayObjects.DestroyAllEnemies();
            isLevelRun = true;
            StartRound(round);
        }

        public void StopLevel()
        {
            isLevelRun = false;
        }

        void Update()
        {
            if (isLevelRun)
            {
                currentRoundConfig.Update();
            }
        }
        public void SetController(LevelController controller)
        {
            levelController = controller;
        }
        public void SetRound(int round)
        {
            roundCurrent = round;
        }

        public void OnDeadQbert()
        {
            levelController.inputController.isEnable = false;
            float oldScale = Time.timeScale;
            Time.timeScale = 0.0000001f;
            UnscaleTimer.Create(2.0f, timer =>
            {
                levelController.OnQbertDead();

            
            });
        }
    }
}