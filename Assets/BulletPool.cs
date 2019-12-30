using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{

    [SerializeField] private Bullet bulletPrefab;

    private Queue<Bullet> _bullets = new Queue<Bullet>();
    
    public static BulletPool Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        AddBullets(100);
    }

    public Bullet Get()
    {
        if (_bullets.Count == 0)
        {
            AddBullets(1);
        }
        var bullet =  _bullets.Dequeue();
        bullet.gameObject.SetActive(true);
        return bullet;
    }

    private void AddBullets(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Bullet b = Instantiate(bulletPrefab);
            b.gameObject.SetActive(false);
            _bullets.Enqueue(b);
        }
    }

    public void ReturnToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        _bullets.Enqueue(bullet);
    }
}
