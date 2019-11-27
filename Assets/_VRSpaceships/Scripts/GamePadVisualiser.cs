using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePadVisualiser : Visualiser
{
    private const float ANALOG_STICK_RANGE = 15.0f;
    private const float TRIGGER_RANGE= -17.0f;
    private const float CAMERA_HORIZONTAL_RANGE = 110.0f;
    private const float CAMERA_VERTICAL_RANGE = 90.0f;
    private const float BUTTONS_RANGE = -0.0028f;

    [SerializeField]
    private Transform _rightTrigger;

    [SerializeField]
    private Transform _leftTrigger;

    [SerializeField]
    private Transform _camera;
    [SerializeField]
    private Transform _greenButton;

    [SerializeField]
    private Transform _leftAnalog;
    [SerializeField]
    private Transform _rightAnalog;


    public override void ChangeHandlesPosition()
    {
        if (PlayerInput._controlMode == PlayerInput.ControlMode.GAMEPAD)
        {
            ChangeHandlePosition(_rightTrigger, Input.GetAxis(AxesDefinitions.RightThrottleAxis));
            ChangeHandlePosition(_leftTrigger, Input.GetAxis(AxesDefinitions.LeftThrottleAxis));
        }
    }
    private void ChangeHandlePosition(Transform handle, float value)
    {
        handle.localRotation = Quaternion.Euler(value * TRIGGER_RANGE, handle.localEulerAngles.y, handle.localEulerAngles.z);
    }

    public override void ChangeYokePosition()
    {
        float rotation = Input.GetAxis(AxesDefinitions.YokeTurn);
        float pull = Input.GetAxis(AxesDefinitions.YokePull);
        _leftAnalog.localRotation = Quaternion.Euler(pull * ANALOG_STICK_RANGE, _leftAnalog.localEulerAngles.y, rotation * ANALOG_STICK_RANGE);
        
    }

    public override void ChangeCameraPosition()
    {
        if (PlayerInput._controlMode == PlayerInput.ControlMode.GAMEPAD)
        {
            float vertical = Input.GetAxis(AxesDefinitions.CameraVerical);
            float horizontal = Input.GetAxis(AxesDefinitions.CameraHorizontal);
            _camera.localRotation = Quaternion.Euler(vertical * CAMERA_VERTICAL_RANGE, horizontal * CAMERA_HORIZONTAL_RANGE, _camera.localEulerAngles.z);
            _rightAnalog.localRotation = Quaternion.Euler(vertical * ANALOG_STICK_RANGE, _rightAnalog.localEulerAngles.y, horizontal * ANALOG_STICK_RANGE);
        }
    }

    public override void ChangeButtonPosition()
    {
        if (Input.GetButton(AxesDefinitions.Fire))
        {
            _greenButton.localPosition = new Vector3(_greenButton.localPosition.x, -0.00228f, 0.0004f);
        }
        else
        {
            _greenButton.localPosition = new Vector3(_greenButton.localPosition.x, 0.0f, 0.0f);
        }
    }


    [SerializeField] protected GameObject Yoke;
    [SerializeField] protected GameObject Pad;

    public override void AdjustControls()
    {
        if (PlayerInput._controlMode == PlayerInput.ControlMode.GAMEPAD)
        {
            Pad.SetActive(true);
            Yoke.SetActive(false);
        }
        else
        {
            Pad.SetActive(false);
            Yoke.SetActive(true);
        }
    }
}
