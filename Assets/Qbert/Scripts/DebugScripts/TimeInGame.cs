using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Qbert.Scripts.DebugScripts
{
    public class TimeInGame : MonoBehaviour
    {
        public Text text;

        void Start () 
        {
	
        }

        void Update ()
        {
            var ts = TimeSpan.FromSeconds(GlobalValues.timeInGameSecond);

            string timeInGame = string.Format("{0:D2}:{1:D2}:{2:D2}",
                ts.Hours, ts.Minutes, ts.Seconds);

            text.text = timeInGame;
        }
    }
}
