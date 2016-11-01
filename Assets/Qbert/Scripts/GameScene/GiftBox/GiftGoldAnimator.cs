using System;
using UnityEngine;
using System.Collections;
using Assets.Qbert.Scripts.GameScene;
using Assets.Qbert.Scripts.GameScene.AnimationToTime;
using Assets.Qbert.Scripts.GameScene.Gui;
using Assets.Qbert.Scripts.Utils;
using UnityEngine.UI;

public class GiftGoldAnimator : MonoBehaviour
{
    public Transform rootGiftBox; 
    public GameObject prefabGift;
    public Transform rootAll;
    public Vector3 setGravity;
    public Transform giftBox;
    public Transform doubleGiftRoot;

    public VideoAD videoAd;

    public ResourceCounter currentCoinsCount;
    public ResourceCounter addGoldCoinsCount;

    public TapScreen tapScreenRoot;
    public Button buttonPressExit;
    public Button buttonPressVideoToGift;

    public bool showDoubleGift { get; set; }

    public Action OnEndGift;
    public Action<bool> OnPressVideoToGift;

    [Header("Animations")]

    public AnimationToTimeChangeImageAlpha background;
    public AnimationToTimeMassive showTapAnimation;
    public AnimationToTimeMassive hideTapAnimation;
    public AnimationToTime showCenterCoints;
    public AnimationToTimeMassive showPanelAndLabels;
    public AnimationToTimeMassive hideAllElements;

    public ITime[] resetAnimations;

    private GiftAnimations animations;
    private Vector3 oldGravity;

    void Start () 
	{
        
    }

    public void OnPressExitButton()
    {
        HidePanels(OnEndGift);
    }

    public void OnPressVideoToGiftButton()
    {
       // 
        HidePanels(() =>
        {
            videoAd.ShowAD((bool isOk) =>
            {
                OnPressVideoToGift(isOk);
            });

        });
    }

    public void HidePanels(Action AfterAction)
    {
        buttonPressExit.onClick.RemoveAllListeners();
        buttonPressVideoToGift.onClick.RemoveAllListeners();

        StopAllCoroutines();
        StartCoroutine(HideAllElemenysAnimation(AfterAction));
    }

    IEnumerator HideAllElemenysAnimation(Action OnEnd)
    {
        yield return StartCoroutine(hideAllElements.PlayToTime(0.5f));
        rootAll.gameObject.SetActive(false);

        if (OnEnd != null)
        {
            OnEnd();
        }
    }

    void ResetAnimations()
    {
        foreach (var resetAnimation in resetAnimations)
        {
            resetAnimation.time = 0;
        }
    }

    public void GiftDropToGround()
    {
        doubleGiftRoot.gameObject.SetActive(showDoubleGift);

        ResetAnimations();
        rootAll.gameObject.SetActive(true);
        CreateNewBox();
        StopAllCoroutines();
        StartCoroutine(DropBoxAnimation());
    }

    IEnumerator DropBoxAnimation()
    {
        yield return StartCoroutine(animations.dropToGround.PlayToTime(0.5f));
        yield return StartCoroutine(showTapAnimation.PlayToTime(0.2f));

        tapScreenRoot.OnTapScreen = OnTapScreen;
    }

    IEnumerator OpenGiftAnimation()
    {
        yield return StartCoroutine(hideTapAnimation.PlayToTime(0.5f, null, true));

        oldGravity = Physics.gravity;
        Physics.gravity = setGravity;

        yield return StartCoroutine(animations.opennBox.PlayToTime(1.0f));
        yield return new WaitForSeconds(2.0f);
        yield return StartCoroutine(showCenterCoints.PlayToTime(0.3f));
        yield return new WaitForSeconds(1.0f);
        yield return StartCoroutine(showPanelAndLabels.PlayToTime(0.3f));

        currentCoinsCount.SetValue(currentCoinsCount.count + addGoldCoinsCount.count);
        addGoldCoinsCount.SetValue(0);

        buttonPressExit.onClick.RemoveAllListeners();
        buttonPressExit.onClick.AddListener(OnPressExitButton);

        buttonPressVideoToGift.onClick.RemoveAllListeners();
        buttonPressVideoToGift.onClick.AddListener(OnPressVideoToGiftButton);
    }


    private void OnTapScreen()
    {
        tapScreenRoot.OnTapScreen = null;
        StopAllCoroutines();
        StartCoroutine(OpenGiftAnimation());
    }

    public void HideGift()
    {
        Physics.gravity = oldGravity;
    }

    private void CreateNewBox()
    {
        if (giftBox != null)
        {
            giftBox.gameObject.SetActive(false);
            Destroy(giftBox.gameObject);
        }

        GameObject giftBoxObject = Instantiate(prefabGift);
        giftBox = giftBoxObject.transform;


        giftBox.transform.SetParent(rootGiftBox);
        giftBox.transform.localPosition = Vector3.zero;
        giftBox.transform.localScale = new Vector3(1, 1, 1);

        animations = giftBox.GetComponentInChildren<GiftAnimations>();
    }

	void Update () 
	{
	
	}
}
