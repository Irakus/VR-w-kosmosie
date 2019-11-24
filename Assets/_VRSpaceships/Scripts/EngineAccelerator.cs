using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineAccelerator : MonoBehaviour
{
    private const float MINIMAL_ENGINE_THROTTLE = 0.0f;
    private const float ENGINE_THROTTLE = 500.0f;
    private const float ROTATION_MULTIPLIER = 100.0f;
    private const float ENGINES_SOUND_MAX_SPEED = 20.0f;

    [SerializeField]
    private Transform _leftEngine;

    [SerializeField]
    private Transform _rightEngine;

    [SerializeField]
    private Transform _bow;
    private Rigidbody _rigidbody;
    [SerializeField]
    private AudioSource _leftThrusterAudioSource;
    [SerializeField] 
    private AudioSource _rightThrusterAudioSource;
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _leftThrusterAudioSource.pitch = _rightThrusterAudioSource.pitch = Mathf.Lerp(0.25f, 1f, _rigidbody.velocity.magnitude / ENGINES_SOUND_MAX_SPEED);
    }

    public void ThrottleLeftEngine(float value)
    {
        ApplyForceToEngine(_leftEngine,value);
    }
    public void ThrottleRightEngine(float value)
    {
        ApplyForceToEngine(_rightEngine, value);
    }
    private void ApplyForceToEngine(Transform engine, float value)
    {
        float forceStrength = CalculateThrottleStrength(value);
        Vector3 forward = transform.forward;
        _rigidbody.AddForceAtPosition(forward.normalized * forceStrength, engine.position, ForceMode.Force);
    }
    private float CalculateThrottleStrength(float value)
    { 
        return MINIMAL_ENGINE_THROTTLE + ENGINE_THROTTLE * value;
    }
    public void VerticalRotationEngine(float value)
    {
        float verticalRotationStrength = ROTATION_MULTIPLIER * value;
        _rigidbody.AddForceAtPosition(transform.up.normalized * verticalRotationStrength, _bow.position, ForceMode.Force);
    }
    public void HorizontalRotationEngine(float value)
    {
        float horizontalRotationStrength = ROTATION_MULTIPLIER * value;
        _rigidbody.AddForceAtPosition(transform.up * horizontalRotationStrength, _leftEngine.position, ForceMode.Force);
        _rigidbody.AddForceAtPosition(-transform.up * horizontalRotationStrength, _rightEngine.position, ForceMode.Force);
    }
}
