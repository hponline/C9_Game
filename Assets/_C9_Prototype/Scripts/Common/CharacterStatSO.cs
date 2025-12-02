using UnityEngine;

[CreateAssetMenu(menuName = "C9/Character Stats")]
public class CharacterStatSO : ScriptableObject
{
    public float maxHealth = 100f;
    public float attackPower = 100f;
    public float defense = 0f;
    public float moveSpeed = 5f;
    public float jumpForce = 3f;
}
