using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsFuncs : MonoBehaviour
{
    // Start is called before the first frame update
    public void WolantButton()
    {
        PlayerInput.SetControlMode(PlayerInput.ControlMode.WOLANT);
        SceneManager.LoadScene("ShipScene");
    }

    public void GamepadButton()
    {
        PlayerInput.SetControlMode(PlayerInput.ControlMode.GAMEPAD);
        SceneManager.LoadScene("ShipScene");
    }
}
