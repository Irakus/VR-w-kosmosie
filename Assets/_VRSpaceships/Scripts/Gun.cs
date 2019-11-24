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

    private float _temperature = 0.0f;
    private float _cooldownTimer = 0.0f;
    [SerializeField] private TemperatureArrow temperatureArrow;
    private bool _isTemperatureArrowNotNull;
    private AudioSource _audioSource;

    private void Start()
    {
        _isTemperatureArrowNotNull = temperatureArrow != null;
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(_temperature>0.0f)
            _temperature -= temperatureCooling * Time.deltaTime;
        _cooldownTimer -= Time.deltaTime;
        if (_isTemperatureArrowNotNull)
        {
            temperatureArrow.Temperature = _temperature;
        }
    }

    public void Fire()
    {
        if (_temperature < 1f && _cooldownTimer <= 0.0f)
        {
            GameObject.Instantiate(bulletPrefab, transform.position, transform.rotation * Quaternion.Euler(90, 0, 0), null);
            _cooldownTimer = firingCooldown;
            _temperature += temperatureGain;
            _audioSource.pitch = Mathf.Lerp(0.8f, 1f, _temperature);
            _audioSource.PlayOneShot(_audioSource.clip);
        }
    }
}
