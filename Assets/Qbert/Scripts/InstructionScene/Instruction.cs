using Assets.Qbert.Scripts.GameScene.MapLoader;
using UnityEngine;

namespace Assets.Qbert.Scripts.InstructionScene
{
    public class Instruction : MonoBehaviour 
    {
        public FadeScreen fadeScreen;
        public MapFieldGenerator map;

        void Start () 
        {
            fadeScreen.OnEnd = transform1 =>
            {
                StartInstruction();
            };

            fadeScreen.StartDisable(1.0f);
        }

        public void CreateMap()
        {
            
        }


        public void StartInstruction()
        {
            
        }
	
        void Update () 
        {
	
        }
    }
}
