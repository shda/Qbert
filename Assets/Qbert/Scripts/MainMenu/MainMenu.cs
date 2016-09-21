using Assets.Qbert.Scripts.GameScene;
using Assets.Qbert.Scripts.GameScene.GameAssets;
using Assets.Qbert.Scripts.GameScene.MapLoader;
using UnityEngine;

namespace Assets.Qbert.Scripts.MainMenu
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
