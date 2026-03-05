using System;
using UnityEngine;

public class PlayerRunTimeStats : MonoBehaviour
{
    public static PlayerRunTimeStats Instance;

    public event Action OnStatsChanged;
    [SerializeField] BuffController buffController;

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
    public float Health => baseHealth * buffController.GetMultiplier(StatType.Health);
    public float Damage => baseDamage * buffController.GetMultiplier(StatType.AttackDamage);
    public float MoveSpeed => baseMoveSpeed * buffController.GetMultiplier(StatType.MoveSpeed);
    public float AttackSpeed => baseAttackSpeed * buffController.GetMultiplier(StatType.AttackSpeed);
    public float JumpForce => jumpForce * buffController.GetMultiplier(StatType.Health); // Opsiyonel
    public float CritChange => baseCritChance * buffController.GetMultiplier(StatType.CritChange);
    public float CritMultiplier => baseCritMultiplier * buffController.GetMultiplier(StatType.Health);

    private void Awake()
    {
        Instance = this;
        buffController = GetComponent<BuffController>();
    }

    private void OnEnable()
    {
        buffController.OnBuffAdded += HandleBuffChanged;
        buffController.OnBuffRemoved += HandleBuffChanged;
    }
    private void OnDisable()
    {
        buffController.OnBuffAdded -= HandleBuffChanged;
        buffController.OnBuffRemoved -= HandleBuffChanged;
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
    void RecalculateHealth()
    {
        MaxHealth = baseHealth;
        currentHealth = MaxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, MaxHealth);
    }


    void HandleBuffChanged()
    {
        OnStatsChanged?.Invoke();
    }

    public void UpgradeStats(ValueType type, float value)
    {
        switch (type)
        {
            case ValueType.AttackDamage:
                baseDamage += value;
                break;
            case ValueType.AttackSpeed:
                baseAttackSpeed += value;
                baseAttackSpeed = Mathf.Clamp(baseAttackSpeed, 1, 15);
                break;
            case ValueType.Health:
                baseHealth += value;
                MaxHealth = baseHealth;
                currentHealth += value;
                break;
            case ValueType.CritChange:
                baseCritChance += value;
                baseCritChance = Mathf.Clamp(baseCritChance, 0f, 1f);
                break;
        }

        OnStatsChanged?.Invoke();
    }

}
