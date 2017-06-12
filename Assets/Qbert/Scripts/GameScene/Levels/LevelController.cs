using System;
using System.Collections;
using System.Linq;
using Assets.Qbert.Scripts.GameScene.Characters;
using Assets.Qbert.Scripts.GameScene.GameAssets;
using Assets.Qbert.Scripts.GameScene.Gui;
using Assets.Qbert.Scripts.GameScene.InputControl;
using Assets.Qbert.Scripts.GameScene.Sound;
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
        public CameraFallowToCharacter cameraFallowToCharacter;
        public GlobalConfigurationAsset globalConfiguraion;
        public FlashBackground flashBackground;

        public bool isCheckToWin = true;

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

            SetPauseGamplayObjects(false);
            SetPauseQbert(false);
            gameGui.SetEnableButtonPause(true);

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

        public void GameOver()
        {
            inputController.isEnable = false;
            SetPauseGamplayObjects(true);

            GameSound.PlayLevelLose();
            //Time.timeScale = 1.0f;

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
            qbert.SetTimeScaler(isPause?  gameplayObjects  : null);
            qbert.SetPauseAnimation(!isPause);
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
            levelLogic.currentRoundConfig.Run();
            qbert.OnCommandMove(buttonType , character =>
            {
                OnQbertDropDown();
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
            if(!isCheckToWin)
                return;;

            levelLogic.StopLevel();
            DestroyAllEnemies();
            inputController.isEnable = false;

            cameraFallowToCharacter.CameraBackSizeAndToCenterMap();

            GameSound.PlayWin();

            mapField.FlashGameFiels(() =>
            {
                inputController.isEnable = true;
                NextRound();
            });
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
                gameGui.SetLevelNumber(levelLogicSwitcher.currentLevel, GlobalValues.currentRound);
                gameGui.UpdateLiveCount();
            }

            StopAllCoroutines();
            SetPauseGamplayObjects(false);

            levelLogic.ResetLevel();
            levelLogic.StartLevel(GlobalValues.currentRound);

            UpdateColorGuiCubeChangeTo();

            qbert.Run();

            SetPauseGamplayObjects(false);
            
        }

        public void InitBonusLevel()
        {
            levelLogic = levelLogicSwitcher.GetBonusLogic();
            levelLogic.InitLevel();

            mapField.mapGenerator.mapAsset = GetMapAssetFromLevel();
            mapField.mapGenerator.CreateMap();
            mapField.Init();
        }

        public void InitLevel(int level, int round)
        {
            this.level = level;
            this.round = round;

            if (gameGui)
            {
                gameGui.SetLevelNumber(level, round);
                gameGui.UpdateScore();
            }
                

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

                PlayBonusLevel(GlobalValues.currentLevel);

                //
            }
            else
            {
                gameScene.LoadMainScene();
            }
        }


        public void PlayBonusLevel(int level)
        {
            GlobalValues.isBonusLevel = true;
            ReloadScene();
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
            qbert.checkCollision = Character.CollisionCheck.OnlyBonus;

            yield return new WaitForSeconds(time);

            SetPauseGamplayObjects(false);
            qbert.checkCollision = Character.CollisionCheck.All;
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

        public void ReloadScene()
        {
            gameScene.ReloadScene();
        }
    }
}
