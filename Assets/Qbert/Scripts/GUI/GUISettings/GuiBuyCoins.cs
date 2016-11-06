using UnityEngine;
using System.Collections;
using Assets.Qbert.Scripts;
using Assets.Qbert.Scripts.GameScene.AD;
using Assets.Qbert.Scripts.GameScene.GameAssets;
using Assets.Qbert.Scripts.GameScene.Gui;
using Assets.Qbert.Scripts.GestureRecognizerScripts;
using Assets.Qbert.Scripts.GUI.GUISettings;
using Assets.Qbert.Scripts.Utils;

public class GuiBuyCoins : SwipeModel<CoinBuyModels>
{
    public GlobalConfigurationAsset globalConfigurationAsset;
    public CameraMenuController cameraMenuController;

    public TextMesh textCoinsAddToWall;
    public TextMesh textPriceToWall;
    public TextMesh textPriceToButton;

    public Transform imageWatchAd;

    public Transform buttonDisabled;
    public Transform buttonEnabled;

    public ResourceCounter coinsCounter;

    public VideoAD videoAd;

    public void OnFocusCameraToThis()
    {
        ConnectSwipe();
        cameraMenuController.OnCameraMoveCoinsBuy();

        ShowHelpHand(false);
        UnscaleTimer.StartDelay(1.0f, timer =>
        {
            ShowHelpHand(GlobalValues.isShowHelpHandToSelectCharacter);
        });
    }

    public void OnCloseButtonPress()
    {
        DisconnectSwipe();
        cameraMenuController.OnCamaraMoveToRootMenu();
        ShowHelpHand(false);
    }

    public void InitScene()
    {
        var characters = globalConfigurationAsset.coinsModels;
        CreateModels(characters);
        UpdateTextToCorrent();
    }

    public override void Scroll(bool isLeft)
    {
        base.Scroll(isLeft);

        UpdateTextToCorrent();
    }

    public void UpdateTextToCorrent()
    {
        if (currentModel.isWatchAd)
        {
            imageWatchAd.gameObject.SetActive(true);
            textPriceToButton.gameObject.SetActive(false);

            if (GlobalValues.isCointsByWatchAdIsBeenViewed)
            {
                buttonDisabled.gameObject.SetActive(true);
                buttonEnabled.gameObject.SetActive(false);
            }
            else
            {
                buttonDisabled.gameObject.SetActive(false);
                buttonEnabled.gameObject.SetActive(true);
            }
        }
        else
        {
            buttonDisabled.gameObject.SetActive(false);
            buttonEnabled.gameObject.SetActive(true);

            imageWatchAd.gameObject.SetActive(false);
            textPriceToButton.gameObject.SetActive(true);
        }

        textPriceToButton.text = string.Format("$ {0:F2}", currentModel.price);
        textCoinsAddToWall.text = string.Format("{0} coins", currentModel.coinsAdd);
        textPriceToWall.text = string.Format("{0}", currentModel.description);
    }

    public void OnButtonBuy()
    {
        if (currentModel.isWatchAd)
        {
            if(GlobalValues.isCointsByWatchAdIsBeenViewed)
                return;

            videoAd.ShowAD(isOk =>
            {
                if (isOk)
                {
                    coinsCounter.SetValue(GlobalValues.AddCoins(currentModel.coinsAdd));
                    GlobalValues.isCointsByWatchAdIsBeenViewed = true;
                    GlobalValues.Save();

                    UpdateTextToCorrent();
                }
            });
        }
        else
        {
            
            //Buy events
            coinsCounter.SetValue(GlobalValues.AddCoins(currentModel.coinsAdd));
            UpdateTextToCorrent();
        }

    }


    void Start()
    {
        diagonalSwipe.gameObject.SetActive(false);
        InitScene();
    }

	void Update () 
	{
	
	}

    
}
