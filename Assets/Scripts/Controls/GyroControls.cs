using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class GyroControls : MonoBehaviour
{
    // STATE
    private Transform _rawGyroRotation;
    private Quaternion _initialRotation; 
    private Quaternion _gyroInitialRotation;
    private Quaternion _offsetRotation;

    private bool _gyroEnabled;
    private bool _gyroInitialized = false;

    // SETTINGS
    [SerializeField] private float smoothing = 0.1f;
    [SerializeField] private float speed = 60.0f;
    [SerializeField] private bool waitGyroInitialization = true; 
    [SerializeField] private float waitGyroInitializationDuration = 1f; 

    public bool debug;

    private void InitGyro() {
        if(!_gyroInitialized){
            Input.gyro.enabled = true;
            Input.gyro.updateInterval = 0.0167f;
        }
        _gyroInitialized = true;
    }

    private void Awake() {
        if(waitGyroInitialization && waitGyroInitializationDuration < 0f){
            waitGyroInitializationDuration = 1f;
            throw new System.ArgumentException("waitGyroInitializationDuration can't be negative, it was set to 1 second");
        }
    }

    private IEnumerator Start()
    {
        if(HasGyro()){
            InitGyro();
            _gyroEnabled = true;
            Debug.Log("this device has a gyro");
        } else _gyroEnabled = false;

        if(waitGyroInitialization)
            yield return new WaitForSeconds(waitGyroInitializationDuration);
        else
            yield return null;
        
        /* Get object and gyroscope initial rotation for calibration */
        _initialRotation = transform.rotation; 
        Recalibrate();

        
        /* GameObject instance used to prepare object movement */
        _rawGyroRotation = new GameObject("GyroRaw").transform;
        _rawGyroRotation.position = transform.position;
        _rawGyroRotation.rotation = transform.rotation;
    }

    protected virtual void Update()
    {
        if (Time.timeScale == 1 && _gyroEnabled)
        {
            ApplyGyroRotation(); // Get rotation state in rawGyroRotation
        }
    }

    private void ApplyGyroRotation()
    {
        // Apply initial offset for calibration
        _offsetRotation = Quaternion.Inverse(_gyroInitialRotation) * GyroToUnity(Input.gyro.attitude);

        float curSpeed = Time.deltaTime * speed;
        Quaternion tempGyroRotation = new Quaternion(
            _offsetRotation.x * curSpeed, 
            _offsetRotation.y * curSpeed, 
            _offsetRotation.z * curSpeed, 
            _offsetRotation.w * curSpeed
        );
        _rawGyroRotation.rotation = tempGyroRotation;
    }

    private Quaternion GyroToUnity(Quaternion gyro){
        return new Quaternion(gyro.x, gyro.y, -gyro.z, -gyro.w);
    }

    /// <summary>
    /// returns whether the device the code is running on
    /// has a gyroscope.
    /// </summary>
    /// <returns></returns>
    private static bool HasGyro(){
        return SystemInfo.supportsGyroscope;
    }

    public bool IsGyroEnabled()
    {
        return _gyroEnabled;
    }

    public Quaternion GetInitialRotation()
    {
        return _initialRotation;
    }

    /// <summary>
    /// returns the rotation of the gyroscope.
    /// </summary>
    /// <returns></returns>
    public virtual Quaternion GetCurrentRotation()
    {
        return _rawGyroRotation.rotation;
    }

    /// <summary>
    /// sets the gyroscope speed
    /// </summary>
    /// <param name="targetSpeed"></param>
    public void SetSpeed(float targetSpeed){
        speed = targetSpeed;
    }

    public float GetSpeed(){
        return speed;
    } 

    /* Used for calibrate gyro at start or during execution using UI button for exemple */
    public void Recalibrate(){
        Quaternion gyro = GyroToUnity(Input.gyro.attitude);
        _gyroInitialRotation.x = gyro.x;
        _gyroInitialRotation.y = gyro.y; // Fixed Y axis
        _gyroInitialRotation.z = gyro.z; // We rotate object on Y with Z axis gyro
        _gyroInitialRotation.w = gyro.w;
        print("Successfully recalibrated !");
    }

    void OnGUI () {
        if(debug){
            GUIStyle style = new GUIStyle();
            style.fontSize = Mathf.RoundToInt(Mathf.Min(Screen.width, Screen.height) / 20f);
            style.normal.textColor = Color.white;
            GUILayout.BeginVertical("box");
            GUILayout.Label("Attitude: " + Input.gyro.attitude.ToString(), style);
            GUILayout.Label("Rotation: " + transform.rotation.ToString(), style);
            GUILayout.Label("Offset Rotation: " + _offsetRotation.ToString(), style);
            GUILayout.Label("Raw Rotation: " + _rawGyroRotation.rotation.ToString(), style);
            GUILayout.EndVertical();
        }
    }
}