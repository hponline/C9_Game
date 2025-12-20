using UnityEngine;

public enum EnemyRace
{
    Orc,
    Elf,
    Human
}

[CreateAssetMenu(menuName = "C9/EnemyConfig")]
public class EnemyConfigSO : ScriptableObject
{
    public EnemyRace enemyRace;
    public GameObject prefab;

    [Header("Base Stats")]
    public float baseHealth = 100f;
    public float baseDamage = 10f;
    public float baseMoveSpeed = 3f;
    public float baseAttackSpeed = 1f;

    [Header("Level Scaling")]
    public float healthPerLevel = 20f;
    public float damagePerLevel = 3f;
}
