using UnityEngine;

namespace Assets.Qbert.Scripts.DebugScripts
{
    public class DebugLog : MonoBehaviour
    {
        public string toLog = "DebugLog";

        public void OnLog()
        {
            Debug.Log(name + " + " + toLog);
        }


        void Start () 
        {
	
        }
	
        void Update () 
        {
	
        }
    }
}
