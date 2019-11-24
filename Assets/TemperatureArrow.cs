using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureArrow : MonoBehaviour
{
    private Quaternion _startingRotation;
    private AudioSource _audioSource;

    public float Temperature { get; set; }
    
    void Start()
    {
        _startingRotation = transform.localRotation;
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        var targetRotationZ = Mathf.Lerp(0, 255, Temperature);
        transform.localRotation = _startingRotation * Quaternion.Euler(new Vector3(0,0, Mathf.Lerp(0, 255, Temperature)));
        if(Temperature>0.95f && !_audioSource.isPlaying)
            _audioSource.Play();
    }
}
