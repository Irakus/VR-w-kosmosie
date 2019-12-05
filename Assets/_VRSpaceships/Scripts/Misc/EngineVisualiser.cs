using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineVisualiser : MonoBehaviour
{
    private const float THRUSTER_INTENSITY = 20.0f;
    private const float FIRE_SIZE_MIN = 2.0f;
    private const float FIRE_SIZE_MAX= 4.0f;
    private const float FIRE_SPEED_MIN = 1.0f;
    private const float FIRE_SPEED_MAX = 1.5f;
    public enum Engine
    {
        Left,Right
    }

    [SerializeField] private Light _rightEngineLight;
    [SerializeField] private Light _leftEngineLight;

    [SerializeField] private ParticleSystem _rightEngineFire;
    [SerializeField] private ParticleSystem _leftEngineFire;

    public void VisualiseEngine(Engine engine,float throttle)
    {
        switch (engine)
        {
            case Engine.Left:
                VisualiseImpl(_leftEngineLight,_leftEngineFire,throttle);
                break;
            case Engine.Right:
                VisualiseImpl(_rightEngineLight, _rightEngineFire, throttle);
                break;
            default:
                Debug.Assert(true,"Invalid engine for visualiser");
                break;
        }
    }

    private void VisualiseImpl(Light light,ParticleSystem fire,float throttle)
    {
        light.intensity = throttle * THRUSTER_INTENSITY;
        var main = fire.main;
        main.startSize = new ParticleSystem.MinMaxCurve(throttle * FIRE_SIZE_MAX, throttle * FIRE_SIZE_MAX * 1.5f);
        main.simulationSpeed = 1f + FIRE_SPEED_MAX * throttle;
        
        //fire.Play();
    }

}
