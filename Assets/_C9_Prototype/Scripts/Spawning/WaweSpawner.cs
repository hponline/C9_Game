using UnityEngine;

public class WaweSpawner : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    //[SerializeField] WaweConfigSO waweConfigSO;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] Transform enemyParentHierarchy;

    [SerializeField] EnemyConfigSO enemyConfigSO;
    public int enemiesToSpawnPerLevel = 10;
    int aliveEnemyCount = 0;

    public int AliveEnemyCount => aliveEnemyCount;

    void StartNextWave()
    {
        Debug.Log("Level " + gameManager.currentLevel);

        aliveEnemyCount = 0;

        for (int i = 0; i < enemiesToSpawnPerLevel; i++)
        {
            var point = spawnPoints[Random.Range(0, spawnPoints.Length)];
            var enemy = Instantiate(enemyConfigSO.prefab, point.transform.position, point.transform.rotation);
            EnemyRunTimeStats currentEnemy = enemy.GetComponent<EnemyRunTimeStats>();
            currentEnemy.Init(enemyConfigSO, gameManager.currentLevel);
            enemy.transform.SetParent(enemyParentHierarchy, true);

            aliveEnemyCount++;
        }
    }

    public void EnemyDied() // event + enemyhealth
    {
        aliveEnemyCount--;
        if (aliveEnemyCount <= 0)
        {
            gameManager.currentLevel ++;
            StartNextWave();
        }
    }
}
