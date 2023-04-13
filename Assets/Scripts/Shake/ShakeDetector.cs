using System;
using UnityEngine;

namespace Shake
{
    public class ShakeDetector : MonoBehaviour
    {

        [SerializeField] private float shakeDetectionThreshold;
        [SerializeField] private float minShakeInterval;

        [SerializeField] private float sqrShakeDetectionThreshold;
        [SerializeField] private float lastShakeTime;
        [SerializeField] private float shakeStrength;
        [SerializeField] private float shakeDecaySpeed;

        private const int PowerOfTwo = 2;

        void Start()
        {
            sqrShakeDetectionThreshold = Mathf.Pow(shakeDetectionThreshold, PowerOfTwo);
        }

        public bool HasBeenShakingFor(float threshold)
        {
            if (shakeStrength > threshold)
                return true;

            return false;
        }

        bool detectorEnabled;

        void Update()
        {

            if (!detectorEnabled)
                return;
            Debug.Log(shakeStrength);
            Debug.Log(shakeDetectionThreshold);

            shakeStrength -= shakeDecaySpeed * Time.deltaTime;

            shakeStrength = Mathf.Clamp(shakeStrength,0.1f,100);

            //if the acceleration magnitude is smaller than the threshold
            //the method will not run.
            if (Input.acceleration.sqrMagnitude < sqrShakeDetectionThreshold)
            {
                return;
            }
        
            //if the unscaled time is smaller than the minimum time between shakes the method
            //will return.
            if (Time.unscaledTime < lastShakeTime + minShakeInterval)
            {
                return;
            }

            

            shakeStrength += 2 * Time.deltaTime;
            lastShakeTime = Time.unscaledTime;
        }

        public void StartDetector()
        {
            detectorEnabled = true;
        }

        public void StopDetector()
        {
            shakeStrength = 1;
            detectorEnabled = false;
        }
    }
}