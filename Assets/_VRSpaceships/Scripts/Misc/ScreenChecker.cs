using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.XR;
using UnityEngine.XR;


public class ScreenChecker : MonoBehaviour
{
    [SerializeField]
    private Camera TPPCamera;
    [SerializeField]
    private Camera SideCamera;
    void Start()
    {

        if (Display.displays.Length >= 2)
        {
            Display.displays[1].Activate();
        }

        if (!Application.isEditor && !Debug.isDebugBuild && XRDevice.isPresent)
        {
            
            TPPCamera.targetDisplay = 0;
            SideCamera.targetDisplay = 1;
        }
    }

}
