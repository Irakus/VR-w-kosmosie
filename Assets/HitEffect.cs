using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    private float _lifetime;

    private void OnEnable()
    {
        _lifetime = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        _lifetime -= Time.deltaTime;
        if (_lifetime < 0)
            HitEffectPool.Instance.ReturnToPool(this);
    }
}
