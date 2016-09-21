using Scripts.GameScene.GameAssets;
using Scripts.GameScene.Characters;
using Scripts.GameScene.MapLoader;
using UnityEngine;

namespace Scripts.InstructionScene
{
    public class Instruction : MonoBehaviour
    {
        public GlobalConfigurationAsset globalSettingAsset;
        public FadeScreen fadeScreen;
        public MapFieldGenerator mapGenerator;
        public Qbert qbert;

        void Start ()
        {
            CreateMap();

            fadeScreen.OnEnd = transform1 =>
            {
                StartInstruction();
            };

            fadeScreen.StartDisable(1.0f);
        }

        public void CreateMap()
        {
            mapGenerator.mapAsset = globalSettingAsset.assetInstruction.globalMap;
            mapGenerator.CreateMap();


            //qbert.SetStartPosition();

        }


        public void StartInstruction()
        {
            
        }
	
        void Update () 
        {
	
        }
    }
}
