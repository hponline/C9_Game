using System.Collections.Generic;
using UnityEngine;

public class EnemyHitVFXController : MonoBehaviour
{
    public static EnemyHitVFXController instance;
    [SerializeField] ParticleSystem getHitVfxPrefab;

    [SerializeField] int poolSize = 100;
    Queue<ParticleSystem> pool = new();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            CreatPool();
        }
    }

    ParticleSystem CreatPool()
    {
        var obj = Instantiate(getHitVfxPrefab, transform);
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
        return obj;
    }

    public ParticleSystem Get(Vector3 position, Quaternion rotation)
    {
        if (pool.Count == 0)
            CreatPool();

        var obj = pool.Dequeue();
        obj.gameObject.SetActive(true);
        obj.transform.SetPositionAndRotation(position, rotation);
        return obj;
    }

    public void ReturnPool(ParticleSystem obj)
    {
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }    
}
