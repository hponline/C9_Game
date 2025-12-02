using System;
using System.Collections;
using UnityEngine;

public class WaweSpawner : MonoBehaviour
{
    [SerializeField] WaweConfigSO waweConfigSO;
    [SerializeField] EnemySpawnPoint[] spawnPoints;

    public void StartWawe()
    {
        StartCoroutine(SpawnWaweCoroutine());
    }

    IEnumerator SpawnWaweCoroutine()
    {
        foreach (var enemy in waweConfigSO.enemies)
        {
            for (int i = 0; i < enemy.count; i++)
            {
                SpawnEnemy(enemy.enemyConfigSO);
                yield return new WaitForSeconds(waweConfigSO.spawnInternal);
            }
        }
    }

    private void SpawnEnemy(EnemyConfigSO enemyConfigSO)
    {
        if (enemyConfigSO == null) return;
        var point = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyConfigSO.prefab, point.Transform.position, point.Transform.rotation);
    }
}
