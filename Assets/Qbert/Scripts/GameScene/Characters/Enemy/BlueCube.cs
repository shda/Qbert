using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.Characters.Enemy
{
    public class BlueCube : RedCube
    {
        [Header("BlueCube")] public float durationFreezingTime = 2.0f;

        public override Type typeObject
        {
            get { return Type.BlueCube; }
        }

        public override bool OnProcessingQbertCollision(Qbert qbert)
        {
            if (qbert.checkCollision == CollisionCheck.All ||
                qbert.checkCollision == CollisionCheck.OnlyBonus)
            {
                AddScore(levelController.globalConfiguraion.scoprePrice.getClueCube);
                levelController.StartPauseGameObjectsToSecond(durationFreezingTime);
                OnStartDestroy();
            }

            return true;
        }
    }
}


