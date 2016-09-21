//Уровень 1.Необходимо прыгнуть на кубик один раз, чтобы изменить цвет
//на нужный.Последующие прыжки не изменяют цвет.
namespace Assets.Qbert.Scripts.GameScene.Levels
{
    public class LevelType1 : LevelLogic
    {
        public override LevelLogic.Type type
        {
            get { return LevelLogic.Type.LevelType1; ; }
        }

        public override bool OnQbertPressToCube(Cube cube, Characters.Qbert qbert)
        {
            cube.SetNextColor();
            return false;
        }
    }
}
