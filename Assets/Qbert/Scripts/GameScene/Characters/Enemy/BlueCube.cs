using UnityEngine;

namespace Scripts.GameScene.Characters.Enemy
{
    public class BlueCube : RedCube
    {
        [Header("BlueCube")] public float timeStopDuration = 2.0f;

        public override Type typeObject
        {
            get { return Type.BlueCube; }
        }

        public override bool OnColisionToQbert(Qbert qbert)
        {
            if (qbert.isCheckColision)
            {
                AddScore(ScorePrice.getClueCube);

                levelController.StartPauseGameObjectsToSecond(timeStopDuration);
                OnStartDestroy();
                return true;
            }

            return true;
        }
    }
}


