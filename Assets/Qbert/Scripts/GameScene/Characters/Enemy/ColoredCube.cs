using UnityEngine;

namespace Scripts.GameScene.Characters.Enemy
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

        public override bool OnColisionToQbert(Qbert qbert)
        {
            if (qbert.isCheckColision)
            {
                AddScore(ScorePrice.getColorCube);
                OnStartDestroy();
                return true;
            }

            return true;
        }
    }
}
