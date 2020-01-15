﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ShipDamage : MonoBehaviour
{
    public int Health;
    [SerializeField] private AudioClip[] oneDamageClips;
    [SerializeField] private AudioClip explosionClip;
    [SerializeField] private TextMeshPro hpText;
    private AudioSource _audioSource;
    private int _lastPlayedClip;
    [SerializeField] private GameObject explosion;
    private bool hasPlayerInput;
    private FragCounter _fragCounter;
    private bool _wasRekt = false;
    private int _initialHealth;
    private AudioSource _alertAudioSource;
    [FormerlySerializedAs("malfunctionClip")] [SerializeField] private AudioClip alertClip;
    [SerializeField] private VirtualKeyboard keyboard;

    private void OnEnable()
    {
        Health = _initialHealth;
        _wasRekt = false;
    }

    // Start is called before the first frame update
    private void Awake()
    {
        _fragCounter = FindObjectOfType<FragCounter>();
        var playerInput = GetComponentInParent<PlayerInput>();        
        hasPlayerInput = playerInput != null;
        _initialHealth = Health;
        if(hpText != null)
            _alertAudioSource = hpText.transform.parent.GetComponent<AudioSource>();
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _lastPlayedClip = GetRandomClipIndex();
        if (hpText != null)
        {
            hpText.text = Health.ToString();
        }
    }

    private int GetRandomClipIndex()
    {
        return Random.Range(1, oneDamageClips.Length);
    }

    public void ReceiveDamage(int damage, Vector3 hitPosition)
    {
        Health -= damage;
        if (hpText != null)
        {
            hpText.text = Health.ToString();
            if (Health <= _initialHealth / 4 && !_alertAudioSource.isPlaying)
            {
                _alertAudioSource.Play();
                _alertAudioSource.PlayOneShot(alertClip, 20f);
            }
        }
        if (Health <= 0 && !_wasRekt)
        {
            _wasRekt = true;
            StartCoroutine(GetRekt());
            return;
        }
        if (_audioSource != null)
        {
            transform.position = hitPosition;
            var temp = oneDamageClips[0];
            oneDamageClips[0] = oneDamageClips[_lastPlayedClip];
            oneDamageClips[_lastPlayedClip] = temp;
            _audioSource.PlayOneShot(oneDamageClips[GetRandomClipIndex()]);
        }

        
    }

    private IEnumerator GetRekt()
    {
        var myTransform = transform;
        Instantiate(explosion, myTransform.position, myTransform.rotation);
        if (!hasPlayerInput)
        {
            _fragCounter.Count++;
            yield return new WaitForSeconds(0.3f);
            EnemySpaceshipPool.Instance.ReturnToPool(transform.parent.GetComponent<AiInput>());
        }
        else
        {
            StartCoroutine(PlayerDeath());
        }
    }

    private IEnumerator PlayerDeath()
    {
        var myTransform = transform;
        var bigExplosion = Instantiate(explosion, transform.parent);
        bigExplosion.transform.localScale *= 15;
        bigExplosion.transform.localPosition += Vector3.forward*25;
        _alertAudioSource.Stop();
        Time.timeScale = 0.2f;
        _audioSource.PlayOneShot(explosionClip);
        yield return new WaitForSecondsRealtime(2f);
        keyboard.gameObject.SetActive(true);
        yield return keyboard.WaitForInput();
        string nickName = keyboard.GetNickname();
        keyboard.gameObject.SetActive(false);
        FindObjectOfType<HighScoreManager>().ShowScores(new PlayerScore(nickName, _fragCounter.Count));
        yield return null;
    }
}
