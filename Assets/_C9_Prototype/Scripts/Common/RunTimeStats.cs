using UnityEngine;

public class RunTimeStats : MonoBehaviour
{
    [SerializeField] CharacterStatSO baseStats;

    public CharacterStatSO BaseStats => baseStats;

    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }
    public float AttackPower { get; private set; }
    public float Defense { get; private set; }
    public float MoveSpeed { get; private set; }
    public float JumpForce { get; private set; }

    private void Awake()
    {
        MaxHealth = baseStats.maxHealth;
        CurrentHealth = MaxHealth;
        AttackPower = baseStats.attackPower;
        Defense = baseStats.defense;
        MoveSpeed = baseStats.moveSpeed;
        JumpForce = baseStats.jumpForce;
    }

    public void ModifyHealth(float delta)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + delta, 0, MaxHealth);
    }
}
