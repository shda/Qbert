using Assets.Qbert.Scripts.GameScene;
using Assets.Qbert.Scripts.GameScene.GameAssets;
using Assets.Qbert.Scripts.GameScene.MapLoader;
using UnityEngine;

namespace Assets.Qbert.Scripts.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        public GameFieldGenerator gameFieldGenerator;
        public GameScene.Characters.Qbert qbert;
        public MapAsset mapLevelLoad;
        public Game game;

        void Start () 
        {
            gameFieldGenerator.mapAsset = mapLevelLoad;
            gameFieldGenerator.CreateMap();

            game.StartGame();
        }
	
        void Update () 
        {
	
        }
    }
}
