using UnityEngine;

namespace Assets.Qbert.Scripts.DebugScripts
{
    public class DebugInfo : MonoBehaviour
    {
        private static DebugInfo debugInfo;

        void Awake()
        {
            if (debugInfo != null)
            {
                Destroy(gameObject);
                return;
            }

            debugInfo = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
