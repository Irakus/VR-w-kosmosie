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
    private Camera UpCamera;
    [SerializeField]
    private Camera BackCamera;

    private bool useTPP = false;
    void Start()
    {
        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            useTPP = !useTPP;
            TPPCamera.targetDisplay = useTPP ? 0 : 7;
        }
    }
}
