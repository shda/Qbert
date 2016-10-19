using Assets.Qbert.Scripts.ControlConfiguratorScripts;
using Assets.Qbert.Scripts.GameScene;
using Assets.Qbert.Scripts.GameScene.AnimationToTime;
using Assets.Qbert.Scripts.GUI.Button;
using Assets.Qbert.Scripts.Utils;
using UnityEngine;

namespace Assets.Qbert.Scripts.GUI.GUISettings
{
    public class GuiSettings : MonoBehaviour
    {
        public GuiHandle guiHandle;
        public LoadScene.SelectSceneLoader selectSceneLoader;
        public FadeScreen fadeScreen;
        public ControlConfigurator controlConfigurator;

        public void ShowControlConfigurator()
        {
            guiHandle.enabled = false;
            controlConfigurator.OnEndConfiguration = configurator =>
            {
                guiHandle.enabled = true;
                controlConfigurator.gameObject.SetActive(false);
            };

            controlConfigurator.Show();
        }

        public void StartGame()
        {
            UnityEngine.Debug.Log("StartGame");

            guiHandle.enabled = false;
            fadeScreen.OnEnd = transform1 =>
            {
                GlobalValues.currentLevel = 0;
                GlobalValues.currentRound = 0;

                selectSceneLoader.OnLoadScene();
            };

            fadeScreen.StartEnable(0.5f);
        }

        void Start()
        {
            fadeScreen.OnEnd = transform1 =>
            {
            
            };

            fadeScreen.StartDisable(1.0f);
        }
    }
}
