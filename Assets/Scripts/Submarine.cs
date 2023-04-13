using Shake;
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
    [SerializeField] private float maxFuel;

    [SerializeField] private float lastSquid;
    [SerializeField] private float minTimeBetweenSquid;
    [SerializeField] private float maxTimeBetweenSquid;
    [SerializeField] private float currentTimeBetweenSquid;
    [SerializeField] private bool squidPresent;
    [SerializeField] private GameObject squid;

    [SerializeField] private float shakeLengthRequired;
    [SerializeField] private ShakeDetector shakeDetector;
    private void Update()
    {
        if (squidPresent)
        {
            currentFuel -= 10 * Time.deltaTime;

            if (shakeDetector.HasBeenShakingFor(shakeLengthRequired))
            {
                lastSquid = Time.time;
                currentTimeBetweenSquid = Random.Range(minTimeBetweenSquid, maxTimeBetweenSquid);
                squidPresent = false;
                squid.SetActive(false);

                //Debug.Log("Squid Cleared" + Time.time);
                
            }

            return;
        }

        if (lastSquid + currentTimeBetweenSquid > Time.time)
        {
            //Debug.Log("squid apeared");

            squid.SetActive(true);
            squidPresent = true;
            throttleSlider.value = 1;
            
            
            return;
        }
        
        
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

        currentFuel -= 1 * Time.deltaTime;

        //todo: move code to separate area because its UI and doesn't belong in here.
        fuelBar.value = currentFuel;
        
        //Debug.Log("---Start of frame---");
        //Debug.Log(currentSpeed);
        //Debug.Log(rigidbody.velocity);
        //Debug.Log("---End of frame---");
    }

    public delegate void PlayerOutOfFuel();
    public event PlayerOutOfFuel OnPlayerOutOfFuel;

    public void AddFuel(float amountOfFuel)
    {
        currentFuel += amountOfFuel;
        currentFuel = Mathf.Clamp(currentFuel, 0, maxFuel);
    }
}