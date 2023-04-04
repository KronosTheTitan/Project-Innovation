using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using Unity.Mathematics;

public class KeyboardControls : GyroControls
{
    private Quaternion _currentRotation = quaternion.identity;

    protected override void Update()
    {
        
    }

    public override Quaternion GetCurrentRotation()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _currentRotation *= Quaternion.Euler(Vector3.right * 10);
            Debug.Log("Up");
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log(_currentRotation);
            _currentRotation *= Quaternion.Euler(Vector3.left * 10);
            Debug.Log(_currentRotation);
            Debug.Log("Down");
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _currentRotation *= Quaternion.Euler(Vector3.down * 10);
            Debug.Log("Left");
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _currentRotation *= Quaternion.Euler(Vector3.up * 10);
            Debug.Log("Right");
        }
        
        //Debug.Log("test");

        return _currentRotation;
    }
}