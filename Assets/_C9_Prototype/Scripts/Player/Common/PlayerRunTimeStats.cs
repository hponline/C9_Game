using UnityEngine;

public class PlayerRunTimeStats : MonoBehaviour
{
    public static PlayerRunTimeStats Instance;

    [Header("Base Stats -- SO dan geliyor")]
    public float baseHealth;
    public float baseDamage;
    float baseMoveSpeed;
    public float baseAttackSpeed;
    float jumpForce;
    public float baseCritChance;
    float baseCritMultiplier;

    [SerializeField] float currentHealth;


    [Header("RUNTIME STATS")]
    public float MaxHealth { get; private set; }
    public float CurrentHealth => currentHealth;
    public float Health => baseHealth * BuffController.Instance.GetMultiplier(StatType.Health);
    public float Damage => baseDamage * BuffController.Instance.GetMultiplier(StatType.AttackDamage);
    public float MoveSpeed => baseMoveSpeed * BuffController.Instance.GetMultiplier(StatType.MoveSpeed);
    public float AttackSpeed => baseAttackSpeed * BuffController.Instance.GetMultiplier(StatType.AttackSpeed);
    public float JumpForce => jumpForce * BuffController.Instance.GetMultiplier(StatType.Health); // Opsiyonel
    public float CritChange => baseCritChance * BuffController.Instance.GetMultiplier(StatType.CritChange);
    public float CritMultiplier => baseCritMultiplier * BuffController.Instance.GetMultiplier(StatType.Health);

    private void Awake()
    {
        Instance = this;
    }

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

    void RecalculateHealth()
    {
        MaxHealth = baseHealth;
        currentHealth = MaxHealth;
    }

    public void UpgradeStats(ValueType type, float value)
    {
        switch (type)
        {
            case ValueType.AttackDamage:
                baseDamage += value;
                break;
            case ValueType.AttackSpeed:
                baseAttackSpeed = Mathf.Clamp(baseAttackSpeed, 1, 15);
                baseAttackSpeed += value;
                break;
            case ValueType.Health:
                baseHealth += value;
                MaxHealth = baseHealth;
                currentHealth += value;
                break;
            case ValueType.CritChange:
                baseCritChance += value;
                break;
        }
    }
}
