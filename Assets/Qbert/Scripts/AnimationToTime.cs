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

    public IEnumerator PlayToTime(float duration)
    {
        float t = 0;
        while (t < 0.99f)
        {
            t += Time.smoothDeltaTime / duration;
            time = t;
            yield return null;
        }

        t = 0.99f;

        time = t;
    }

    void Start () 
	{
        
    }
	
	void Update () 
	{
	
	}
}
