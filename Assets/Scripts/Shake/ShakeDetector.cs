using System;
using UnityEngine;

namespace Shake
{
    public class ShakeDetector : MonoBehaviour
    {
        #region Singleton

        private static ShakeDetector _instance;
        
        private void Awake()
        {
            if (_instance != null)
            {
                _instance = this;
            }
            else
            {
                Destroy(this);
                Debug.LogError("More than one instance of ShakeDetector.");
            }
        }

        #endregion
        
        
        public delegate void OnShakeDelegate(float lastShakeTime);
        public static event OnShakeDelegate OnShake;

        [SerializeField] private float shakeDetectionThreshold;
        [SerializeField] private float minShakeInterval;

        [SerializeField] private float sqrShakeDetectionThreshold;
        [SerializeField] private float lastShakeTime;

        private const int PowerOfTwo = 2;

        void Start()
        {
            sqrShakeDetectionThreshold = Mathf.Pow(shakeDetectionThreshold, PowerOfTwo);
        }

        void Update()
        {
            //if the acceleration magnitude is smaller than the threshold
            //the method will not run.
            if (Input.acceleration.sqrMagnitude < sqrShakeDetectionThreshold)
                return;
        
            //if the unscaled time is smaller than the minimum time between shakes the method
            //will return.
            if (Time.unscaledTime < lastShakeTime + minShakeInterval)
                return;
        
            lastShakeTime = Time.unscaledTime;
            OnShake?.Invoke(lastShakeTime);
        }
    }
}