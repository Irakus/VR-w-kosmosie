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

    private const float MINIMAL_HANDLE_ROTATION = 60.0f;
    private const float HANDLE_ROTATION_MULTIPLIER = -60.0f;
    private const float YOKE_ROTATION_MULTIPLIER = 90.0f;
    private const float YOKE_MIDWAY_PULL = 0.48f;
    private const float YOKE_PULL_RANGE = 0.08f;

    [SerializeField] 
    private ControlMode _controlMode;

    [SerializeField]
    private Transform _rightHandle;

    [SerializeField]
    private Transform _leftHandle;

    [SerializeField]
    private Transform _yoke;

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
        ChangeYokePosition(Input.GetAxis(YokeTurn), Input.GetAxis(YokePull));
        _engineAccelerator.ThrottleRightEngine(Input.GetAxis(RightThrottleAxis));
        _engineAccelerator.ThrottleLeftEngine(Input.GetAxis(LeftThrottleAxis));
        _engineAccelerator.VerticalRotationEngine(Input.GetAxis(YokePull));
        _engineAccelerator.HorizontalRotationEngine(Input.GetAxis(YokeTurn));
    }

    private void ChangeHandlesPosition(Transform handle, float value)
    {
        handle.localRotation = Quaternion.Euler(handle.localEulerAngles.x, handle.localEulerAngles.y, MINIMAL_HANDLE_ROTATION + value * HANDLE_ROTATION_MULTIPLIER);
    }

    private void ChangeYokePosition(float rotation, float pull)
    {
        _yoke.localRotation = Quaternion.Euler(_yoke.localEulerAngles.x, _yoke.localEulerAngles.y, rotation * YOKE_ROTATION_MULTIPLIER);
        _yoke.localPosition = new Vector3(_yoke.localPosition.x, _yoke.localPosition.y, YOKE_MIDWAY_PULL + YOKE_PULL_RANGE * pull); 
    }

    private static void LogAxesInfo()
    {
        Debug.Log("RightThrottle is " + Input.GetAxis("RightThrottle"));
        Debug.Log("LeftThrottle is " + Input.GetAxis("LeftThrottle"));
        Debug.Log("LeftAnalogVertical is " + Input.GetAxis("LeftAnalogVertical"));
    }

}
