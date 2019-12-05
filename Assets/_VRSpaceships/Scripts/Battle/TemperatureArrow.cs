using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureArrow : MonoBehaviour
{
    private Quaternion _startingRotation;
    private AudioSource _audioSource;
    [SerializeField] private Gun gun;

    void Start()
    {
        _startingRotation = transform.localRotation;
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
        var targetRotationZ = Mathf.Lerp(0, 255, gun.Temperature);
        transform.localRotation = _startingRotation * Quaternion.Euler(new Vector3(0,0, Mathf.Lerp(0, 255, gun.Temperature)));
        if(gun.IsOverheated && !_audioSource.isPlaying)
            _audioSource.Play();
    }
}
