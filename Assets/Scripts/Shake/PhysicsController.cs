using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsController : MonoBehaviour
{

    public float ShakeForceMultiplier;
    public Rigidbody2D[] ShakingRigidbodies;

    public void ShakeRigidbodies(Vector3 deviceAcceleration)
    {
        foreach (var rigidbody in ShakingRigidbodies)
        {
            rigidbody.AddForce(deviceAcceleration * ShakeForceMultiplier, ForceMode2D.Impulse);
        }
    }
}
