﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    [SerializeField] private List<RaceRing> _rings;
    private EngineAccelerator playerEngineAccelerator;
    private Timer _timer;

    private int _currentRing;

    [SerializeField] private VirtualKeyboard keyboard;
    void Start()
    {
        playerEngineAccelerator = FindObjectOfType<EngineAccelerator>();
        playerEngineAccelerator.TurnOffEngines();
        StartCoroutine(CountDown());
        _timer = FindObjectOfType<Timer>();
    }

    void StartRace()
    {
        _currentRing = 0;
        _rings[_currentRing].ActivateRing(this);
        _timer.StartTimer();
    }

    public void NextRing()
    {
        _currentRing += 1;
        if (_rings.Count == _currentRing)
        {
            EndRace();
        }
        else
        {
            _rings[_currentRing].ActivateRing(this);
        }
    }

    private void EndRace()
    {
        playerEngineAccelerator.TurnOffEngines();
        _timer.StopTimer();
        keyboard.gameObject.SetActive(true);
        keyboard.GetNickname(this);
    }

    public void ContinueEnding(string nickName)
    {
        keyboard.gameObject.SetActive(false);
        FindObjectOfType<HighScoreManager>().ShowScores(new PlayerScore(nickName, _timer.GetTime()));
    }

    [SerializeField]
    private TextMeshProUGUI _text;
    private const float TEXT_MAX_SIZE = 0.82f;
    private const float TEXT_MIN_SIZE = 0.02f;
    private const float TEXT_DELTA_SIZE = (TEXT_MAX_SIZE - TEXT_MIN_SIZE) / 100;
    IEnumerator CountDown()
    {
        int countdownTime = 5;
        _text.text = countdownTime.ToString();
        _text.fontSize = TEXT_MIN_SIZE;
        while (countdownTime > 0)
        {
            _text.fontSize += TEXT_DELTA_SIZE;
            yield return new WaitForSeconds(0.01f);
            if (_text.fontSize >= TEXT_MAX_SIZE)
            {
                countdownTime -= 1;
                _text.fontSize = TEXT_MIN_SIZE;
                _text.text = countdownTime.ToString();
            }
        }

        _text.text = "GO!";
        playerEngineAccelerator.TurnOnEngines();
        _text.fontSize = TEXT_MAX_SIZE;
        StartRace();
        yield return new WaitForSeconds(1.0f);
        _text.gameObject.SetActive(false);

    }
}
