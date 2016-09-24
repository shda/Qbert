using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Qbert.Scripts.LoadScene
{
    [ExecuteInEditMode]
    public class SelectSceneLoader : MonoBehaviour
    {
        public string sceneName;
        // public bool clearScensSteck = false;

        [HideInInspector]
        public string[] scenes;
    
        public void OnLoadScene()
        {
            SceneManager.LoadScene(sceneName);
        }

        void Start ()
        {
#if UNITY_EDITOR
            ReadScenes();
#endif

            Button button = GetComponent<Button>();
            if (button)
            {
                button.onClick.AddListener(Call);
            }
        }

        private void Call()
        {
            OnLoadScene();
        }


#if UNITY_EDITOR
        private static string[] ReadNamesOfScenes()
        {
            List<string> temp = new List<string>();
            foreach (UnityEditor.EditorBuildSettingsScene scene in UnityEditor.EditorBuildSettings.scenes)
            {
                if (scene.enabled)
                {
                    string name = scene.path.Substring(scene.path.LastIndexOf('/') + 1);
                    name = name.Substring(0, name.Length - 6);
                    temp.Add(name);
                }
            }
            return temp.ToArray();
        }

        public void ReadScenes()
        {
            scenes = ReadNamesOfScenes();
        }
#endif


        void Update ()
        {

        }
    }
}
