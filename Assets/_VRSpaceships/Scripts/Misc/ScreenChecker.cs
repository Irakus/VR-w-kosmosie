using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScreenChecker : MonoBehaviour
{
    void Start()
    {

        if (Display.displays.Length >= 2)
        {
            Display.displays[1].Activate();
        }
    }

}
