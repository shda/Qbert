using System;
using System.Linq;
using UnityEngine;

namespace Assets.Qbert.Scripts.GUI.GUISettings
{
    [ExecuteInEditMode]
    public class VerticalLayoutGroup3d : MonoBehaviour
    {
        public Transform root;
        public float offset;
        private float oldOffset = -1;

        public Transform[] childrens;


        public void UpdatePositions()
        {
            childrens = root.Cast<Transform>().ToArray();
            for (int i = 0; i < childrens.Length; i++)
            {
                childrens[i].localPosition = new Vector3(0 , 0, offset * i);
            }
        }

        void Start () 
        {
	
        }
	
        void Update () 
        {
            if (root != null)
            {
                if (Math.Abs(oldOffset - offset) > 0.00001f)
                {
                    UpdatePositions();
                    oldOffset = offset;
                }
            }
	    
        }
    }
}
