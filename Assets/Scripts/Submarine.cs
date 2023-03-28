using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Submarine : MonoBehaviour
{
    /// <summary>
    /// The minimum speed, make negative to allow for reverse
    /// </summary>
    [SerializeField] private float minSpeed = -1;
    
    /// <summary>
    /// maximum speed, name is pretty self explenatory
    /// </summary>
    [SerializeField] private float maxSpeed = 5;
    
    /// <summary>
    /// maximum change in pitch, meaning the rotation along the vertical axis
    /// </summary>
    [SerializeField] private float maxPitchSpeed = 3;
    
    /// <summary>
    /// maximum change in yaw, meaning the rotation along the horizontal axis
    /// </summary>
    [SerializeField] private float maxTurnSpeed = 50;
    
    /// <summary>
    /// used for storing the current acceleration
    /// </summary>
    [SerializeField] private float acceleration = 2;

    /// <summary>
    /// used to smooth out the change in velocity (acceleration)
    /// </summary>
    [SerializeField] private float smoothSpeed = 3;
    
    /// <summary>
    /// used to smooth out the change in rotation
    /// </summary>
    [SerializeField] private float smoothTurnSpeed = 3;

    /// <summary>
    /// the transform belonging to the object used for the propeller.
    /// Rotation speed changes based on speedPercent inside the update method.
    /// This is only a visual effect, no gameplay changes based on it.
    /// </summary>
    [SerializeField] private Transform propeller;
    
    /// <summary>
    /// the transform used for displaying changes in the horizontally
    /// placed pitch rudder. to display movement up and down.
    /// This is only a visual effect, no gameplay changes based on it.
    /// </summary>
    [SerializeField] private Transform rudderPitch;
    
    /// <summary>
    /// the transform used to control the vertically placed yaw rudder
    /// to display movements to the left and right.
    /// This is only a visual effect, no gameplay changes based on it.
    /// </summary>
    [SerializeField] private Transform rudderYaw;
    
    /// <summary>
    /// controls the rotation speed of the rear propeller.
    /// This is only a visual effect, no gameplay changes based on it.
    /// </summary>
    [SerializeField] private float propellerSpeedFac = 2;
    
    /// <summary>
    /// the maximum angle changes for the rudders (pitch and yaw)
    /// </summary>
    [SerializeField] private float rudderAngle = 30;

    /// <summary>
    /// stores the current velocity
    /// </summary>
    Vector3 velocity;
    
    /// <summary>
    /// the yaw velocity, how much it is currently rotation along the horizontal axis.
    /// </summary>
    float yawVelocity;
    
    /// <summary>
    /// the pitch velocity, how much it is currently rotation along the Vertical axis.
    /// </summary>
    float pitchVelocity;
    
    /// <summary>
    /// the current speed of the submarine, effectively the length of the velocity vector.
    /// even though its length is based on this variable.
    /// assigned during the update method.
    /// </summary>
    float currentSpeed;
    
    /// <summary>
    /// the material is used to control the transparency of the propeller to create
    /// a sort of semi motion blur effect.
    /// </summary>
    [SerializeField] private Material propSpinMat;

    /// <summary>
    /// the rigidbody is a type of collider in this case used to control
    /// the movement of the submarine.
    /// </summary>
    [SerializeField] private Rigidbody _rigidbody;

    /// <summary>
    /// the menu that opens up when the player loses the game
    /// </summary>
    [SerializeField] private GameObject deathMenu;

    /// <summary>
    /// the variable keeping track of the parent object
    /// used for the pause menu.
    /// </summary>
    [SerializeField] private GameObject ingameMenu;
    
    public float speedPercent { get; private set; }

    private bool dead = false;
    
    void Update () {
        
        //TODO: fuel system
        
        //acceleration
        float accelDir = 0;
        
        //take inputs for increasing and decreasing speed.
        if (Input.GetKey (KeyCode.Q)) {
            //decrease speed
            accelDir -= 1;
        }
        if (Input.GetKey (KeyCode.E)) {
            //increase speed
            accelDir += 1;
        }
        
        //add acceleration to the currentSpeed. Accounts for framerate.
        currentSpeed += acceleration * Time.deltaTime * accelDir;
        //clamp currentSpeed between 0 and the maximum speed.
        currentSpeed = Mathf.Clamp (currentSpeed, minSpeed, maxSpeed);
        
        //calculate percentage of max speed reached.
        speedPercent = currentSpeed / maxSpeed;

        //set the target velocity (velocity to be reached over time)
        // equal to the forward direction multiplied by the current speed.
        Vector3 targetVelocity = transform.forward * currentSpeed;
        
        //set actual velocity based on an interpolation between the current speed, the target and time multiplied by
        //smoothing. this creates a more smoothed out change in velocity.
        velocity = Vector3.Lerp (velocity, targetVelocity, Time.deltaTime * smoothSpeed);
        
        //set the desired pitch to the vertical axis (W for up, S for down) multiplied by the max pitch speed.
        float targetPitchVelocity = Input.GetAxisRaw ("Vertical") * maxPitchSpeed;
        
        //set actual pitch velocity based on an interpolation between the current pitch velocity, the target pitch velocity
        //and time multiplied by smoothing. this creates a more smoothed out change in velocity.
        pitchVelocity = Mathf.Lerp (pitchVelocity, targetPitchVelocity, Time.deltaTime * smoothTurnSpeed);
        
        //set the desired yaw to the horizontal axis (W for up, S for down) multiplied by the max yaw speed.
        float targetYawVelocity = Input.GetAxisRaw ("Horizontal") * maxTurnSpeed;
        
        //set actual pitch velocity based on an interpolation between the current pitch velocity, the target pitch velocity
        //and time multiplied by smoothing. this creates a more smoothed out change in velocity.
        yawVelocity = Mathf.Lerp (yawVelocity, targetYawVelocity, Time.deltaTime * smoothTurnSpeed);
        
        //create the new rotation based on the speed, time and target rotations
        Vector3 newRot = (Vector3.up * yawVelocity + Vector3.left * pitchVelocity) * (Time.deltaTime * speedPercent);
        
        //add the current rotation to the new rotation
        newRot += transform.localEulerAngles;
        
        //lock the torque to ensure that it doesn't freak out in some collisions
        newRot.z = 0;
        
        //apply the calculated velocity changes to the submarines rotation.
        transform.eulerAngles = newRot;
        
        //apply the movement velocity, rigidbody will handle the forward movement and collision.
        _rigidbody.velocity = targetVelocity;

        //VISUAL EFFECTS
        //set the yaw rudder, rotate it based on the turn speed and the max turn speed
        rudderYaw.localEulerAngles = Vector3.up * yawVelocity / maxTurnSpeed * rudderAngle;
        //set the pitch rudder, rotate it based on the turn speed and the max turn speed
        rudderPitch.localEulerAngles = Vector3.left * pitchVelocity / maxPitchSpeed * rudderAngle;

        //rotate the propeller based on the speed percentage.
        propeller.Rotate (Vector3.forward * (Time.deltaTime * propellerSpeedFac * speedPercent), Space.Self);
        //propSpinMat.color = new Color (propSpinMat.color.r, propSpinMat.color.g, propSpinMat.color.b, speedPercent * .3f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _rigidbody.velocity = new Vector3();
        velocity = new Vector3();
    }
}