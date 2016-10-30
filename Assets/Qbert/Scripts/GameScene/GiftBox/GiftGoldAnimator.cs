using System;
using UnityEngine;
using System.Collections;
using Assets.Qbert.Scripts.GameScene.AnimationToTime;
using Assets.Qbert.Scripts.GameScene.Gui;
using Assets.Qbert.Scripts.Utils;

public class GiftGoldAnimator : MonoBehaviour
{
    public Transform rootGiftBox; 
    public GameObject prefabGift;
    public Transform rootAll;
    public Vector3 setGravity;
    public Transform giftBox;

    public AnimationToTimeMassive showTapAnimation;
    public AnimationToTimeMassive hideTapAnimation;
    public AnimationToTime showCenterCoints;
    public AnimationToTimeMassive showPanelAndLabels;

    public ResourceCounter currentCoinsCount;
    public ResourceCounter addGoldCoinsCount;

    public TapScreen tapScreenRoot;

    private GiftAnimations animations;
    private Vector3 oldGravity;

    void Start () 
	{
        
    }

    public void GiftDropToGround()
    {
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
