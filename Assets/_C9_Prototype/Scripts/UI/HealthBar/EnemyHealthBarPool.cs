using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBarPool : MonoBehaviour
{
    public static EnemyHealthBarPool instance;

    [SerializeField] int initialSize = 100;
    [SerializeField] HealthBarUI EnemyHealthBarUIPrefab;
    [SerializeField] Transform container;

    Queue<HealthBarUI> pool = new();

    private void Awake()
    {
        instance = this;
        Initialized();
    }

    public void Initialized()
    {
        for (int i = 0; i < initialSize; i++)
        {
            var obj = Instantiate(EnemyHealthBarUIPrefab, container);
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public HealthBarUI Get()
    {
        HealthBarUI bar;

        if (pool.Count > 0)
            bar = pool.Dequeue();
        else
            bar = Instantiate(EnemyHealthBarUIPrefab, container);

        bar.gameObject.SetActive(true);
        return bar;
    }

    public void Release(HealthBarUI bar)
    {
        bar.gameObject.SetActive(false);
        pool.Enqueue(bar);
    }
}
