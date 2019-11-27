using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerVisualiser : Visualiser
{
    private const float MINIMAL_HANDLE_ROTATION = 60.0f;
    private const float HANDLE_ROTATION_MULTIPLIER = -60.0f;
    private const float YOKE_ROTATION_MULTIPLIER = 90.0f;
    private const float YOKE_MIDWAY_PULL = 0.48f;
    private const float YOKE_PULL_RANGE = 0.08f;
    private const float CAMERA_HORIZONTAL_RANGE = 110.0f;
    private const float CAMERA_VERTICAL_RANGE = 90.0f;

    [SerializeField]
    private Transform _rightHandle;

    [SerializeField]
    private Transform _leftHandle;

    [SerializeField]
    private Transform _thirdHandle;

    [SerializeField]
    private Transform _redButton;

    [SerializeField]
    private Transform _yoke;
    public override void ChangeHandlesPosition()
    {
        ChangeHandlePosition(_rightHandle, ((Input.GetAxis(AxesDefinitions.RightThrottleAxis) + 1) / 2));
        ChangeHandlePosition(_leftHandle, ((Input.GetAxis(AxesDefinitions.LeftThrottleAxis) + 1) / 2));
        ChangeHandlePosition(_thirdHandle, ((Input.GetAxis(AxesDefinitions.ThirdHandleAxis) + 1) / 2));
    }
    private void ChangeHandlePosition(Transform handle, float value)
    {
        handle.localRotation = Quaternion.Euler(handle.localEulerAngles.x, handle.localEulerAngles.y, MINIMAL_HANDLE_ROTATION + value * HANDLE_ROTATION_MULTIPLIER);
    }
    public override void ChangeYokePosition()
    {
        float rotation = Input.GetAxis(AxesDefinitions.YokeTurn);
        float pull = Input.GetAxis(AxesDefinitions.YokePull);
        _yoke.localRotation = Quaternion.Euler(_yoke.localEulerAngles.x, _yoke.localEulerAngles.y, rotation * YOKE_ROTATION_MULTIPLIER);
        _yoke.localPosition = new Vector3(_yoke.localPosition.x, _yoke.localPosition.y, YOKE_MIDWAY_PULL + YOKE_PULL_RANGE * pull);
    }
    public override void ChangeCameraPosition()
    {
        
    }
    public override void ChangeButtonPosition()
    {
        if (Input.GetButton(AxesDefinitions.Fire))
        {
            _redButton.localPosition = new Vector3(0.08543f, 0.12449f, -0.00106f);
        }
        else
        {
            _redButton.localPosition = new Vector3(0.08506003f, 0.12525f, -0.00272f);
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
