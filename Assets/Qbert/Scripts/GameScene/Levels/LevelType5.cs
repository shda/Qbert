//Уровень 5 (и до конца игры). Первый прыжок на кубик устанавливает промежуточный цвет.
//Второй прыжок изменяет цвет на нужный.Третий прыжок возвращает кубику начальный цвет.
//Этот цикл повторяется.
namespace Assets.Qbert.Scripts.GameScene.Levels
{
    public class LevelType5 : LevelLogic
    {
        public override LevelLogic.Type type
        {
            get { return LevelLogic.Type.LevelType5; ; }
        }

        public override bool OnQbertPressToCube(Cube cube, Characters.Qbert qbert)
        {
            if (cube.isSet)
            {
                cube.DropColor();
            }
            else
            {
                cube.SetNextColor();
            }
            return false;
        }
    }
}
