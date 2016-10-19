using UnityEngine;
using System.Collections;

public class GiftBoxAnimator : MonoBehaviour
{
    public AnimationToTimeOneByOne animationOpenBox;
	void Start () 
	{
        animationOpenBox.StartOneByOne();

    }

	void Update () 
	{
	
	}
}
