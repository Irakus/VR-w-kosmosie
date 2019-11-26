using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float velocity;
    private float _lifetime = 10.0f;


    // Update is called once per frame
    void Update()
    {
        var time = Time.deltaTime;
        transform.position += time * velocity * transform.up;
        _lifetime -= time;
        if (_lifetime<0)
            Destroy(gameObject);
    }
}
