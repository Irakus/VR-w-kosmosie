using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class LightBlink : MonoBehaviour
{
    private Light pointLight;
    private float _startingIntensity;
    [SerializeField] private float _decayTime;

    // Start is called before the first frame update
    private void Awake()
    {
        pointLight = GetComponent<Light>();
        _startingIntensity = pointLight.intensity;
    }
    
    // Update is called once per frame
    void Update()
    {
        pointLight.intensity -= Time.deltaTime * _startingIntensity / _decayTime;
    }
}
