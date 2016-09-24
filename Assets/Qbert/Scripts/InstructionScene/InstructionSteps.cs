using System.Collections;
using System.Collections.Generic;
using Assets.Qbert.Scripts.GameScene;
using Assets.Qbert.Scripts.GameScene.Characters;
using Assets.Qbert.Scripts.GameScene.Characters.Bonuses;
using Assets.Qbert.Scripts.GameScene.Characters.Enemy;
using Assets.Qbert.Scripts.GameScene.Levels;
using UnityEngine;

namespace Assets.Qbert.Scripts.InstructionScene
{
    public class InstructionSteps : MonoBehaviour
    {
        //Map
        // (1,0)
        //         (3,1)(4,1)
        //       (3,2)(4,2)(5,2)

        public Instruction instruction;
        public GameScene.Characters.Qbert qbert;
        public float waitBetweenJump = 0.3f;
        public LevelController levelController;
        public StepsHeadersText stepsHeadeLabels;

        public List<GameplayObject> listGameplayObjects = new List<GameplayObject>();

        public void SetStepHeader(int step)
        {
            stepsHeadeLabels.SetStepHeader(step);
        }
        public void RunInstruction()
        {
            StartCoroutine(Steps());
        }

        public void SetQbertPosition(int x , int y)
        {
            qbert.SetStartPosition(new PositionCube(x,y));
        }

        IEnumerator JumpToCube(int x, int y)
        {
            bool onPress = false;

            qbert.OnEventPressCube = (character, cube) =>
            {
                onPress = true;
            };

            onPress = false;
            qbert.MoveToCube(new PositionCube(x, y));

            while (!onPress)
            {
                yield return null;
            }

            yield return new WaitForSeconds(waitBetweenJump);
        }

        private IEnumerator Steps()
        {
            yield return StartCoroutine(Step1());
            yield return StartCoroutine(Step2());
            yield return StartCoroutine(Step3());
            yield return StartCoroutine(Step4());
            yield return StartCoroutine(Step5());

            yield return new WaitForSeconds(3.0f);

            instruction.InstructionEnd();
        }


        private IEnumerator Step1()
        {
            instruction.SetColorCubesDefault();
            SetQbertPosition(3,2);

            SetStepHeader(1);

            yield return new WaitForSeconds(2.0f);

            yield return StartCoroutine(JumpToCube(3, 1));
            yield return StartCoroutine(JumpToCube(4, 2));
            yield return StartCoroutine(JumpToCube(4, 1));
            yield return StartCoroutine(JumpToCube(5, 2));

            yield return new WaitForSeconds(1.0f);
        }

        private IEnumerator Step2()
        {
            //instruction.SetColorCubesDefault();
            //SetQbertPosition(5, 2);

            SetStepHeader(2);

            yield return new WaitForSeconds(1.0f);

            yield return StartCoroutine(JumpToCube(4, 1));
            yield return StartCoroutine(JumpToCube(4, 2));
            yield return StartCoroutine(JumpToCube(3, 1));
            yield return StartCoroutine(JumpToCube(3, 2));

            bool wait = true;

            qbert.OnCommandMove(DirectionMove.Direction.DownLeft, character =>
            {
                wait = false;
            });

            while (wait)
            {
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(1.0f);
        }


        private IEnumerator Step3()
        {
            SetStepHeader(3);

            yield return new WaitForSeconds(1.0f);

            instruction.SetColorCubesDefault();
            SetQbertPosition(1, 0);

            CreateObjectToPoint(Character.Type.RedCube, 3, 1);
            CreateObjectToPoint(Character.Type.PurpleCube, 4, 1);
            CreateObjectToPoint(Character.Type.PinkCube, 4, 2);

            var gObject = CreateObjectToPoint(Character.Type.PurpleCube, 3, 2);

            yield return new WaitForSeconds(0.5f);

            var purple = (PurpleCube)gObject;
            yield return StartCoroutine(purple.RebornToEnemy());

            yield return new WaitForSeconds(2.0f);
            DestroyOldObjects();
        }

        private IEnumerator Step4()
        {
            SetStepHeader(4);

            yield return new WaitForSeconds(1.0f);

            instruction.SetColorCubesDefault();
            SetQbertPosition(1, 0);

            yield return null;

            CreateObjectToPoint(Character.Type.BlueCube, 3, 1);
            CreateObjectToPoint(Character.Type.CoinCube, 4, 1);
            CreateObjectToPoint(Character.Type.ColoredCube, 4, 2);

            yield return new WaitForSeconds(3.0f);

            DestroyOldObjects();
        }

        private IEnumerator Step5()
        {
            SetStepHeader(5);

            yield return new WaitForSeconds(1.0f);
            
            instruction.SetColorCubesDefault();
            SetQbertPosition(4, 1);
 
            var field = levelController.mapField.mapGenerator.
                GetPointOutsideFieldToPosition(new PositionCube(2,1));

            var transport = CreateObjectToPoint(Character.Type.Transport,
                field.currentPoint.x , field.currentPoint.y);
            transport.Init();

            var gObject = CreateObjectToPoint(Character.Type.PurpleCube, 5, 2);
            gObject.jumpAnimationToTime = null;
            var purple = (PurpleCube)gObject;
            yield return StartCoroutine(purple.RebornToEnemy());

            yield return new WaitForSeconds(1.0f);


            StartCoroutine(FallowToQubert(gObject));
            yield return StartCoroutine(JumpToCube(4, 2));
            yield return StartCoroutine(JumpToCube(3, 1));
            yield return StartCoroutine(JumpToCube(3, 2));
            yield return StartCoroutine(qbert.MoveToPointAnimation(transport.transform.position));

            var tr = (Transport) transport;
            StartCoroutine(tr.MoveTransport(qbert));
            yield return new WaitForSeconds(1.0f);
        }


        IEnumerator FallowToQubert(GameplayObject go)
        {
            var purple = (PurpleCube)go;

            float timeWait = 0.5f;

            yield return StartCoroutine(JumpAndWait(go, 4, 1, timeWait));
            yield return StartCoroutine(JumpAndWait(go, 4, 2, timeWait));
            yield return StartCoroutine(JumpAndWait(go, 3, 1, timeWait));
            yield return StartCoroutine(JumpAndWait(go, 3, 2, timeWait));

            purple.dropDownHeight *= 2.0f;

            purple.Drop(purple.currentPosition, new PositionCube(2, 1));
            yield return new WaitForSeconds(2.0f);
            PoolGameplayObjects.ReturnObject(purple);
        }

        IEnumerator JumpAndWait(GameplayObject go , int x, int y , float wait)
        {
            yield return StartCoroutine(MoveToCube(go, x, y));
            yield return new WaitForSeconds(wait);
        }

        IEnumerator MoveToCube(GameplayObject go , int x , int y)
        {
            var cube = levelController.mapField.GetCube(new PositionCube(x, y));
            yield return StartCoroutine(go.MoveToCubeAnimation(cube));
        }


        private void DestroyOldObjects()
        {
            foreach (var gameplayObject in listGameplayObjects)
            {
                gameplayObject.gameObject.SetActive(false);
                Destroy(gameplayObject.gameObject);
            }

            listGameplayObjects.Clear();
        }

        private GameplayObject CreateObjectToPoint(GameplayObject.Type type , int x , int y)
        {
            var gameplayObject =  PoolGameplayObjects.GetGameplayObject(type);
            gameplayObject.levelController = levelController;
            gameplayObject.SetStartPosition(new PositionCube(x , y));
            gameplayObject.gameObject.SetActive(true);

            if (type != Character.Type.PinkCube)
            {
                gameplayObject.transform.rotation *= Quaternion.Euler(0, 45, 0);
            } 
            else
            {
                gameplayObject.transform.localRotation *= Quaternion.Euler(0, 0, 180);
            }

            listGameplayObjects.Add(gameplayObject);

            return gameplayObject;
        }


        void Awake()
        {
           stepsHeadeLabels.HideAllHeader();
        }

    }
}


