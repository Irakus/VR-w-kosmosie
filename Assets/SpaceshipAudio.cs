using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipAudio : MonoBehaviour
{
    [SerializeField]
    private AudioSource _leftThrusterAudioSource;
    [SerializeField] 
    private AudioSource _rightThrusterAudioSource;
    [SerializeField] 
    private AudioSource _vibratingWallsAudioSource;

    private Rigidbody _rigidbody;

    private const float ENGINES_SOUND_MAX_SPEED = 20.0f;
    private const float VIBRATING_WALLS_SOUND_MAX_VALUE = 150f;
    
    
    // Update is called once per frame
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _leftThrusterAudioSource.pitch = _rightThrusterAudioSource.pitch = Mathf.Lerp(0.25f, 1f, _rigidbody.velocity.magnitude / ENGINES_SOUND_MAX_SPEED);
        _vibratingWallsAudioSource.volume = Mathf.Lerp(0, 1.0f,(_rigidbody.angularVelocity.magnitude*8 + _rigidbody.velocity.magnitude - 25) / VIBRATING_WALLS_SOUND_MAX_VALUE);
    }
}
