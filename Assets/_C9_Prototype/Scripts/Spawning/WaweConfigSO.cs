using UnityEngine;

[System.Serializable]
public struct EnemySpawnEntry
{
    public EnemyConfigSO enemyConfigSO;
    public int count;
}

[CreateAssetMenu(menuName = "C9/WaweConfig")]
public class WaweConfigSO : ScriptableObject
{
    public EnemySpawnEntry[] enemies;
    public float spawnInternal = 1f;
}
