using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomIntervalAudio : MonoBehaviour
{
    private AudioSource _audioSource;
    private float _timer = 0.0f;
    private double _waitingTime;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        GenerateWaitingTime();
    }

    private void GenerateWaitingTime()
    {
        _waitingTime = Random.Range(5.0f, 20.0f);
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _waitingTime)
        {
            _audioSource.Play();
            _timer = 0.0f;
            GenerateWaitingTime();
        }
    }

 
}
