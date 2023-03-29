using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class Submarine : MonoBehaviour
{
    /// <summary>
    /// the maximum speed of the submarine
    /// </summary>
    [SerializeField] private float maxSpeed;
    /// <summary>
    /// the minimum speed of the submarine
    /// negative number allows reversing
    /// </summary>
    [SerializeField] private float minSpeed;
    
    /// <summary>
    /// the smoothing for speeding up and slowing down.
    /// </summary>
    [SerializeField] private float accelerationSmoothing;
    
    /// <summary>
    /// the smoothing to make the camera not jitter around a lot.
    /// </summary>
    [SerializeField] private float lookSmoothing = 0.1f;

    /// <summary>
    /// the slider used to control the throttle
    /// </summary>
    [SerializeField] private Slider throttleSlider;
    [SerializeField] private Slider fuelBar;

    [SerializeField] private GyroControls gyroControls;
    [SerializeField] private new Rigidbody rigidbody;

    [SerializeField] private float currentFuel;
    
    private void Update()
    {
        //Check if the player has enough fuel to continue.
        if (currentFuel <= 0)
        {
            throttleSlider.value = 0;
            rigidbody.velocity = new Vector3(0, 0, 0);
            
            OnPlayerOutOfFuel?.Invoke();
            
            return;
        }

        if(!gyroControls.enabled)
            return;
        
        transform.rotation = Quaternion.Slerp(transform.rotation, gyroControls.GetInitialRotation() * gyroControls.GetCurrentRotation(), lookSmoothing);

        float currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, throttleSlider.value/throttleSlider.maxValue);
        
        Vector3 desiredVelocity = transform.forward * currentSpeed;
        
        rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, desiredVelocity,accelerationSmoothing);

        currentFuel -= currentSpeed * Time.deltaTime;

        //todo: move code to separate area because its UI and doesn't belong in here.
        fuelBar.value = currentFuel;
    }

    public delegate void PlayerOutOfFuel();
    public event PlayerOutOfFuel OnPlayerOutOfFuel;
}