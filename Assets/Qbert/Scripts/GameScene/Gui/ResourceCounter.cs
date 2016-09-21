using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.GameScene.Gui
{
    public class ResourceCounter : MonoBehaviour
    {
        public Text _label;
        public float _currentCount;

        public float count
        {
            get { return _currentCount; }
        }
        public float _labelCount = 0;

        public float _speedAnimation;

        public void SetValue(float value)
        {
            _currentCount = value;
        }

        void Start () 
        {
	
        }
        // Update is called once per frame
        void Update () 
        {
            if (!(Math.Abs(_currentCount - _labelCount) < 0.01))
            {

                float speed = 0;
                float countSpeed = Math.Abs(_currentCount - _labelCount);

                if (countSpeed > 100)
                    speed = _speedAnimation * (countSpeed / 50.0f);
                else
                    speed = _speedAnimation;

                if (_currentCount > _labelCount)
                {
                    _labelCount += speed * Time.deltaTime;
                    if (_labelCount > _currentCount)
                        _labelCount = _currentCount;
                }
                else
                {
                    _labelCount -= speed * Time.deltaTime;
                    if (_labelCount < _currentCount)
                        _labelCount = _currentCount;
                }

                UpdateText();
            }
        }

        public void UpdateText()
        {
            _label.text = string.Format("{0}", (int)_labelCount);
        }
    }
}
