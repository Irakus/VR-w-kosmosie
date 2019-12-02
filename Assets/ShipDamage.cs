using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDamage : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private AudioClip[] oneDamageClips;
    private AudioSource _audioSource;
    
    
    // Start is called before the first frame update
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void ReceiveDamage(int damage, Vector3 hitPosition)
    {
        health -= damage;
        if (_audioSource != null)
        {
            transform.position = hitPosition;
            _audioSource.PlayOneShot(oneDamageClips[Random.Range(1,oneDamageClips.Length)]);
        }
    }
}
