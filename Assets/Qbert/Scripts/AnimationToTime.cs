using UnityEngine;
using System.Collections;

public class AnimationToTime : MonoBehaviour , ITimeScale
{
    //ITimeScale
    private float _timeScale = 1.0f;
    public float timeScale
    {
        get { return _timeScale; }
        set { _timeScale = value; }
    }
    //end ITimeScale

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

    public IEnumerator PlayToTime(float duration , ITimeScale ITimeScale = null)
    {
        ITimeScale iTimeCurrent = ITimeScale ?? this;
        float t = 0;
        while (t < 1)
        {
            t += (Time.deltaTime * iTimeCurrent.timeScale) / duration;
            time = t;
            yield return null;
        }

        t = 1;

        time = t;
    }

    void Start () 
	{
        
    }
	
	void Update () 
	{
	
	}
}
