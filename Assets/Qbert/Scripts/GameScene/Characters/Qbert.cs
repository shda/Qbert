﻿using System;
using System.Collections;
using System.Linq;
using Assets.Qbert.Scripts.GameScene.GameAssets;
using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.Characters
{
    public class Qbert : GameplayObject
    {
        public GlobalConfigurationAsset configuration;

        public Transform boobleDead;
        public Transform rootModel;

        private QbertModel model;

        private Animator rootModelAnimator;

        public int cointMultiplier
        {
            get
            {
                if (model != null)
                {
                    return model.coinsMultiplier;
                }

                return 1;
            }
        }

        public override Type typeObject
        {
            get { return Type.Qbert; }
        }

        public void OnEnemyAttack()
        {
            model.SetTextBooble(levelController.globalConfiguraion.GetEmoticon());
            boobleDead.gameObject.SetActive(true);
            boobleDead.rotation = Quaternion.Euler(0, 0, 0);

            levelController.levelLogic.OnDeadQbert();
        }

        public void SetPauseAnimation(bool isPause)
        {
            rootModelAnimator.enabled = isPause;
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
                AddScore(levelController.globalConfiguraion.scoprePrice.pressCubeMediumColor);
            }
            else if (cube.lastState < cube.stateColor && cube.isSet)
            {
                AddScore(levelController.globalConfiguraion.scoprePrice.pressCubeNeedColor);
            }
            else if (cube.lastState > cube.stateColor && !cube.isSet)
            {
                AddScore(levelController.globalConfiguraion.scoprePrice.pressCubeMediumColor);
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
            InitInter();
            boobleDead.gameObject.SetActive(false);
            isFrize = false;
            checkCollision = CollisionCheck.All;
            base.Run();
        }


        void Start()
        {
            UpdateModel();
        }

        public void UpdateModel()
        {
            SetModel(GlobalValues.currentModel);
            rootModelAnimator = rootModel.GetComponent<Animator>();
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

                model = tr.GetComponent<QbertModel>();
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

        private void InitInter()
        {
            var config = levelController.globalConfiguraion.qubertConfiguration;

            timeMove        = config.timeMove;
            timeRotate      = config.timeRotate;
            jumpAmplitude   = config.jumpAmplitude;
            timeDropDown    = config.timeDropDown;
            dropDownHeight  = config.dropDownHeight;
            pauseAfterMove  = config.pauseAfterMove;
        }

    }
}
