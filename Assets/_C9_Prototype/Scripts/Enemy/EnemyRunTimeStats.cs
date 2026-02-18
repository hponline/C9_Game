using UnityEngine;

public class EnemyRunTimeStats : MonoBehaviour
{
    [Header("Base Stats -- SO dan geliyor")]
    float baseHealth;
    float baseDamage;
    float baseMoveSpeed;
    float baseAttackSpeed;

    [SerializeField] float currentHealth;

    [Header("RUNTIME STATS")]
    public float MaxHealth { get; private set; }
    public float Damage { get; private set; }
    public float MoveSpeed => baseMoveSpeed;
    public float AttackSpeed => baseAttackSpeed;
    public float CurrentHealth => currentHealth;

    public void Init(EnemyConfigSO enemyConfigSO, int level)
    {
        baseHealth = enemyConfigSO.baseHealth + enemyConfigSO.healthPerLevel * (level - 1);
        baseDamage = enemyConfigSO.baseDamage + enemyConfigSO.damagePerLevel * (level - 1);
        baseMoveSpeed = enemyConfigSO.baseMoveSpeed;
        baseAttackSpeed = enemyConfigSO.baseAttackSpeed;

        MaxHealth = baseHealth;
        Damage = baseDamage;
        currentHealth = MaxHealth;

        //  level çarpanlarýný yap
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, MaxHealth);        
    }
}
