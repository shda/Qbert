using UnityEngine;
using System.Collections;
using Assets.Qbert.Scripts.GameScene.Characters;

public class LogoMoveOupDown : MonoBehaviour
{
    public Qbert qbert;
    public RectTransform logo;
    private float startPosition;

	void Start ()
	{
	    startPosition = logo.anchoredPosition.y;

	}

	void Update ()
	{
	    Vector3 v = qbert.rootModel.localScale;

        logo.anchoredPosition = new Vector2(logo.anchoredPosition.x ,
            startPosition + v.y * 100.0f - 100.0f);

       // Debug.Log(v.y);

	}
}
