using System.Collections;
using System.Linq;
using Scripts.GameScene.Characters;
using Scripts.GameScene.GameAssets;
using Scripts.GameScene.Gui;
using Scripts.GameScene.InputControl;
using Scripts.Utils;
using UnityEngine;

namespace Scripts.GameScene.Levels
{
    public class LevelController : MonoBehaviour
    {
        public InputController inputController;
        public GameplayObjects gameplayObjects;
        public MapField mapField;
        public Qbert qbert;
        public GameGui gameGui;
        public LevelLogicSwitcher levelLogicSwitcher;
        public GameScene gameScene;

        [HideInInspector]
        public LevelLogic levelLogic;

        public void AddScore(float score)
        {
            if (gameGui)
            {
                gameGui.AddScore(score);
            }
        }

        public void AddCoins(int coint)
        {
            if (gameGui)
            {
                gameGui.AddCoins(coint);
            }
        }

        public void OnQbertDead()
        {
            GlobalSettings.countLive--;
            UpdareCountLives();

            if (GlobalSettings.countLive > 0)
            {
                inputController.isEnable = true;
                Time.timeScale = 1.0f;

                gameplayObjects.DestroyAllEnemies();
                levelLogic.currentRoundConfig.ResetRound();
                qbert.SetStartPosition(qbert.currentPosition);
                DestroyAllEnemies();
                qbert.Run();
            }
            else
            {
                GameOver();
            }
        }

        public void OnQbertDropDown()
        {
            OnQbertDead();
        }


        public void DestroyAllEnemies()
        {
            gameplayObjects.DestroyAllEnemies();
        }


        public void GameOver()
        {
            gameGui.ShowGameOver();
            inputController.isEnable = false;

            gameplayObjects.DestroyAllEnemies();
            qbert.gameObject.SetActive(false);

            levelLogic.currentRoundConfig.Stop();

            Time.timeScale = 1.0f;

            UnscaleTimer.Create(3.0f, timer =>
            {
                gameScene.LoadMainScene();
            });
        }

        public void UpdareCountLives()
        {
            gameGui.UpdateLiveCount();
        }

        public void SetPauseGamplayObjects(bool isPause)
        {
            float timeScale = isPause ? 0.0000001f : 1.0f;
            gameplayObjects.SetTimeScale(timeScale);
            levelLogic.SetTimeScaleGameplayObjects(timeScale);
        }

        public void SetPauseGame(bool isPause)
        {
            Time.timeScale = isPause ? 0.0000001f : 1.0f;
        }

        public MapAsset GetMapAssetFromLevel()
        {
            return levelLogic.GetMapAssetFromCurrentRound();
        }

        private void OnPressCubeEvents(Cube cube, Character character)
        {
            levelLogic.OnCharacterPressToCube(cube , character);
        }

        private void OnCollisionCharacters(Transform owner1, Transform owner2)
        {
            var character1 = owner1.GetComponent<Character>();
            var character2 = owner2.GetComponent<Character>();

            levelLogic.OnCollisionCharacters(character1, character2);
        }

        private void OnPressControl(DirectionMove.Direction buttonType)
        {
            qbert.OnCommandMove(buttonType , character =>
            {
                OnQbertDropDown();
                UnityEngine.Debug.Log("OnDead");
            });
        }

        void ConnectEvents()
        {
            if (inputController != null)
            {
                inputController.OnPress = OnPressControl;
            }
           
            mapField.OnPressCubeEvents = OnPressCubeEvents;
            qbert.collisionProxy.triggerEnterEvent = OnCollisionCharacters;
        }

        public void OnRoundCubesInWin()
        {
            levelLogic.StopLevel();
            DestroyAllEnemies();
            inputController.isEnable = false;

            mapField.FlashGameFiels(() =>
            {
                inputController.isEnable = true;
                NextRound();
            });

            // NextRound();
        }

        public void RestartLevel()
        {
            DestroyAllEnemies();

            if (gameGui != null)
            {
                gameGui.SetLevelNumber(levelLogicSwitcher.currentLevel, levelLogic.roundCurrent);
                gameGui.UpdateLiveCount();
            }

            StopAllCoroutines();
            SetPauseGamplayObjects(false);

            levelLogic.ResetLevel();
            levelLogic.StartLevel(levelLogic.roundCurrent);

            UpdateColorGuiCubeChangeTo();

            qbert.Run();

            SetPauseGamplayObjects(false);
            Time.timeScale = 1.0f;
        }

        public void InitLevel(int level, int round)
        {
            if (gameGui)
                gameGui.SetLevelNumber(level, round);

            levelLogic = levelLogicSwitcher.GetLevelLogic(level, round);
            levelLogic.InitLevel();

            mapField.mapGenerator.mapAsset = GetMapAssetFromLevel();
            mapField.mapGenerator.CreateMap();
            mapField.Init();

        }

        public void UpdateColorGuiCubeChangeTo()
        {
            var first = mapField.mapGenerator.map.FirstOrDefault();
            if (first != null)
            {
                gameGui.SetColorCube(first.colors.Last());
            }
        }

        public void ResetScore()
        {
            if (gameGui != null)
            {
                gameGui.UpdateScore();
            }
        }

        public void NextLevel()
        {
            if (levelLogicSwitcher.IsCanNextLevels())
            {
                GlobalSettings.currentLevel++;
                GlobalSettings.currentRound = 0;

                gameScene.LoadSceneShowLevel();
            }
            else
            {
                gameScene.LoadMainScene();
            }
        }

        public void NextRound()
        {
            levelLogic.NextRound();
        }

        public void EndLevels()
        {
            InitLevel(0,0);
        }

        public void InitSceneLevelNumber()
        {
            levelLogic = levelLogicSwitcher.InitLevelLoad(GlobalSettings.currentLevel);

            levelLogic.InitLevel();

            mapField.mapGenerator.mapAsset = GetMapAssetFromLevel();
            mapField.mapGenerator.CreateMap();

            mapField.Init();
        }

        public void InitNextLevel()
        {
        
        }

        public void StartLoadingLevel()
        {
            ConnectEvents();
            levelLogic.ResetLevel();
        }

        public void StartLevel()
        {
            gameGui.UpdateLiveCount();

            ConnectEvents();
            RestartLevel();
        }

        public void StartPauseGameObjectsToSecond(float time)
        {
            StopAllCoroutines();
            StartCoroutine(TimerPauseGameObjectsToSecond(time));
        }
        private IEnumerator TimerPauseGameObjectsToSecond(float time)
        {
            SetPauseGamplayObjects(true);
            qbert.isCheckColision = false;

            yield return new WaitForSeconds(time);

            SetPauseGamplayObjects(false);
            qbert.isCheckColision = true;
        }

    
    }
}
