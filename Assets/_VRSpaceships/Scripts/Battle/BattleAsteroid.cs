using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BattleAsteroid : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddForce(_rigidbody.mass * new Vector3(Random.Range(-20f,20f), Random.Range(-20f,20f), Random.Range(-20f,20f)));
        _rigidbody.AddTorque(_rigidbody.mass * new Vector3(Random.Range(-15f,15f), Random.Range(-15f,15f), Random.Range(-15f,15f)));
    }

    private void OnCollisionEnter(Collision other)
    {
        var impulseMagnitude = other.impulse.magnitude;
        Debug.Log((int)(impulseMagnitude/10));
        if (impulseMagnitude > 10f)
        {
            var rb = other.rigidbody;
            if (rb != null)
            {
                var shipDamage = rb.GetComponentInChildren<ShipDamage>();
                if (shipDamage != null)
                {
                    var damage = Mathf.Max((int) (impulseMagnitude / 10), 3);
                    shipDamage.ReceiveDamage(damage, transform.position);
                }
            }
        }
    }
}
