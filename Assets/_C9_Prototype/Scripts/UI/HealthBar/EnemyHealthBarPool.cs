using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBarPool : MonoBehaviour
{
    public static EnemyHealthBarPool instance;

    [SerializeField] HealthBarUI EnemyHealthBarUIPrefab;
    [SerializeField] Transform container;

    Queue<HealthBarUI> pool = new();

    private void Awake()
    {
        instance = this;
        // pool ve HealthBar scriptlerini tekrar incele
    }

    public HealthBarUI Get()
    {
        if (pool.Count > 0)
            return pool.Dequeue();

        return Instantiate(EnemyHealthBarUIPrefab, container);
    }

    public void Release(HealthBarUI bar)
    {
        bar.gameObject.SetActive(false);
        pool.Enqueue(bar);
    }
}
