using UnityEngine;

public class PlayerRunTimeStats : MonoBehaviour
{
    [Header("Base Stats -- SO dan geliyor")]
    float baseHealth;
    int baseDamage;
    float baseMoveSpeed;
    float baseAttackSpeed;
    float jumpForce;
    float baseCritChance;
    float baseCritMultiplier;

    [SerializeField] float currentHealth;


    [Header("RUNTIME STATS")]
    public float MaxHealth { get; private set; }
    public float CurrentHealth => currentHealth;
    public float Health => baseHealth * BuffController.Instance.GetMultiplier(StatType.Health);
    public float Damage => baseDamage * BuffController.Instance.GetMultiplier(StatType.AttackDamage);
    public float MoveSpeed => baseMoveSpeed * BuffController.Instance.GetMultiplier(StatType.MoveSpeed);
    public float AttackSpeed => baseAttackSpeed * BuffController.Instance.GetMultiplier(StatType.AttackSpeed);
    public float JumpForce => jumpForce * BuffController.Instance.GetMultiplier(StatType.Health);
    public float CritChange => baseCritChance * BuffController.Instance.GetMultiplier(StatType.Health);
    public float CritMultiplier => baseCritMultiplier * BuffController.Instance.GetMultiplier(StatType.Health);

    public void Init(PlayerConfigSO playerConfigSO)
    {
        baseHealth = playerConfigSO.baseHealth;
        baseDamage = playerConfigSO.baseDamage;
        baseMoveSpeed = playerConfigSO.baseMoveSpeed;
        baseAttackSpeed = playerConfigSO.baseAttackSpeed;
        jumpForce = playerConfigSO.jumpForce;
        baseCritChance = playerConfigSO.critChangeMultiplier;
        baseCritMultiplier = playerConfigSO.critMultiplier;

        RecalculateHealth();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, MaxHealth);
    }

    public void SetHealth(float amount)
    {
        currentHealth = Mathf.Min(MaxHealth, currentHealth + amount);
    }

    void RecalculateHealth()
    {
        //MaxHealth = baseHealth * healthMultiplier;
        MaxHealth = baseHealth;
        currentHealth = MaxHealth;
    }
}
