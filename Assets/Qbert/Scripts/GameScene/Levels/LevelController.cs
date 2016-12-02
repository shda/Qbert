using System;
using System.Collections;
using System.Linq;
using Assets.Qbert.Scripts.GameScene.Characters;
using Assets.Qbert.Scripts.GameScene.GameAssets;
using Assets.Qbert.Scripts.GameScene.Gui;
using Assets.Qbert.Scripts.GameScene.InputControl;
using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.Levels
{
    public class LevelController : MonoBehaviour
    {
        public InputController inputController;
        public GameplayObjects gameplayObjects;
        public MapField mapField;
        public Characters.Qbert qbert;
        public GameGui gameGui;
        public LevelLogicSwitcher levelLogicSwitcher;
        public GameScene gameScene;

        [HideInInspector]
        public LevelLogic levelLogic;

        public int level;
        public int round;

        public bool isPlay;

        private static float oldTime;
        
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
            isPlay = false;

            GlobalValues.countLive--;
            UpdareCountLives();

            if (GlobalValues.countLive > 0)
            {
                ReturnQbertToPosution();
            }
            else
            {
                GameOver();
            }
        }

        public void ReturnQbertToPosution()
        {
            inputController.isEnable = true;
            Time.timeScale = 1.0f;

            gameplayObjects.DestroyAllEnemies();
            levelLogic.currentRoundConfig.ResetRound();
            qbert.SetStartPosition(qbert.currentPosition);
            DestroyAllEnemies();
            qbert.Run();

            SetPauseGamplayObjects(false);
            UpdareCountLives();

            inputController.ShowButtons();
        }

        public void OnQbertDropDown()
        {
            OnQbertDead();
        }

        public void DestroyAllEnemies()
        {
            gameplayObjects.DestroyAllEnemies();
        }

       // private bool isFirstEndGame = true;

        public void GameOver()
        {
            inputController.isEnable = false;
            SetPauseGamplayObjects(true);
            Time.timeScale = 1.0f;

            GlobalValues.UpdateBestScore();

            gameGui.ShowGameOver();
        }

        public void AddLiveReturnGame()
        {
            GlobalValues.countLive = 3;
            ReturnQbertToPosution();
        }

        public void UpdareCountLives()
        {
            gameGui.UpdateLiveCount();
        }

        public void SetPauseGamplayObjects(bool isPause)
        {
            isPlay = !isPause;

            float timeScale = isPause ? 0.0000001f : 1.0f;
            gameplayObjects.SetTimeScale(timeScale);
            levelLogic.SetTimeScaleGameplayObjects(timeScale);
        }

        public void SetPauseQbert(bool isPause)
        {
            isPlay = !isPause;
            // float timeScale = isPause ? 0.0000001f : 1.0f;
            qbert.SetTimeScaler(isPause?  gameplayObjects  : null);
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
           //     UnityEngine.Debug.Log("OnDead");
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


        public void InitRound(int round)
        {
            InitLevel(level , round);
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
            
        }

        public void InitLevel(int level, int round)
        {
            this.level = level;
            this.round = round;

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
                GlobalValues.currentLevel++;
                GlobalValues.currentRound = 0;

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
            levelLogic = levelLogicSwitcher.InitLevelLoad(GlobalValues.currentLevel);

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

        void Update()
        {
            if (isPlay)
            {
                GlobalValues.timeInGameSecond += Time.unscaledDeltaTime;

                float time = Mathf.Abs(oldTime - GlobalValues.timeInGameSecond);

                if (time > 30.0f)
                {
                    oldTime = GlobalValues.timeInGameSecond;
                    GlobalValues.Save();
                    Debug.Log("Save");
                }
            }
        }
    }
}
