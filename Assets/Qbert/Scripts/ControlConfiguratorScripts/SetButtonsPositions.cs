using UnityEngine;

namespace Assets.Qbert.Scripts.ControlConfiguratorScripts
{
    public class SetButtonsPositions : MonoBehaviour
    {
        public Transform root;

        void Start ()
        {
            UpdatePositions();
        }

        public void UpdatePositions()
        {
            var childs = root.GetComponentsInChildren<ButtonMove>();
            foreach (var buttonMove in childs)
            {
                buttonMove.LoadPosition();
            }
        }
	
        void Update () 
        {
	
        }
    }
}
