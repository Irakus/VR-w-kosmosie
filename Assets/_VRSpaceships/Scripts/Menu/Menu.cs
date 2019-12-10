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
    [SerializeField] private GameObject RacesButtons;
    private SpaceSceneLoader _sceneLoader;

    void Awake()
    {
        _sceneLoader = FindObjectOfType<SpaceSceneLoader>();
    }

    public void WolantButton()
    {
        PlayerInput.SetControlMode(PlayerInput.ControlMode.WOLANT);
        ControlButtons.SetActive(false);
        ModeButtons.SetActive(true);
    }

    public void GamepadButton()
    {
        PlayerInput.SetControlMode(PlayerInput.ControlMode.GAMEPAD);
        ControlButtons.SetActive(false);
        ModeButtons.SetActive(true);
    }

    public void RaceMode()
    {
        ModeButtons.SetActive(false);
        RacesButtons.SetActive(true);
    }

    public void DemoRace()
    {
        _sceneLoader.LoadScene("TimeRaceDemo");
    }

    public void Race1()
    {
        _sceneLoader.LoadScene("TimeRace");
    }

    public void BattleMode()
    {
        _sceneLoader.LoadScene("Battle");
    }
}
