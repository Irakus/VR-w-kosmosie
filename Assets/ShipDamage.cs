using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class ShipDamage : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private AudioClip[] oneDamageClips;
    [SerializeField] private TextMeshPro hpText;
    private AudioSource _audioSource;
    private int _lastPlayedClip;
    
    // Start is called before the first frame update
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _lastPlayedClip = GetRandomClipIndex();
        hpText.text = health.ToString();
    }

    private int GetRandomClipIndex()
    {
        return Random.Range(1, oneDamageClips.Length);
    }

    public void ReceiveDamage(int damage, Vector3 hitPosition)
    {
        health -= damage;
        hpText.text = health.ToString();
        if (_audioSource != null)
        {
            transform.position = hitPosition;
            var temp = oneDamageClips[0];
            oneDamageClips[0] = oneDamageClips[_lastPlayedClip];
            oneDamageClips[_lastPlayedClip] = temp;
            _audioSource.PlayOneShot(oneDamageClips[GetRandomClipIndex()]);
        }
    }
}
