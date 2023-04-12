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

        void Update()
        {
            shakeStrength -= shakeDecaySpeed * Time.deltaTime;
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
            
            shakeStrength += 1 * Time.deltaTime;
            lastShakeTime = Time.unscaledTime;
        }
    }
}