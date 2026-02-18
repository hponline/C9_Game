using UnityEngine;

[CreateAssetMenu(menuName = "C9/PlayerConfig")]
public class PlayerConfigSO : ScriptableObject
{
    [Header("Base Stats")]
    public float baseHealth = 100f;
    public int baseDamage = 100;
    public float baseMoveSpeed = 3f;
    public float baseAttackSpeed = 1f;
    public float jumpForce = 3f;
    public float critChangeMultiplier = 0.2f;
    public float critMultiplier = 1.5f;
}
