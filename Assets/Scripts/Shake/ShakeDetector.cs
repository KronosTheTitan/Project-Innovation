using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(PhysicsController))]
public class ShakeDetector : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI shakeTxt = null;
    [SerializeField] private Slider ShakeSlider= null;

    public float ShakeDetectionThreshold;
    public float MinShakeInterval;

    private float sqrShakeDetectionThreshold;
    private float timeSinceLastShake;

    private PhysicsController physicsController;

    void Start()
    {
        sqrShakeDetectionThreshold = Mathf.Pow(ShakeDetectionThreshold, 2);
        physicsController = GetComponent<PhysicsController>();

    }

    void Update()
    {
        shakeTxt.text = (Input.acceleration.sqrMagnitude + " ");

        
        if (Input.acceleration.sqrMagnitude >= sqrShakeDetectionThreshold
            && Time.unscaledTime >= timeSinceLastShake + MinShakeInterval)
        {
            physicsController.ShakeRigidbodies(Input.acceleration);
            timeSinceLastShake = Time.unscaledTime;

        }
    }
}
