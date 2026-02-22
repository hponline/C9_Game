using UnityEngine;

public class GameManager : MonoBehaviour
{
    public WaweSpawner waweSpawner;

    public int currentLevel = 1;

    private void Start()
    {
        if (waweSpawner.AliveEnemyCount == 0)
            waweSpawner.EnemyDied();

        Debug.Log("WaweSpawner, gameManager, enemy + enemy kalýtým, enemyHealth exp + died düzenle");
    }
}
