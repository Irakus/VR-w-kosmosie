using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

internal enum Maneuver
{
    Aim,
    Evade
}

public class AiInput : MonoBehaviour
{
    private Transform _target;
    private Vector3 _angleToTarget;
    private Vector3 _targetDirection;
    [SerializeField] private Gun _gun1;
    [SerializeField] private Gun _gun2;
    [SerializeField] private Maneuver currentManeuver;
    [SerializeField] private float maxFiringAngle;
    [SerializeField] private float aimingMinTime;
    [SerializeField] private float aimingMaxTime;
    [SerializeField] private float evadingMinTime;
    [SerializeField] private float evadingMaxTime;

    private float _maneuverTimeLeft;
    private EngineAccelerator _engineAccelerator;
    private Rigidbody _rigidbody;

    private float _evadeLeftThrottle;
    private float _evadeRightThrottle;
    private float _evadeHorizontalThrottle;
    private float _evadeVerticalThrottle;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _engineAccelerator = GetComponent<EngineAccelerator>();
        _target = FindObjectOfType<PlayerInput>().transform;
        StartEvadeManeuver();
    }

    private void StartAimManeuver()
    {
        _maneuverTimeLeft = Random.Range(aimingMinTime, aimingMaxTime);
        currentManeuver = Maneuver.Aim;
    }
    
    private void StartEvadeManeuver()
    {
        _maneuverTimeLeft = Random.Range(evadingMinTime, evadingMaxTime);
        _evadeLeftThrottle = Random.Range(0.6f, 1f);
        _evadeRightThrottle = Random.Range(0.6f, 1f);
        _evadeHorizontalThrottle = Random.Range(-1f, 1f);
        currentManeuver = Maneuver.Evade;
    }

    void Update()
    {
        var posDiff = _target.position - transform.position;
        _angleToTarget = Quaternion.FromToRotation(transform.forward, posDiff).eulerAngles;
        _angleToTarget = ShortenAngle(_angleToTarget);
        FireUpdate();

        _targetDirection = transform.InverseTransformPoint(_target.position);

        _maneuverTimeLeft -= Time.deltaTime;
        if (_maneuverTimeLeft < 0)
        {
            if (currentManeuver == Maneuver.Aim)
            {
                StartEvadeManeuver();
            }
            else if (currentManeuver == Maneuver.Evade)
            {
                StartAimManeuver();
            }
        }

    }

    private void FireUpdate()
    {
        if (Mathf.Abs(_angleToTarget.x) < maxFiringAngle &&
            Mathf.Abs(_angleToTarget.y) < maxFiringAngle &&
            Mathf.Abs(_angleToTarget.z) < maxFiringAngle)
        {
            _gun1.Fire();
            _gun2.Fire();
        }
    }

    private void FixedUpdate()
    {
        AdjustEnginesFixedUpdate();
    }

    private void AdjustEnginesFixedUpdate()
    {
        switch (currentManeuver)
        {
            case Maneuver.Aim:
                YTrackingUpdate();
                XTrackingUpdate();
                StabiliseZUpdate();
                break;
            case Maneuver.Evade:
                EvadeUpdate();
                break;
        }
        
    }

    private void EvadeUpdate()
    {
        _engineAccelerator.ThrottleLeftEngine(_evadeLeftThrottle);        
        _engineAccelerator.ThrottleRightEngine(_evadeRightThrottle);  
        _engineAccelerator.HorizontalRotationEngine(_evadeHorizontalThrottle);
    }

    private void YTrackingUpdate()
    {
        if (_targetDirection.y > 0)
        {
            _engineAccelerator.VerticalRotationEngine(0.2f);
        }
        else
        {
            _engineAccelerator.VerticalRotationEngine(-0.2f);
        }    
    }

    private void XTrackingUpdate()
    {
        if (_targetDirection.x > 0)
        {
            _engineAccelerator.ThrottleLeftEngine(0.3f);            

        }
        else
        {
            _engineAccelerator.ThrottleRightEngine(0.3f);
        }
    }

    private void StabiliseZUpdate()
    {
        float velocityInterpolationValue = (_rigidbody.angularVelocity.z /10);
        _engineAccelerator.HorizontalRotationEngine(Mathf.Clamp(velocityInterpolationValue,-3f,3f));
    }
    


    private Vector3 ShortenAngle(Vector3 angle)
    {
        if (angle.x > 180)
            angle.x -= 360;
        if (angle.y > 180)
            angle.y -= 360;
        if (angle.z > 180)
            angle.z -= 360;
        return angle;
    }
}


