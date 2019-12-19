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
    Evade,
    TurnAround
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
    [SerializeField] private float maxFiringDistance;
    [SerializeField] private float aimingMinTime;
    [SerializeField] private float aimingMaxTime;
    [SerializeField] private float evadingMinTime;
    [SerializeField] private float evadingMaxTime;

    private float _maneuverTimeLeft;
    private EngineAccelerator _engineAccelerator;
    private Rigidbody _rigidbody;

    private float _maneuverLeftThrottle;
    private float _maneuverRightThrottle;
    private float _evadeHorizontalThrottle;
    private float _evadeVerticalThrottle;
    private float _maneuverVerticalThrottle;
    private Vector3 _posDiff;


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
        _maneuverLeftThrottle = Random.Range(0.6f, 1f);
        _maneuverRightThrottle = Random.Range(0.6f, 1f);
        _evadeHorizontalThrottle = Random.Range(-1f, 1f);
        currentManeuver = Maneuver.Evade;
    }
    
    private void StartTurnAroundManeuver()
    {
        _maneuverTimeLeft = 1.0f;
        _maneuverLeftThrottle = Random.Range(0.3f, 0.6f);
        _maneuverRightThrottle = Random.Range(0.3f, 0.6f);
        _maneuverVerticalThrottle = Random.Range(0, 1)>0 ? 1 : -1 ;
        currentManeuver = Maneuver.TurnAround;
    }

    void Update()
    {
        _posDiff = _target.position - transform.position;
        _angleToTarget = Quaternion.FromToRotation(transform.forward, _posDiff).eulerAngles;
        _angleToTarget = ShortenAngle(_angleToTarget);
        FireUpdate();

        _targetDirection = transform.InverseTransformPoint(_target.position);

        _maneuverTimeLeft -= Time.deltaTime;
        if (_maneuverTimeLeft < 0)
        {
            if (currentManeuver == Maneuver.Aim)
            {
                if (_posDiff.magnitude < 100f)
                    StartTurnAroundManeuver();
                else
                    StartEvadeManeuver();
            }
            else if (currentManeuver == Maneuver.TurnAround)
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
            Mathf.Abs(_angleToTarget.z) < maxFiringAngle &&
            _posDiff.magnitude < maxFiringDistance)
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
                StabiliseXUpdate();
                break;
            case Maneuver.TurnAround:
                TurnAroundUpdate();
                break;
        }
        
    }

    private void TurnAroundUpdate()
    {
        _engineAccelerator.VerticalRotationEngine(_maneuverVerticalThrottle);
        _engineAccelerator.ThrottleLeftEngine(_maneuverLeftThrottle);        
        _engineAccelerator.ThrottleRightEngine(_maneuverRightThrottle); 
    }

    private void EvadeUpdate()
    {
        _engineAccelerator.ThrottleLeftEngine(_maneuverLeftThrottle);        
        _engineAccelerator.ThrottleRightEngine(_maneuverRightThrottle);  
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
    
    private void StabiliseXUpdate()
    {
        float velocityInterpolationValue = (_rigidbody.angularVelocity.x /10);
        _engineAccelerator.VerticalRotationEngine(Mathf.Clamp(velocityInterpolationValue,-3f,3f));
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


