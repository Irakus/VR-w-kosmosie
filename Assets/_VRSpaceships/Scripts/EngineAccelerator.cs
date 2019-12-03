using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineAccelerator : MonoBehaviour
{
    private const float MINIMAL_ENGINE_THROTTLE = 0.0f;
    private const float ENGINE_THROTTLE = 500.0f;
    private const float ROTATION_MULTIPLIER = 100.0f;

    private bool _enginesWorking;

    [SerializeField]
    private Transform _leftEngine;

    [SerializeField]
    private Transform _rightEngine;

    [SerializeField]
    private Transform _bow;
    private Rigidbody _rigidbody;

    private EngineVisualiser _engineVisualiser;
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _engineVisualiser = GetComponent<EngineVisualiser>();
    }

    private void Update()
    {
    }

    public void ThrottleLeftEngine(float value)
    {
        ApplyForceToEngine(_leftEngine,value);
        _engineVisualiser.VisualiseEngine(EngineVisualiser.Engine.Left, value);
    }

    public void ThrottleRightEngine(float value)
    {
        ApplyForceToEngine(_rightEngine, value);
        _engineVisualiser.VisualiseEngine(EngineVisualiser.Engine.Right,value);
    }
    private void ApplyForceToEngine(Transform engine, float value)
    {
        if (!_enginesWorking) return;
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
        if (!_enginesWorking) return;
        float verticalRotationStrength = ROTATION_MULTIPLIER * value;
        _rigidbody.AddForceAtPosition(transform.up.normalized * verticalRotationStrength, _bow.position, ForceMode.Force);
    }
    public void HorizontalRotationEngine(float value)
    {
        if (!_enginesWorking) return;
        float horizontalRotationStrength = ROTATION_MULTIPLIER * value;
        _rigidbody.AddForceAtPosition(transform.up * horizontalRotationStrength, _leftEngine.position, ForceMode.Force);
        _rigidbody.AddForceAtPosition(-transform.up * horizontalRotationStrength, _rightEngine.position, ForceMode.Force);
    }

    public void TurnOnEngines()
    {
        _enginesWorking = true;
    }

    public void TurnOffEngines()
    {
        _enginesWorking = false;
    }
}
