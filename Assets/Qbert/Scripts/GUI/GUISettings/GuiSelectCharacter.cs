using System;
using System.Collections;
using System.Linq;
using Assets.Qbert.Scripts.GameScene;
using Assets.Qbert.Scripts.GameScene.Characters;
using Assets.Qbert.Scripts.GameScene.GameAssets;
using Assets.Qbert.Scripts.GestureRecognizerScripts;
using Assets.Qbert.Scripts.Utils;
using UnityEngine;

namespace Assets.Qbert.Scripts.GUI.GUISettings
{
    public class GuiSelectCharacter : MonoBehaviour
    {
        public GlobalConfigurationAsset globalConfigurationAsset;
        public Transform rootModels;
        public TextMesh nameCurrentCharacter;
        public TextMesh descriptionCurrentCharacter;
        public CameraMenuController cameraMenuController;
        public GuiMainMenu rootMenu;
        public DiagonalSwipe diagonalSwipe;
        public GameObject swipeHelpShow;

        public float scaleModelNoFocus = 0.8f;
        public float modelsOffset;

        private QbertModel[] models;
        private QbertModel currentModel;
        private Coroutine scrollInWork;

        public void OnFocusCameraToThis()
        {
            diagonalSwipe.gameObject.SetActive(true);
            cameraMenuController.OnCameraMoveSelectCharacter();

            ShowHalpHand(false);

            UnscaleTimer.StartDelay(1.0f, timer =>
            {
                ShowHalpHand(GlobalValues.isShowHelpHandToSelectCharacter);
            });
        }


        public void OnCloseButtonPress()
        {
            cameraMenuController.OnCamaraMoveToRootMenu();
            ShowHalpHand(false);
        }

        private void ShowHalpHand(bool isShow)
        {
            swipeHelpShow.gameObject.SetActive(isShow);
        }

        public void OnButtonCharacterSelect()
        {
            Debug.Log("Select character: " + currentModel.nameCharacter);

            GlobalValues.currentModel = currentModel.nameCharacter;
            GlobalValues.Save();

            diagonalSwipe.gameObject.SetActive(false);
            rootMenu.OnFocusCameraToThis();
        }

        public void InitScene()
        {
            var characters = globalConfigurationAsset.characters;
            models = new QbertModel[characters.Length];

            for (int i = 0; i < characters.Length; i++)
            {
                var createCharacter = Instantiate(characters[i]);
                models[i] = createCharacter;

                var tr = createCharacter.transform;
                tr.SetParent(rootModels);
                tr.localRotation = Quaternion.Euler(0,0,0);
                tr.localPosition = new Vector3(i * modelsOffset , 0, 0);
                tr.localScale = new Vector3(scaleModelNoFocus, scaleModelNoFocus, scaleModelNoFocus);

                createCharacter.booldeDead.gameObject.SetActive(false);
            }

            currentModel = models[0];
            currentModel.transform.localScale = new Vector3(
                1, 1, 1);

            UpdateInfoFromModel(currentModel);
        }

        public void Scroll(bool isLeft)
        {
            int index = Array.IndexOf(models ,currentModel);

            if (isLeft && index >= models.Length - 1)
            {
                return;
            }

            if (!isLeft && index <= 0)
            {
                return;
            }

            QbertModel first = currentModel;
            QbertModel second = isLeft ? models[index + 1] : models[index - 1];
            ScrollToModel(first , second , 0.3f);
            currentModel = second;
            UpdateInfoFromModel(currentModel);

            ShowHalpHand(false);
            GlobalValues.isShowHelpHandToSelectCharacter = false;
            GlobalValues.Save();
        }


        public void ScrollToModel(QbertModel first , QbertModel second , float speedMove)
        {
            float x = first.transform.localPosition.x - second.transform.localPosition.x;
            x *= 0.3f;
            var moveTo = rootModels.localPosition + new Vector3(0, 0, x);
            scrollInWork = StartCoroutine(MovingLocalTransformTo(rootModels, moveTo, speedMove));
            StartCoroutine(this.ScaleTranformTo(first.transform, scaleModelNoFocus, speedMove));
            StartCoroutine(this.ScaleTranformTo(second.transform, 1.0f, speedMove));
        }

        public void UpdateInfoFromModel(QbertModel model)
        {
            nameCurrentCharacter.text = model.nameCharacter;
            descriptionCurrentCharacter.text = model.description;
        }


        public void OnScrollEvent(DirectionMove.Direction dir)
        {
            if (scrollInWork == null)
            {
                if (dir == DirectionMove.Direction.DownLeft)
                {
                    Scroll(true);
                }
                else if (dir == DirectionMove.Direction.UpRight)
                {
                    Scroll(false);
                }
            }
        }

        void Start ()
        {
            diagonalSwipe.gameObject.SetActive(false);
            InitScene();
            SetFocusToGlobalModel();

        }

        public void SetFocusToGlobalModel()
        {
            var model = models.FirstOrDefault(x => x.nameCharacter == GlobalValues.currentModel);
            if (model != null)
            {
                ScrollToModel(currentModel , model , 0.0f);
                UpdateInfoFromModel(model);
                currentModel = model;
            }
        }


        IEnumerator Test()
        {
            yield return new WaitForSeconds(2.0f);
            Scroll(true);
            yield return new WaitForSeconds(2.0f);
            Scroll(false);
        }

        public IEnumerator MovingLocalTransformTo(Transform tr, Vector3 movingTo, float time)
        {
            float distance = Vector3.Distance(tr.localPosition, movingTo);
            float speedMoving = distance / time;

            while (distance > 0.001f)
            {
                Vector3 move = Vector3.MoveTowards(tr.localPosition, movingTo, speedMoving * Time.deltaTime);
                tr.localPosition = move;
                distance = Vector3.Distance(tr.localPosition, movingTo);
                yield return null;
            }

            scrollInWork = null;
        }

        void Update () 
        {
	
        }
    }
}
