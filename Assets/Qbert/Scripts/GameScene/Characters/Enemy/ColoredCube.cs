using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.Characters.Enemy
{
    public class ColoredCube : RedCube 
    {
        public override Type typeObject
        {
            get { return Type.ColoredCube; }
        }

        public override bool OnPressCube(Cube cube)
        {
            int randomPress = Random.Range(0, 2);

            if (randomPress == 1)
            {
                cube.SetNextColor();
            }
            else
            {
                cube.SetLastColor();
            }

            //after press to cube, need check to win
            return false;
        }

        public override bool OnProcessingQbertCollision(Qbert qbert)
        {
            if (qbert.checkCollision == CollisionCheck.All ||
                qbert.checkCollision == CollisionCheck.OnlyBonus)
            {
                AddScore(levelController.globalConfiguraion.scoprePrice.getColorCube);
                OnStartDestroy();
                return true;
            }

            return true;
        }
    }
}
