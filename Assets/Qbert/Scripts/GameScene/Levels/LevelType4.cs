//Уровень 4. Первый прыжок на кубик устанавливает промежуточный цвет. 
//Повторный прыжок изменяет цвет на нужный. Однако третий прыжок возвращает 
//кубику промежуточный цвет. Каждый последующий прыжок переключает цвет кубика между промежуточным 
//и нужным.
namespace Assets.Qbert.Scripts.GameScene.Levels
{
    public class LevelType4 : LevelLogic
    {
        public override LevelLogic.Type type
        {
            get { return LevelLogic.Type.LevelType4; ; }
        }

        public override bool OnQbertPressToCube(Cube cube, Characters.Qbert qbert)
        {
            if (cube.isSet)
            {
                cube.SetLastColor();
            }
            else
            {
                cube.SetNextColor();
            }

            return false;
        }
    }
}
