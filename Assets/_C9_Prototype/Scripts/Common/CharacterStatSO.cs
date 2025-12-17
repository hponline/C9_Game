using UnityEngine;

[CreateAssetMenu(menuName = "C9/Character Stats")]
public class CharacterStatSO : ScriptableObject
{
    [Header("BaseStats")]
    public float maxHealth = 100f;
    public float attackPower = 100f;
    public float defense = 0f;
    public float moveSpeed = 5f;
    public float jumpForce = 3f;

    [Header("UpgradeStats")]
    [SerializeField] float attackRange = 3f;
    float finalAttackSpeed; // item + buff sonrasý OPSÝYONEL
    float attackSpeedMultiplier;
    float baseAttackSpeed = 1f;
    float attackCooldown;
    float attackTimer;
}
