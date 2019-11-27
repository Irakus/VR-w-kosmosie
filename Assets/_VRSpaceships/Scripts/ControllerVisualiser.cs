using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerVisualiser : MonoBehaviour
{
    private const float MINIMAL_HANDLE_ROTATION = 60.0f;
    private const float HANDLE_ROTATION_MULTIPLIER = -60.0f;
    private const float YOKE_ROTATION_MULTIPLIER = 90.0f;
    private const float YOKE_MIDWAY_PULL = 0.48f;
    private const float YOKE_PULL_RANGE = 0.08f;
    private const float CAMERA_HORIZONTAL_RANGE = 110.0f;
    private const float CAMERA_VERTICAL_RANGE = 90.0f;

    [SerializeField]
    private static Transform _rightHandle;

    [SerializeField]
    private static Transform _leftHandle;

    [SerializeField]
    private static Transform _camera;
    [SerializeField]
    private static Transform _redButton;

    [SerializeField]
    private static Transform _yoke;


    public static void ChangeHandlesPosition()
    {
        if (PlayerInput._controlMode == PlayerInput.ControlMode.GAMEPAD)
        {
            ChangeHandlePosition(_rightHandle, Input.GetAxis(AxesDefinitions.RightThrottleAxis));
            ChangeHandlePosition(_leftHandle, Input.GetAxis(AxesDefinitions.LeftThrottleAxis));
        }
        else
        {
            ChangeHandlePosition(_rightHandle, ((Input.GetAxis(AxesDefinitions.RightThrottleAxis) + 1) / 2));
            ChangeHandlePosition(_leftHandle, ((Input.GetAxis(AxesDefinitions.LeftThrottleAxis) + 1) / 2));
        }
    }
    private static void ChangeHandlePosition(Transform handle, float value)
    {
        handle.localRotation = Quaternion.Euler(handle.localEulerAngles.x, handle.localEulerAngles.y, MINIMAL_HANDLE_ROTATION + value * HANDLE_ROTATION_MULTIPLIER);
    }

    public static void ChangeYokePosition()
    {
        float rotation = Input.GetAxis(AxesDefinitions.YokeTurn);
        float pull = Input.GetAxis(AxesDefinitions.YokePull);
        _yoke.localRotation = Quaternion.Euler(_yoke.localEulerAngles.x, _yoke.localEulerAngles.y, rotation * YOKE_ROTATION_MULTIPLIER);
        _yoke.localPosition = new Vector3(_yoke.localPosition.x, _yoke.localPosition.y, YOKE_MIDWAY_PULL + YOKE_PULL_RANGE * pull);
    }

    public static void ChangeCameraPosition()
    {
        if (PlayerInput._controlMode == PlayerInput.ControlMode.GAMEPAD)
        {
            float vertical = Input.GetAxis(AxesDefinitions.CameraVerical);
            float horizontal = Input.GetAxis(AxesDefinitions.CameraHorizontal);
            _camera.localRotation = Quaternion.Euler(vertical * CAMERA_VERTICAL_RANGE, horizontal * CAMERA_HORIZONTAL_RANGE, _camera.localEulerAngles.z);
        }
    }

    public static void ChangeButtonPosition
}
