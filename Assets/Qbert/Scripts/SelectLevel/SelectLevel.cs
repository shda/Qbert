using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour
{
    public static int selectLevel = 0;

    public string sceneName;

    public void RunLevel(int level)
    {
        selectLevel = level;
        SceneManager.LoadScene(sceneName);
    }

	void Start () 
	{
	
	}
	
	void Update () 
	{
	
	}
}
