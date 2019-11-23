using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolantControls : MonoBehaviour
{
    enum ControlMode
    {
        GAMEPAD,
        WOLANT
    };

    private const float MINIMAL_ENGINE_THROTTLE = 0.0f;
    private const float ENGINE_THROTTLE = 500.0f;
    private const float MINIMAL_HANDLE_ROTATION = -50.0f;
    private const float HANDLE_ROTATION_MULTIPLIER = 100.0f;
    private const float ROTATION_MULTIPLIER = 100.0f;

    [SerializeField]
    private Transform _leftEngine;

    [SerializeField]
    private Transform _rightEngine;

    [SerializeField]
    private Transform _bow;

    [SerializeField]
    private Transform _rightHandle;

    [SerializeField]
    private Transform _leftHandle;

    [SerializeField] 
    private ControlMode controlMode;


    private Rigidbody _rigidbody;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        LogAxesInfo();
        ChangeHandlesPosition(_rightHandle, Input.GetAxis("RightThrottle"));
        ChangeHandlesPosition(_leftHandle, Input.GetAxis("LeftThrottle"));
        ApplyForceToEngine(_rightEngine, Input.GetAxis("RightThrottle"));
        ApplyForceToEngine(_leftEngine, Input.GetAxis("LeftThrottle"));
        RotateShip();
    }

    private static void LogAxesInfo()
    {
        Debug.Log("RightThrottle is " + Input.GetAxis("RightThrottle"));
        Debug.Log("LeftThrottle is " + Input.GetAxis("LeftThrottle"));
        Debug.Log("LeftAnalogVertical is " + Input.GetAxis("LeftAnalogVertical"));
    }

    private void ChangeHandlesPosition(Transform handle, float axisValue)
    {
        handle.localRotation = Quaternion.Euler(MINIMAL_HANDLE_ROTATION + axisValue * HANDLE_ROTATION_MULTIPLIER, handle.localEulerAngles.y, handle.localEulerAngles.z);
    }

    private void ApplyForceToEngine(Transform engine, float axisValue)
    {
        float forceStrength = CalculateForceStrength(axisValue);
        Vector3 forward = transform.forward;
        _rigidbody.AddForceAtPosition(forward.normalized * forceStrength,engine.position,ForceMode.Force);
    }

    private float CalculateForceStrength(float axisValue)
    {
        if (controlMode == ControlMode.WOLANT)
        {
            return MINIMAL_ENGINE_THROTTLE + ENGINE_THROTTLE * ((axisValue + 1) / 2);
        }
        else
        {
            return MINIMAL_ENGINE_THROTTLE + ENGINE_THROTTLE * axisValue;
        }
    }

    private void RotateShip()
    {
        float verticalRotationStrength = ROTATION_MULTIPLIER * Input.GetAxis("LeftAnalogVertical");
        _rigidbody.AddForceAtPosition(transform.up.normalized * verticalRotationStrength, _bow.position, ForceMode.Force);

        float horizontalRotationStrength = ROTATION_MULTIPLIER * Input.GetAxis("LeftAnalogHorizontal");
        _rigidbody.AddForceAtPosition(transform.up * horizontalRotationStrength, _leftEngine.position, ForceMode.Force);
        _rigidbody.AddForceAtPosition(-transform.up * horizontalRotationStrength, _rightEngine.position, ForceMode.Force);
    }
}
