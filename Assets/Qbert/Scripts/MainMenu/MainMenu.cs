using Assets.Qbert.Scripts.GameScene.GameAssets;
using Assets.Qbert.Scripts.GameScene.MapLoader;
using UnityEngine;

namespace Assets.Qbert.Scripts.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        public MapFieldGenerator mapFieldGenerator;
        public GameScene.Characters.Qbert qbert;
        public MapAsset mapLevelLoad;
        public GameScene.GameScene GameScene;

        void Start () 
        {
            mapFieldGenerator.mapAsset = mapLevelLoad;
            mapFieldGenerator.CreateMap();

            GameScene.StartGame();
        }
	
        void Update () 
        {
	
        }
    }
}
