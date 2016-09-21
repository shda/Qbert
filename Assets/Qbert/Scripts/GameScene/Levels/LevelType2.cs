//Уровень 2. Первый прыжок на кубик устанавливает промежуточный цвет.
//Повторный прыжок изменяет цвет на нужный.Последующие прыжки не изменяют цвет.
namespace Assets.Qbert.Scripts.GameScene.Levels
{
    public class LevelType2 : LevelLogic
    {
        public override LevelLogic.Type type
        {
            get { return LevelLogic.Type.LevelType2; ; }
        }

        public override bool OnQbertPressToCube(Cube cube, Characters.Qbert qbert)
        {
            cube.SetNextColor();
            return false;
        }
    }
}
