using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private float minSpawnDistance;
    [SerializeField] private float maxSpawnDistance;
    private int _waveNumber = 1;
    [SerializeField] private TextMesh waveNumberText;

    public static BattleManager Instance { get; private set; }

    public void EndWave()
    {
        StartRound(++_waveNumber);
        waveNumberText.text = _waveNumber.ToString();
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
        StartRound(1);
    }
    
    private void StartRound(int roundNumber)
    {
        int enemyHealth = roundNumber;
        int enemyNumber = (roundNumber/3)*4 + (roundNumber%3);
        Debug.Log($"Spawning {enemyNumber} enemies with {enemyHealth} HP each");
        for (int i = 0; i < enemyNumber; i++)
        {
            SpawnEnemy(enemyHealth);
        }
    }

    private void SpawnEnemy(int health)
    {
        var enemy = EnemySpaceshipPool.Instance.Get();
        enemy.GetComponentInChildren<ShipDamage>().Health = health;
        var spawnOffset = GenerateSpawnOffset();
        var parentTransform = transform.parent;
        enemy.transform.position = parentTransform.position + spawnOffset;
        enemy.transform.LookAt(parentTransform);
    }

    private Vector3 GenerateSpawnOffset()
    {
        var distanceVector = Random.Range(minSpawnDistance, maxSpawnDistance) * Vector3.forward;
        var rotation = Random.rotationUniform;
        return rotation * distanceVector;
    }
    
}
