using System;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Referances")]
    [SerializeField] GameManager gameManager;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] Transform enemyParentHierarchy;
    [SerializeField] EnemyConfigSO enemyConfigSO;
    [SerializeField] EnemyObjectPool enemyObjectPool;


    [SerializeField] int baseEnemyCount = 20;
    [SerializeField] float spawnRadius = 1f;
    public int totalEnemy;
    int aliveEnemyCount;
    public int AliveEnemyCount => aliveEnemyCount;

    public event Action<int> OnAliveEnemyChanged;

    public void StartNextWave()
    {
        aliveEnemyCount = 0;
        totalEnemy = GetEnemyCount();

        for (int i = 0; i < totalEnemy; i++)
        {
            var point = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
            Vector2 randomPos = UnityEngine.Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPos = point.position + new Vector3(randomPos.x, 0, randomPos.y);

            var enemy = enemyObjectPool.Get();
            enemy.transform.position = spawnPos;
            enemy.transform.SetParent(enemyParentHierarchy, true);

            EnemyRunTimeStats runTimeStats = enemy.GetComponent<EnemyRunTimeStats>();
            runTimeStats.Init(enemyConfigSO, gameManager.globalLevel);

            var health = enemy.GetComponent<EnemyHealth>();
            enemy.gameObject.SetActive(true);

            health.OnDied += EnemyDied;

            aliveEnemyCount++;
        }
        gameManager.ShowLevel();

    }

    public void EnemyDied(EnemyHealth enemy)
    {
        aliveEnemyCount--;
        OnAliveEnemyChanged?.Invoke(AliveEnemyCount);
        enemy.OnDied -= EnemyDied;

        if (aliveEnemyCount <= 0)
            StartNextWave();
    }

    public int GetEnemyCount()
    {
        int baseEnemy = baseEnemyCount;
        float multiplier = 1.2f;
        return Mathf.RoundToInt(baseEnemy * Mathf.Pow(multiplier, gameManager.globalLevel - 1));
    }
}
