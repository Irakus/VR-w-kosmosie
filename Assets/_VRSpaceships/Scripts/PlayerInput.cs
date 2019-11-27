using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public enum ControlMode
    {
        GAMEPAD,
        WOLANT
    };

    [SerializeField] public static ControlMode _controlMode;

    public static void SetControlMode(ControlMode controlMode)
    {
        _controlMode = controlMode;
        AxesDefinitions.ChangeController(controlMode);
    }

    [SerializeField] 
    private GameObject _hud;
    
    [SerializeField] 
    private Gun _gun1;
    
    [SerializeField] 
    private Gun _gun2;

    private EngineAccelerator _engineAccelerator;

    [SerializeField] private AudioSource _leftThrusterAudioSource;
    [SerializeField] private AudioSource _rightThrusterAudioSource;

    private Visualiser _controllerVisualiser;


    void Awake()
    {
        if (_controlMode == ControlMode.GAMEPAD)
        {
            _controllerVisualiser = GetComponent<GamePadVisualiser>();
        }
        else
        {
            _controllerVisualiser = GetComponent<ControllerVisualiser>();
        }

        _controllerVisualiser.AdjustControls();
        

        _engineAccelerator = GetComponent<EngineAccelerator>();
        
        AxesDefinitions.ChangeController(_controlMode);

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
        if (Input.GetButton(AxesDefinitions.Fire))
        {
            _gun1.Fire();
            _gun2.Fire();
            GetComponent<UserHUD>().ShowHP((int)Random.Range(0.0f,100.0f));
        }

        _controllerVisualiser.ChangeButtonPosition();
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
        _controllerVisualiser.ChangeHandlesPosition();
        _controllerVisualiser.ChangeYokePosition();
        _controllerVisualiser.ChangeCameraPosition();

        if(_controlMode == ControlMode.GAMEPAD)
        {
            _engineAccelerator.ThrottleRightEngine(Input.GetAxis(AxesDefinitions.RightThrottleAxis));
            _engineAccelerator.ThrottleLeftEngine(Input.GetAxis(AxesDefinitions.LeftThrottleAxis));
        }
        else
        {
            _engineAccelerator.ThrottleRightEngine(((Input.GetAxis(AxesDefinitions.RightThrottleAxis) + 1) / 2));
            _engineAccelerator.ThrottleLeftEngine(((Input.GetAxis(AxesDefinitions.LeftThrottleAxis) + 1) / 2));
        }
        _engineAccelerator.VerticalRotationEngine(Input.GetAxis(AxesDefinitions.YokePull));
        _engineAccelerator.HorizontalRotationEngine(Input.GetAxis(AxesDefinitions.YokeTurn));
        _rightThrusterAudioSource.volume = 0.5f + Input.GetAxis(AxesDefinitions.RightThrottleAxis) * 2f;
        _leftThrusterAudioSource.volume = 0.5f + Input.GetAxis(AxesDefinitions.LeftThrottleAxis) * 2f;
    }
    private void LogAxesInfo()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Debug.isDebugBuild)
        {

        }
    }

}
