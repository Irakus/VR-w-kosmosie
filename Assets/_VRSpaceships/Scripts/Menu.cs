using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject ControlButtons;
    [SerializeField] private GameObject ModeButtons;

    public void WolantButton()
    {
        PlayerInput.SetControlMode(PlayerInput.ControlMode.WOLANT);
        ControlButtons.SetActive(false);
        ModeButtons.SetActive(true);
        FindObjectOfType<EventSystem>().SetSelectedGameObject(ModeButtons.GetComponentInChildren<Button>().gameObject);
    }

    public void GamepadButton()
    {
        PlayerInput.SetControlMode(PlayerInput.ControlMode.GAMEPAD);
        ControlButtons.SetActive(false);
        ModeButtons.SetActive(true);
        FindObjectOfType<EventSystem>().SetSelectedGameObject(ModeButtons.GetComponentInChildren<Button>().gameObject);
    }

    public void RaceMode()
    {
        SceneManager.LoadScene("ShipScene");
    }

    public void BattleMode()
    {
        SceneManager.LoadScene("ShipScene");
    }
}
