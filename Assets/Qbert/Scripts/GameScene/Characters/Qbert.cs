using System;
using System.Collections;
using System.Linq;
using Assets.Qbert.Scripts.GameScene.GameAssets;
using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.Characters
{
    public class Qbert : GameplayObject
    {
        public Transform fallowFront;

        public GlobalConfigurationAsset configuration;

        public Transform boobleDead;
        public Transform rootModel;

        public virtual Type typeObject
        {
            get { return Type.Qbert; }
        }

        public void OnEnemyAttack()
        {
            //return;;
            boobleDead.gameObject.SetActive(true);
            boobleDead.rotation = Quaternion.Euler(0, 0, 0);

            levelController.levelLogic.OnDeadQbert();
        }

        public override void SetStartPosition(PositionCube startPositionQbert)
        {
            base.SetStartPosition(startPositionQbert);
            root.rotation = Quaternion.Euler(0, -135, 0);
        }

        public override bool OnPressCube(Cube cube)
        {
            if (cube.lastState < cube.stateColor && !cube.isSet)
            {
                AddScore(ScorePrice.pressCubeMediumColor);
            }
            else if (cube.lastState < cube.stateColor && cube.isSet)
            {
                AddScore(ScorePrice.pressCubeNeedColor);
            }
            else if (cube.lastState > cube.stateColor && !cube.isSet)
            {
                AddScore(ScorePrice.pressCubeMediumColor);
            }
        
            return base.OnPressCube(cube);
        }

        public void OnCommandMove(DirectionMove.Direction buttonType , Action<Character> OnDownEnd)
        {
            if (!isFrize)
            {
                Cube findCube = levelController.mapField.GetCubeDirection(buttonType, currentPosition);
                if (findCube)
                {
                    MoveToCube(findCube);
                }
                else
                {
                    var pointToJump = levelController.mapField.GetPointCubeDirection(buttonType, currentPosition);
                    var findObject = levelController.gameplayObjects.GetGameplayObjectInPoint(pointToJump);
                    if (findObject && findObject.typeObject == Type.Transport)
                    {
                        MoveToPointAndDropDown(findObject.transform.position);
                    }
                    else if(levelController.levelLogic == null || levelController.levelLogic.isPlayerCanFallOffCube)
                    {
                        Vector3 newPos = root.position + levelController.mapField.GetOffsetDirection(buttonType);
                        MoveToPointAndDropDown(newPos, OnDownEnd);
                    }
                }
            }
        }

        public override void Run()
        {
            boobleDead.gameObject.SetActive(false);
            isFrize = false;
            isCheckColision = true;
            base.Run();
        }


        void Start()
        {
            UpdateModel();
        }

        public void UpdateModel()
        {
            SetModel(GlobalValues.currentModel);
        }

        public void SetModel(string name = "Default")
        {
            Debug.Log(name);

            foreach (var cild in rootModel.Cast<Transform>())
            {
                cild.gameObject.SetActive(false);
                Destroy(cild.gameObject);
            }

            var findModel = configuration.GetModelByName(name);

            if (findModel != null)
            {
                Transform tr = Instantiate(findModel.transform);
                tr.SetParent(rootModel);
                tr.localPosition = Vector3.zero;
                tr.localRotation = Quaternion.Euler(0,0,0);
                tr.localScale = new Vector3(1,1,1);

                var model = tr.GetComponent<QbertModel>();
                boobleDead = model.booldeDead;
                collisionProxy = model.collisionProxy;
                collisionProxy.proxyObject = transform;

                boobleDead.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("Dont find mode.");
            }
        }

        
    }
}
