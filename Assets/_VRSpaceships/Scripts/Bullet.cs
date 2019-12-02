using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using Object = UnityEngine.Object;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float velocity;
    
    private int _damage = 1;
    private float _lifetime = 10.0f;
    private bool _hasHit = false;
    [SerializeField] private GameObject hitEffectPrefab;

    // Update is called once per frame
    void Update()
    {
        var time = Time.deltaTime;
        transform.position += time * velocity * transform.up;
        _lifetime -= time;
        if (_lifetime < 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_hasHit)
            return;
        
        _hasHit = true;
        var ship = other.attachedRigidbody;
        var shipDamage = ship.GetComponentInChildren<ShipDamage>();
        shipDamage.ReceiveDamage(this._damage, transform.position);
        Instantiate(hitEffectPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
