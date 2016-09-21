using Scripts.GameScene;
using Scripts.GameScene.GameAssets;
using Scripts.GameScene.MapLoader;
using UnityEngine;

namespace Scripts.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        public MapFieldGenerator MapFieldGenerator;
        public GameScene.Characters.Qbert qbert;
        public MapAsset mapLevelLoad;
        public GameScene.GameScene GameScene;

        void Start () 
        {
            MapFieldGenerator.mapAsset = mapLevelLoad;
            MapFieldGenerator.CreateMap();

            GameScene.StartGame();
        }
	
        void Update () 
        {
	
        }
    }
}
