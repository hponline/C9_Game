using UnityEngine;

[CreateAssetMenu(menuName = "C9/EnemyStats")]
public class EnemyStatSO : ScriptableObject
{
    [Header("BaseStats")]
    public float maxHealth = 100f;
    public float attackPower = 100f;
    public float defense = 0f;
    public float moveSpeed = 5f;
    public float jumpForce = 3f;

    [Header("UpgradeStats")]
    public float attackRange = 3f;
    public float finalAttackSpeed;
    public float attackSpeedMultiplier;
    public float baseAttackSpeed = 1f;
    public float attackCooldown;
    public float attackTimer;

}
