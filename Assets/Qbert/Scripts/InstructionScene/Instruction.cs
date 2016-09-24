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

        public AnimationToTimeChangeCanvasGroup showButtonSkip;

        public bool isLock = false;

        public void OnPressSkip()
        {
            InstructionEnd();
        }

        public void TimerShowButtonSkip()
        {
            if(isLock)
                return;

            isLock = true;

            showButtonSkip.gameObject.SetActive(false);

            UnscaleTimer.Create(5.0f, timer =>
            {
                showButtonSkip.gameObject.SetActive(true);
                StartCoroutine(showButtonSkip.PlayToTime(0.5f));
            });
        }


        void Start ()
        {
            CreateMap();
            InitQbert();

            TimerShowButtonSkip();

            fadeScreen.OnEnd = transform1 =>
            {
                StartInstruction();
            };

            fadeScreen.StartDisable(1.0f);

        }

        public void InstructionEnd()
        {
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
