using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameObjectPool<TPooledObject> : MonoBehaviour where TPooledObject : Component
{

    [SerializeField] private TPooledObject prefab;
    [SerializeField] private int startingCapacity;

    protected Queue<TPooledObject> _objects = new Queue<TPooledObject>();

    public static GameObjectPool<TPooledObject> Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        AddObjects(startingCapacity);
    }

    public TPooledObject Get()
    {
        if (_objects.Count == 0)
        {
            AddObjects(1);
        }
        var pooledObject = _objects.Dequeue();
        pooledObject.gameObject.SetActive(true);
        return pooledObject;
    }

    private void AddObjects(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            TPooledObject o = Instantiate(prefab);
            o.gameObject.SetActive(false);
            _objects.Enqueue(o);
        }
    }

    public void ReturnToPool(TPooledObject pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
        _objects.Enqueue(pooledObject);
    }
}
