using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Qbert.Scripts.DebugScripts
{
    public class DisableForTime : MonoBehaviour
    {
        public Transform[] objects;
        public float time;

        public Text disableText;

        void Start ()
        {
            StartCoroutine(Timer());
        }

        IEnumerator Timer()
        {
            while (true)
            {
                yield return new WaitForSeconds(time);

                string text = "";

                foreach (var o in objects)
                {
                    text += o.name + "\n";
                    if (o.gameObject.activeSelf)
                    {
                        text += "<color=red>" + o.name + "</color>";
                        o.gameObject.SetActive(false);
                        break;;
                    }
                }

                if (disableText)
                {
                    disableText.text = text;
                }
            
            }
        }
	
        void Update () 
        {
	    
        }
    }
}
