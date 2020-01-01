using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpaceshipPool : GameObjectPool<AiInput>
{
    public new void ReturnToPool(AiInput pooledObject)
    {
        base.ReturnToPool(pooledObject);
        if (_objects.Count == 0)
        {
            BattleManager.Instance.EndWave();
        }
    }
}
