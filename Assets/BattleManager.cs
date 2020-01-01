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
    
    public static BattleManager Instance { get; private set; }

    public void EndWave()
    {
        StartWave(++_waveNumber);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0,1,1,0.8f);
        Gizmos.DrawWireSphere(transform.position, minSpawnDistance);
        Gizmos.color = new Color(0,1,1,0.1f);
        Gizmos.DrawWireSphere(transform.position, maxSpawnDistance);
    }

    private void Awake()
    {
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartWave(1);
    }
    
    private void StartWave(int number)
    {
        for (int i = 0; i < number; i++)
        {
            var enemy = EnemySpaceshipPool.Instance.Get();
            enemy.transform.position = transform.position + transform.forward * 10;
        }
    }
}
