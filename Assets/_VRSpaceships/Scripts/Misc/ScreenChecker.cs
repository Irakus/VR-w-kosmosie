using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.XR;
using UnityEngine.XR;


public class ScreenChecker : MonoBehaviour
{
    enum CAMERA_MODES  {
        USER,ENEMY
    };
    [SerializeField]
    private int _userIndex = 0;
    [SerializeField]
    private List<UserCamera> _userCameras;
    [SerializeField]
    private int _enemyIndex = 0;
    [SerializeField]
    private List<EnemyCamera> _enemyCameras;

    [SerializeField]
    private CAMERA_MODES _cameraMode = CAMERA_MODES.USER;

    private const int UNUSED_DISPLAY = 7;
    private const int MAIN_DISPLAY = 0;
    void Start()
    {
        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }

        _userCameras = new List<UserCamera>(FindObjectsOfType<UserCamera>());
        //_enemyCameras = new List<EnemyCamera>(FindObjectsOfType<EnemyCamera>());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            switch (_cameraMode)
            {
                case CAMERA_MODES.USER:
                    _userCameras[_userIndex].GetComponent<Camera>().targetDisplay = UNUSED_DISPLAY;
                    break;
                case CAMERA_MODES.ENEMY:
                    _enemyCameras[_enemyIndex].GetComponent<Camera>().targetDisplay = UNUSED_DISPLAY;
                    break;
                default:
                    break;
            }
            _cameraMode += 1;
            _cameraMode = (CAMERA_MODES)((int)_cameraMode % Enum.GetNames(typeof(CAMERA_MODES)).Length);
            switch (_cameraMode)
            {
                case CAMERA_MODES.USER:
                    _userCameras[_userIndex].GetComponent<Camera>().targetDisplay = MAIN_DISPLAY;
                    break;
                case CAMERA_MODES.ENEMY:
                    _enemyCameras[_enemyIndex].GetComponent<Camera>().targetDisplay = MAIN_DISPLAY;
                    break;
                default:
                    break;
            }
        }
        else
        if (Input.GetKeyDown(KeyCode.RightArrow) )
        {
            switch (_cameraMode)
            {
                case CAMERA_MODES.USER:
                    _userCameras[_userIndex].GetComponent<Camera>().targetDisplay = UNUSED_DISPLAY;
                    _userIndex += 1;
                    _userIndex %= _userCameras.Count;
                    _userCameras[_userIndex].GetComponent<Camera>().targetDisplay = MAIN_DISPLAY;
                    break;
                case CAMERA_MODES.ENEMY:
                    _enemyCameras[_enemyIndex].GetComponent<Camera>().targetDisplay = UNUSED_DISPLAY;
                    _enemyIndex += 1;
                    _enemyIndex %= _enemyCameras.Count;
                    _enemyCameras[_enemyIndex].GetComponent<Camera>().targetDisplay = MAIN_DISPLAY;
                    break;
                default:

                    break;
            }
        }
        else
        if ( Input.GetKeyDown(KeyCode.LeftArrow))
        {
            switch (_cameraMode)
            {
                case CAMERA_MODES.USER:
                    _userCameras[_userIndex].GetComponent<Camera>().targetDisplay = UNUSED_DISPLAY;
                    _userIndex -= 1;
                    if (_userIndex == -1) _userIndex = _userCameras.Count-1;
                    _userIndex %= _userCameras.Count;
                    _userCameras[_userIndex].GetComponent<Camera>().targetDisplay = MAIN_DISPLAY;
                    break;
                case CAMERA_MODES.ENEMY:
                    _enemyCameras[_enemyIndex].GetComponent<Camera>().targetDisplay = UNUSED_DISPLAY;
                    _enemyIndex -= 1;
                    if (_enemyIndex == -1) _enemyIndex = _enemyCameras.Count-1;
                    _enemyIndex %= _enemyCameras.Count;
                    _enemyCameras[_enemyIndex].GetComponent<Camera>().targetDisplay = MAIN_DISPLAY;
                    break;
                default:

                    break;
            }
        }
    }

    public void RegisterEnemyCamera(EnemyCamera newCamera)
    {
        _enemyCameras.Add(newCamera);
    }
    public void UnregisterEnemyCamera(EnemyCamera cameraToRemove)
    {
        _enemyCameras.Remove(cameraToRemove);
    }
}
