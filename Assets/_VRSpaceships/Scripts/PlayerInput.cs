using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    enum ControlMode
    {
        GAMEPAD,
        WOLANT
    };

    private const float MINIMAL_HANDLE_ROTATION = -50.0f;
    private const float HANDLE_ROTATION_MULTIPLIER = 100.0f;

    [SerializeField] 
    private ControlMode _controlMode;

    [SerializeField]
    private Transform _rightHandle;

    [SerializeField]
    private Transform _leftHandle;

    private EngineAccelerator _engineAccelerator;

    private string RightThrottleAxis;
    private string LeftThrottleAxis;
    private string YokeTurn;
    private string YokePull;

    void Awake()
    {
        _engineAccelerator = GetComponent<EngineAccelerator>();
        switch (_controlMode)
        {
            case ControlMode.GAMEPAD:
                RightThrottleAxis = "RightThrottle";
                LeftThrottleAxis = "LeftThrottle";
                YokeTurn = "LeftAnalogHorizontal";
                YokePull = "LeftAnalogVertical";
                break;
            case ControlMode.WOLANT:

                break;
            default:
                break;
        }
    }
    void FixedUpdate()
    {
        LogAxesInfo();
        ChangeHandlesPosition(_rightHandle, Input.GetAxis(RightThrottleAxis));
        ChangeHandlesPosition(_leftHandle, Input.GetAxis(LeftThrottleAxis));
        _engineAccelerator.ThrottleRightEngine(Input.GetAxis(RightThrottleAxis));
        _engineAccelerator.ThrottleLeftEngine(Input.GetAxis(LeftThrottleAxis));
        _engineAccelerator.VerticalRotationEngine(Input.GetAxis(YokePull));
        _engineAccelerator.HorizontalRotationEngine(Input.GetAxis(YokeTurn));
    }

    private void ChangeHandlesPosition(Transform handle, float value)
    {
        handle.localRotation = Quaternion.Euler(MINIMAL_HANDLE_ROTATION + value * HANDLE_ROTATION_MULTIPLIER, handle.localEulerAngles.y, handle.localEulerAngles.z);
    }

    private static void LogAxesInfo()
    {
        Debug.Log("RightThrottle is " + Input.GetAxis("RightThrottle"));
        Debug.Log("LeftThrottle is " + Input.GetAxis("LeftThrottle"));
        Debug.Log("LeftAnalogVertical is " + Input.GetAxis("LeftAnalogVertical"));
    }

}
