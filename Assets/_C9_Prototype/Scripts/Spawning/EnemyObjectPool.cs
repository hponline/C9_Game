using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : MonoBehaviour
{
    public static EnemyObjectPool instance;

    [SerializeField] int initialSize = 100;
    [SerializeField] Transform container;
    [SerializeField] EnemyHealth enemyPrefab;

    Queue<EnemyHealth> pool = new();

    private void Awake()
    {
        instance = this;
        Initialized();
    }

    public void Initialized()
    {
        for (int i = 0; i < initialSize; i++)
        {
            var obj = Instantiate(enemyPrefab, container);
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public EnemyHealth Get()
    {
        EnemyHealth obj;

        if (pool.Count > 0)
            obj = pool.Dequeue();
        else
            obj = Instantiate(enemyPrefab, container);

        obj.gameObject.SetActive(true);

        if (obj is IPoolable poolable)
            poolable.OnSpawn();

        return obj;
    }

    public void Release(EnemyHealth obj)
    {
        if (obj is IPoolable poolable)
            poolable.OnDespawn();

        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }
}
