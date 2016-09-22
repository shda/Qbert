using UnityEngine;
using System.Collections;

namespace Scripts.GameScene
{
    public abstract class ITime : MonoBehaviour
    {
        private float _time;
        public virtual float time
        {
            get { return _time; }
            set
            {
                _time = Mathf.Clamp(value, minValue, maxValue);
                ChangeValue(_time);
            }
        }


        protected virtual float maxValue
        {
            get { return 0.999f; }
        }

        protected virtual float minValue
        {
            get { return 0.0f; }
        }

        public abstract void ChangeValue(float value);
    }
}
