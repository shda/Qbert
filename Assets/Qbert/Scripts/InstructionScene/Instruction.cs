using Assets.Qbert.Scripts.GameScene;
using Assets.Qbert.Scripts.GameScene.AnimationToTime;
using Assets.Qbert.Scripts.GameScene.GameAssets;
using Assets.Qbert.Scripts.LoadScene;
using Assets.Qbert.Scripts.Utils;
using UnityEngine;

namespace Assets.Qbert.Scripts.InstructionScene
{
    public class Instruction : MonoBehaviour
    {
        public GlobalConfigurationAsset globalSettingAsset;
        public FadeScreen fadeScreen;
        public MapField mapField;
        public GameScene.Characters.Qbert qbert;
        public InstructionSteps instructionSteps;
        public SelectSceneLoader loadSceneLevel;

        public bool isLock = false;

        public void OnPressSkip()
        {
            if(isLock)
                return;

            isLock = true;

            InstructionEnd();
        }

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
            isLock = true;

            instructionSteps.StopAllCoroutines();

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
                cube.SetColors(colors.GetValue().colors);
                cube.Reset();
            }
        }

        public void CreateMap()
        {
            mapField.mapGenerator.mapAsset = 
                globalSettingAsset.assetInstruction.globalMap.GetValue();
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
