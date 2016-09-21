using UnityEngine;
using System.Collections;

 public abstract class SingletonT<T> : MonoBehaviour where T : SingletonT<T>
{
    public abstract void AwakeFirst();

	void Start () 
	{
	
	}
	void Update () 
	{
	
	}

#region Singleton
    private static T _instance = null;

    public static T instance
    {
        get
        {
            return _instance;
        }
    }

    public void Awake()
    {
        if (_instance == null)
        {
            _instance =  this as T;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        
        AwakeFirst();

        _instance = this as T;
    }
#endregion
}
