using System;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] Transform enemyParentHierarchy;
    [SerializeField] EnemyConfigSO enemyConfigSO;

    public int enemiesToSpawnPerLevel = 10;
    int aliveEnemyCount;
    public int AliveEnemyCount => aliveEnemyCount;

    public event Action<int> OnAliveEnemyChanged;

    public void StartNextWave()
    {
        Debug.Log("Level " + gameManager.currentLevel);

        aliveEnemyCount = 0;

        for (int i = 0; i < enemiesToSpawnPerLevel; i++)
        {
            var point = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
            var enemyGO = Instantiate(enemyConfigSO.prefab, point.transform.position, point.transform.rotation);

            enemyGO.transform.SetParent(enemyParentHierarchy, true);

            EnemyRunTimeStats runTimeStats = enemyGO.GetComponent<EnemyRunTimeStats>();
            runTimeStats.Init(enemyConfigSO, gameManager.currentLevel);

            var health = enemyGO.GetComponent<EnemyHealth>();
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
        {
            gameManager.currentLevel++;
            StartNextWave();
        }
    }
}
