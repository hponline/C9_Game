using UnityEngine;

[CreateAssetMenu(menuName = "C9/PlayerConfig")]
public class PlayerConfigSO : ScriptableObject
{
    [Header("Base Stats")]
    public float baseHealth = 100f;
    public float baseDamage = 10f;
    public float baseMoveSpeed = 3f;
    public float baseAttackSpeed = 1f;
    public float jumpForce = 3f;
}
