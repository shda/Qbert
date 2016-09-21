using System;
using Scripts.GameScene;
using Scripts.GameScene.GameAssets;
using Scripts.GameScene.Characters;
using Scripts.GameScene.MapLoader;
using Scripts.LoadScene;
using UnityEngine;

namespace Scripts.InstructionScene
{
    public class Instruction : MonoBehaviour
    {
        public GlobalConfigurationAsset globalSettingAsset;
        public FadeScreen fadeScreen;
        public MapField mapField;
        public Qbert qbert;

        public InstructionSteps instructionSteps;

        public SelectSceneLoader loadSceneLevel;


        void Start ()
        {
            CreateMap();
            InitQbert();

            fadeScreen.OnEnd = transform1 =>
            {
                StartInstruction();
            };

            fadeScreen.StartDisable(1.0f);

        }

        public void InstructionEnd()
        {
            fadeScreen.OnEnd = transform1 =>
            {
                loadSceneLevel.OnLoadScene();
            };

            fadeScreen.StartEnable(1.0f);
        }

        private void InitQbert()
        {
            qbert.SetStartPosition(new PositionCube(1, 0));

            mapField.OnPressCubeEvents = (cube, character) =>
            {
                character.OnPressCube(cube);
                cube.SetNextColor();
            };
        }


        public void SetColorCubesDefault()
        {
            var colors = globalSettingAsset.assetInstruction.globalLevelColors;

            foreach (var cube in mapField.field)
            {
                cube.SetColors(colors);
                cube.Reset();
            }
        }

        public void CreateMap()
        {
            mapField.mapGenerator.mapAsset = globalSettingAsset.assetInstruction.globalMap;
            mapField.mapGenerator.CreateMap();
            mapField.Init();

            SetColorCubesDefault();
        }


        public void StartInstruction()
        {
            instructionSteps.RunInstruction();
        }
	
        void Update () 
        {
	
        }
    }
}
