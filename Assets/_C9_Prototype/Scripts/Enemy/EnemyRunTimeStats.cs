using UnityEngine;

public class EnemyRunTimeStats : MonoBehaviour
{
    [Header("Base Stats -- SO dan geliyor")]
    float baseHealth;
    float baseDamage;
    float baseMoveSpeed;

    [SerializeField] float currentHealth;

    [Header("RUNTIME STATS")]
    public float MaxHealth { get; private set; }
    public float Damage { get; private set; }
    public float MoveSpeed => baseMoveSpeed;
    public float CurrentHealth => currentHealth;

    public void Init(EnemyConfigSO enemyConfigSO, int currentLvl)
    {
        baseHealth = enemyConfigSO.baseHealth * Mathf.Pow(enemyConfigSO.healthPerLevelMultiplier, currentLvl - 1);
        baseDamage = enemyConfigSO.baseDamage * Mathf.Pow(enemyConfigSO.damagePerLevelMultiplier, currentLvl - 1);

        baseMoveSpeed = enemyConfigSO.baseMoveSpeed;

        MaxHealth = baseHealth;
        Damage = baseDamage;
        currentHealth = MaxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, MaxHealth);
    }
}
