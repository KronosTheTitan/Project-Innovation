using UnityEngine;

namespace Controls
{
    /// <summary>
    /// This class is used to interact with the Gyroscope.
    /// if any problems appear 
    /// </summary>
    public class GyroUtils
    {
        /// <summary>
        /// if there is a gyroscope available this method to return true
        /// </summary>
        /// <returns></returns>
        public static bool IsGyroAvailable()
        {
            return Input.gyro.enabled;
        }
        /// <summary>
        /// Use this method to get the rotation of the gyroscope in unity
        /// ready measurements.
        /// </summary>
        /// <returns></returns>
        public static Quaternion GetGyroscopeRotation()
        { 
            return GyroToUnity(Input.gyro.attitude);
        }
        
        /// <summary>
        /// use this method to check if the device is being shaken by the user based
        /// on a certain threshold.
        /// </summary>
        /// <param name="threshold"> parameter is used to decide the minimum movement
        /// for it to count as shaking</param>
        /// <returns></returns>
        public static bool IsShaking(float threshold)
        {
            if (Input.gyro.userAcceleration.magnitude > threshold)
                return true;
            
            return false;
        }

        /// <summary>
        /// This method is used to convert the gyro
        /// rotation into a unity compatible rotation.
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        private static Quaternion GyroToUnity(Quaternion q)
        {
            return new Quaternion(q.x, q.y, -q.z, -q.w);
        }
    }
}