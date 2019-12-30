using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private float minSpawnDistance;
    [SerializeField] private float maxSpawnDistance;
    private int _waveNumber;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0,1,1,0.8f);
        Gizmos.DrawWireSphere(transform.position, minSpawnDistance);
        Gizmos.color = new Color(0,1,1,0.1f);
        Gizmos.DrawWireSphere(transform.position, maxSpawnDistance);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _waveNumber = 1;
    }

    
}
