using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float firingCooldown;
    [SerializeField] private float temperatureGain;
    [SerializeField] private float temperatureCooling;

    public float Temperature { get; private set; }
    private float _cooldownTimer = 0.0f;
    private AudioSource _audioSource;
    private bool _isAudioSourceNotNull;
    public bool IsOverheated { get; private set; }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _isAudioSourceNotNull = _audioSource != null;
        Temperature = 0.0f;
        IsOverheated = false;
    }

    private void Update()
    {
        if(Temperature>0.0f)
            Temperature -= temperatureCooling * Time.deltaTime;
        _cooldownTimer -= Time.deltaTime;

        if (Temperature > 1.0f)
        {
            IsOverheated = true;
        }
        else if (Temperature < 0.85)
        {
            IsOverheated = false;
        }
    }

    public void Fire()
    {
        if (!IsOverheated && _cooldownTimer <= 0.0f)
        {
            var bullet = GameObject.Instantiate(bulletPrefab, transform.position, transform.rotation * Quaternion.Euler(90, 0, 0), null);
            bullet.transform.position += transform.forward * 5;
            _cooldownTimer = firingCooldown;
            Temperature += temperatureGain;
            if (_isAudioSourceNotNull)
            {
                _audioSource.pitch = Mathf.Lerp(0.8f, 1f, Temperature);
                _audioSource.PlayOneShot(_audioSource.clip);
            }
        }
    }
}
