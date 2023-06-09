﻿using System;
using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene
{
    public abstract class ITime : MonoBehaviour , ITimeScale
    {
        //ITimeScale
        private float _timeScale = 1.0f;
        public float timeScale
        {
            get { return _timeScale; }
            set { _timeScale = value; }
        }
        //end ITimeScale

        public bool isReverce = false;

        private float _time;
        private float _oldTime = float.MaxValue;
        public virtual float time
        {
            get { return _time; }
            set
            {
                _time = Mathf.Clamp(value, minValue, maxValue);
                ChangeValue(isReverce ? 1.0f - _time : _time);
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
