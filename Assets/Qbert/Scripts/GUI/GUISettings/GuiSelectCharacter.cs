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
        public Transform playImage;
        public TextMesh textPrice;
        public TextMesh textCountModels;

        public void OnFocusCameraToThis()
        {
            ConnectSwipe();
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
            DisconnectSwipe();
        }
        public void OnButtonCharacterSelect()
        {
            if (currentModel.isBuyed() || currentModel.isFree)
            {
                Debug.Log("Select character: " + currentModel.nameCharacter);

                GlobalValues.currentModel = currentModel.nameCharacter;
                GlobalValues.Save();

                DisconnectSwipe();
                rootMenu.OnFocusCameraToThis();
            }
            else
            {
                
                //Buy process
                SetTransparentMaterial(currentModel.transform,
                        SwitchMaterialRenderingMode.BlendMode.Opaque, 0.3f);

                GlobalValues.AddBuyModel(currentModel.codeName);

                ShowPlay();
            }
        }

        public void InitScene()
        {
            var characters = globalConfigurationAsset.characters;

            CreateModels(characters);

            foreach (var model in this)
            {
                model.booldeDead.gameObject.SetActive(false);

                if (model.isFree || model.isBuyed())
                {
                    SetTransparentMaterial(model.transform,
                        SwitchMaterialRenderingMode.BlendMode.Opaque, 0.3f);
                }
                else
                {
                    SetTransparentMaterial(model.transform,
                        SwitchMaterialRenderingMode.BlendMode.Transparent, 0.3f);
                }
            }

            UpdateInfoFromModel(currentModel);
        }
        private void SetTransparentMaterial(Transform model,
            SwitchMaterialRenderingMode.BlendMode mode, float transparent)
        {
            MeshRenderer[] renders = model.GetComponentsInChildren<MeshRenderer>();
            foreach (var meshRenderer in renders)
            {
                Material newMat = new Material(meshRenderer.material);
                var material = newMat;

                SwitchMaterialRenderingMode.SetupMaterialWithBlendMode(material, mode);
                material.color = new Color(material.color.r, material.color.g, material.color.b, transparent);
                meshRenderer.material = material;
            }
        }

        public void UpdateInfoFromModel(QbertModel model)
        {
            nameCurrentCharacter.text = model.nameCharacter.ToUpper();
            descriptionCurrentCharacter.text = model.description.ToUpper();

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
