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
    public class GuiSelectCharacter : SwipeModel<QbertModel>
    {
        public GlobalConfigurationAsset globalConfigurationAsset;
        public TextMesh nameCurrentCharacter;
        public TextMesh descriptionCurrentCharacter;
        public CameraMenuController cameraMenuController;
        public GuiMainMenu rootMenu;
        public DiagonalSwipe diagonalSwipe;
        
        public Transform playImage;
        public TextMesh textPrice;
        public TextMesh textCountModels;

        public void OnFocusCameraToThis()
        {
            diagonalSwipe.gameObject.SetActive(true);
            cameraMenuController.OnCameraMoveSelectCharacter();

            ShowHelpHand(false);
            UpdateCountModels();

            UnscaleTimer.StartDelay(1.0f, timer =>
            {
                ShowHelpHand(GlobalValues.isShowHelpHandToSelectCharacter);
            });
        }

        public void UpdateCountModels()
        {
            int countAll = this.Count();
            int countOpen = this.Count(x => x.isFree || x.isBuyed());

            textCountModels.text = string.Format("{0}/{1}", countOpen, countAll);
        }

        public override void Scroll(bool isLeft)
        {
            base.Scroll(isLeft);


            UpdateInfoFromModel(currentModel);

            ShowHelpHand(false);
            GlobalValues.isShowHelpHandToSelectCharacter = false;
            GlobalValues.Save();
        }

        public void ShowPrice(float price)
        {
            playImage.gameObject.SetActive(false);
            textPrice.gameObject.SetActive(true);

            textPrice.text = string.Format("$ {0:F2}", price);
        }

        public void ShowPlay()
        {
            playImage.gameObject.SetActive(true);
            textPrice.gameObject.SetActive(false);
        }

        public void OnCloseButtonPress()
        {
            cameraMenuController.OnCamaraMoveToRootMenu();
            ShowHelpHand(false);
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

            CreateModels(characters);

            foreach (var model in this)
            {
                model.booldeDead.gameObject.SetActive(false);

                if (!model.isFree && !model.isBuyed())
                {
                    MeshRenderer[] renders = model.GetComponentsInChildren<MeshRenderer>();
                    foreach (var meshRenderer in renders)
                    {
                        SetTransparentMaterial(meshRenderer,
                            SwitchMaterialRenderingMode.BlendMode.Transparent, 0.3f);
                    }
                }
            }

            UpdateInfoFromModel(currentModel);
        }
        private void SetTransparentMaterial(MeshRenderer meshRenderer,
            SwitchMaterialRenderingMode.BlendMode mode, float transparent)
        {
            Material newMat = new Material(meshRenderer.material);
            var material = newMat;

            SwitchMaterialRenderingMode.SetupMaterialWithBlendMode( material, mode);
            material.color = new Color(material.color.r, material.color.g, material.color.b, transparent);
            meshRenderer.material = material;
        }

        public void UpdateInfoFromModel(QbertModel model)
        {
            nameCurrentCharacter.text = model.nameCharacter;
            descriptionCurrentCharacter.text = model.description;

            if (model.isFree || model.isBuyed())
            {
                ShowPlay();
            }
            else
            {
                ShowPrice(model.price);
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
                SetFocusToModel(model);
                UpdateInfoFromModel(model);
            }
        }
        IEnumerator Test()
        {
            yield return new WaitForSeconds(2.0f);
            Scroll(true);
            yield return new WaitForSeconds(2.0f);
            Scroll(false);
        }

        void Update () 
        {
	
        }
    }
}
