using System.Collections;
using Assets.Qbert.Scripts.GameScene;
using Assets.Qbert.Scripts.GameScene.Levels;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Qbert.Scripts.ShowLevel
{
    public class ShowLevel : MonoBehaviour
    {
        public Text textLevel;
        public GameScene.Characters.Qbert qbert;
        public FadeScreen fadeScreen;
        public LevelController levelController;
        public LoadScene.LoadScene loadSceneAfter;
        public float waitBetweenJump = 0.5f;

        public void SetLevel()
        {
            textLevel.text = "level " + (GlobalSettings.currentLevel + 1);
        }

        void Start ()
        {
            levelController.InitSceneLevelNumber();
            levelController.StartLoadingLevel();

            SetLevel();

            //yield return new WaitForSeconds(0.5f);

            fadeScreen.OnEnd = transform1 =>
            {
                StartCoroutine(Jumper());
            };

            fadeScreen.StartDisable(1.0f);
        }


        public IEnumerator Jumper()
        {
            yield return new WaitForSeconds(0.4f);

            var logic = levelController.levelLogic;
            if (logic.type == LevelLogic.Type.LevelType1 ||
                logic.type == LevelLogic.Type.LevelType2)
            {
                yield return JumpToCube(1, 1);
                yield return JumpToCube(0, 1);
                yield return JumpToCube(1, 2);
                yield return JumpToCube(0, 1);
            }
            else if (logic.type == LevelLogic.Type.LevelType3 ||
                     logic.type == LevelLogic.Type.LevelType4 ||
                     logic.type == LevelLogic.Type.LevelType5)
            {
                yield return JumpToCube(1, 2);
                yield return JumpToCube(0, 1);
                yield return JumpToCube(1, 1);
                yield return JumpToCube(0, 1);

                yield return JumpToCube(1, 2);
                yield return JumpToCube(0, 1);
                yield return JumpToCube(1, 2);
            }

            yield return new WaitForSeconds(0.5f);

            fadeScreen.OnEnd = transform1 =>
            {
                LoadLevel();
            };

            fadeScreen.StartEnable(1.0f);
        }


        public void LoadLevel()
        {
            loadSceneAfter.OnLoadScene();
        }

        IEnumerator JumpToCube(int lint , int  pos)
        {
            bool onPress = false;

            qbert.OnEventPressCube = (character, cube) =>
            {
                onPress = true;
            };

            onPress = false;
            qbert.MoveToCube(new PositionCube(lint , pos));

            while (!onPress)
            {
                yield return null;
            }

            yield return new WaitForSeconds(waitBetweenJump);
        }
	
        void Update () 
        {
	
        }
    }
}
