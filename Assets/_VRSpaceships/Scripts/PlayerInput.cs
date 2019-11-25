﻿using System.Collections;
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
    private const float CAMERA_HORIZONTAL_RANGE = 110.0f;
    private const float CAMERA_VERTICAL_RANGE = 90.0f;

    [SerializeField] 
    private ControlMode _controlMode;

    [SerializeField]
    private Transform _rightHandle;

    [SerializeField]
    private Transform _leftHandle;

    [SerializeField]
    private Transform _camera;

    [SerializeField]
    private Transform _yoke;

    [SerializeField] 
    private GameObject _hud;
    
    [SerializeField] 
    private Gun _gun1;
    
    [SerializeField] 
    private Gun _gun2;
    
    private EngineAccelerator _engineAccelerator;

    private string RightThrottleAxis;
    private string LeftThrottleAxis;
    private string YokeTurn;
    private string YokePull;
    private string CameraVerical;
    private string CameraHorizontal;
    [SerializeField] private AudioSource _leftThrusterAudioSource;
    [SerializeField] private AudioSource _rightThrusterAudioSource;


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
                CameraHorizontal = "RightAnalogHorizontal";
                CameraVerical = "RightAnalogVertical";
                break;
            case ControlMode.WOLANT:
                RightThrottleAxis = "RightThrottleYoke";
                LeftThrottleAxis = "LeftThrottleYoke";
                YokeTurn = "YokeTurn";
                YokePull = "YokePull";
                CameraHorizontal = "RightAnalogHorizontal";
                CameraVerical = "RightAnalogVertical";
                break;
            default:
                break;
        }

        if (_controlMode == ControlMode.WOLANT)
        {
            _hud.SetActive(false);
        }
    }

    void Update()
    {
        HUDControls();
        GunControls();
    }

    private void GunControls()
    {
        if (Input.GetButton("Fire1"))
        {
            _gun1.Fire();
            _gun2.Fire();
        }
    }

    private void HUDControls()
    {
        if (Input.GetButtonDown("HUDhide") && _controlMode == ControlMode.GAMEPAD)
        {
            _hud.SetActive(!_hud.activeSelf);
        }
    }

    void FixedUpdate()
    {
        LogAxesInfo();
        if (_controlMode == ControlMode.GAMEPAD)
        {
            ChangeHandlesPosition(_rightHandle, Input.GetAxis(RightThrottleAxis));
            ChangeHandlesPosition(_leftHandle, Input.GetAxis(LeftThrottleAxis));
        }
        else
        {
            ChangeHandlesPosition(_rightHandle, ((Input.GetAxis(RightThrottleAxis) + 1) / 2));
            ChangeHandlesPosition(_leftHandle, ((Input.GetAxis(LeftThrottleAxis) + 1) / 2));
        }
        
        ChangeYokePosition(Input.GetAxis(YokeTurn), Input.GetAxis(YokePull));
        ChangeCameraPosition(Input.GetAxis(CameraVerical), Input.GetAxis(CameraHorizontal));
        if(_controlMode == ControlMode.GAMEPAD)
        {
            _engineAccelerator.ThrottleRightEngine(Input.GetAxis(RightThrottleAxis));
            _engineAccelerator.ThrottleLeftEngine(Input.GetAxis(LeftThrottleAxis));
        }
        else
        {
            _engineAccelerator.ThrottleRightEngine(((Input.GetAxis(RightThrottleAxis) + 1) / 2));
            _engineAccelerator.ThrottleLeftEngine(((Input.GetAxis(LeftThrottleAxis) + 1) / 2));
        }
        _engineAccelerator.VerticalRotationEngine(Input.GetAxis(YokePull));
        _engineAccelerator.HorizontalRotationEngine(Input.GetAxis(YokeTurn));
        _rightThrusterAudioSource.volume = 0.5f + Input.GetAxis(RightThrottleAxis) * 2f;
        _leftThrusterAudioSource.volume = 0.5f + Input.GetAxis(LeftThrottleAxis) * 2f;
    }

    private void ChangeCameraPosition(float vertical, float horizontal)
    {
        if (_controlMode != ControlMode.WOLANT)
        {
            _camera.localRotation = Quaternion.Euler(vertical * CAMERA_VERTICAL_RANGE, horizontal * CAMERA_HORIZONTAL_RANGE, _camera.localEulerAngles.z);
        }
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

    private void LogAxesInfo()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Debug.isDebugBuild)
        {
            //            Debug.Log("RightThrottle is " + Input.GetAxis("RightThrottle"));
            //            Debug.Log("LeftThrottle is " + Input.GetAxis("LeftThrottle"));
            //            Debug.Log("LeftAnalogVertical is " + Input.GetAxis("LeftAnalogVertical"));
            Debug.Log(Input.GetAxis(RightThrottleAxis) + " " 
                                                       + Input.GetAxis(LeftThrottleAxis) + " " 
                                                       + Input.GetAxis(YokeTurn) + " " 
                                                       + Input.GetAxis(YokePull) );
        }
    }

}
