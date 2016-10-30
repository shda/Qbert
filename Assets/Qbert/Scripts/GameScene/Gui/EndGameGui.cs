using UnityEngine;
using System.Collections;
using Assets.Qbert.Scripts.GameScene.Gui;

public class EndGameGui : MonoBehaviour
{
    public EndGameGuiAnimator endGameGuiAnimator;

    public SecondMenuPanel secondPanel;
    public FirstMenuPanel firstPanel;

    public void ShowGameOver()
    {
        endGameGuiAnimator.ShowGameOver();
    }

    public void OnShowSecondRails()
    {
        endGameGuiAnimator.ShowSecondRails();
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
