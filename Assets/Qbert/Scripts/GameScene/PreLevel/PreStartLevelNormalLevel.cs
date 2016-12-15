using System;
using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.PreLevel
{
    public class PreStartLevelNormalLevel : MonoBehaviour
    {
        public PreLevelAnimationGui preLevelAnimationGui;

        public void StartAnimation(Action OnEnd)
        {
            preLevelAnimationGui.StartShowRound(GlobalValues.currentRound + 1, OnEnd);
        }

        public void StartBonusAnimation(Action OnEnd)
        {
            preLevelAnimationGui.StartShowBonus(OnEnd);
        }

        void Start () 
        {
	
        }

        void Update () 
        {
	
        }
    }
}
