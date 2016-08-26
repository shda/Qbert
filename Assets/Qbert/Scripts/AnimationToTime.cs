using UnityEngine;
using System.Collections;

public class AnimationToTime : MonoBehaviour
{
    public string animationName;
    public Animator animator;
    public bool reverse;

    private int heshAnimationName;

    public float time
    {
        set
        {
            value = Mathf.Clamp01(value);
            animator.Play(heshAnimationName, 0, reverse ? 1.0f - value : value);
            animator.Update(0);
        }
    }

    void Awake()
    {
        heshAnimationName = Animator.StringToHash(animationName);
        animator.speed = 0;
    }

    void Start () 
	{
        
    }
	
	void Update () 
	{
	
	}
}
