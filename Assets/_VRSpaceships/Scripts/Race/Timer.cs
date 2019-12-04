using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float _totalTime;
    private bool _isRunning;
    private TextMeshPro _timerText;

    void Start()
    {
        _timerText = GetComponentInChildren<TextMeshPro>();
    }
    public void StartTimer()
    {
        _isRunning = true;
        _totalTime = 0.0f;
    }

    public void StopTimer()
    {
        _isRunning = false;
    }

    void Update()
    {
        if (_isRunning)
        {
            _totalTime += Time.deltaTime;
            VisualiseTime();
        }
    }

    public float GetTime()
    {
        return _totalTime; 
    }

    private void VisualiseTime()
    {
        _timerText.text = TimeConverter.ConvertTimeToString(_totalTime);
    }
}
