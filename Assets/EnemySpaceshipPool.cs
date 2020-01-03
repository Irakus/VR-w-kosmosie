using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpaceshipPool : GameObjectPool<AiInput>
{
    public new static EnemySpaceshipPool Instance { get; set; }

    public void Awake()
    {
        Instance = this;
    }
    public new void ReturnToPool(AiInput pooledObject)
    {
        base.ReturnToPool(pooledObject);
        if (_activeObjectAmount == 0)
        {
            BattleManager.Instance.EndWave();
        }
    }
}
