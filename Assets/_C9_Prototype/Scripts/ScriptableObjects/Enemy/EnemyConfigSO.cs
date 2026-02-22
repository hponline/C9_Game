using UnityEngine;

[CreateAssetMenu(menuName = "C9/EnemyConfig")]
public class EnemyConfigSO : ScriptableObject
{
    public GameObject prefab;

    [Header("Base Stats")]
    public float baseHealth = 100f;
    public float baseDamage = 10f;
    public float baseMoveSpeed = 3f;

    [Header("Level Scaling")]
    public int expReward = 10;
    public float healthPerLevelMultiplier = 1.2f;
    public float damagePerLevelMultiplier = 1.2f;
}
