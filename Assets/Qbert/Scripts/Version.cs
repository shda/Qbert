using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Version : MonoBehaviour 
{
    public string version;
    public int buildCounter;
    public string date;

    public Text versionText;

    void Start ()
    {
        versionText.text = string.Format("{0}\n{1}", version, date);
    }
	
	void Update () 
	{
	
	}
}
