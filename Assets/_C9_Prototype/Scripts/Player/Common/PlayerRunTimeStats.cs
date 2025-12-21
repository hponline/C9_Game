using UnityEngine;

public class PlayerRunTimeStats : MonoBehaviour
{
    [Header("Base Stats -- SO dan geliyor")]
    float baseHealth;
    float baseDamage;
    float baseMoveSpeed;
    float baseAttackSpeed;
    float jumpForce;

    float currentHealth;

    [Header("Stat Çarpanlarý")]
    [Tooltip("1f deðerin kendisi (1.2f) %20 buff // 0.5f %50 debuff anlamýna gelir")]    
    float healthMultiplier = 1f;
    float damageMultiplier = 1f;
    float moveSpeedMultiplier = 1f;
    float attackSpeedMultiplier = 1f;
    float jumpForceMultiplier = 1f;

    [Header("RUNTIME STATS")]
    public float MaxHealth { get; private set; }
    public float CurrentHealth => currentHealth;
    public float Health => baseHealth * healthMultiplier;
    public float Damage => baseDamage * damageMultiplier;
    public float MoveSpeed => baseMoveSpeed * moveSpeedMultiplier;
    public float AttackSpeed => baseAttackSpeed * attackSpeedMultiplier;
    public float JumpForce => jumpForce * jumpForceMultiplier;
    // Player Statlarý levele göre artmýcak

    public void Init(PlayerConfigSO playerConfigSO)
    {
        baseHealth = playerConfigSO.baseHealth;
        baseDamage = playerConfigSO.baseDamage;
        baseMoveSpeed = playerConfigSO.baseMoveSpeed;
        baseAttackSpeed = playerConfigSO.baseAttackSpeed;
        jumpForce = playerConfigSO.jumpForce;

        RecalculateHealth();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, MaxHealth);

        if (currentHealth < 0)
        {
            Debug.Log($"Player {this.name} öldü");
        }
    }

    public void SetHealth(float amount)
    {
        currentHealth = Mathf.Min(MaxHealth, CurrentHealth + amount);        
    }


    #region Buff

    public void AppylHealhtBuff(float multiplier)
    {
        // Heal alma vfx
        healthMultiplier *= multiplier;
        RecalculateHealth();
    }

    public void ApplyAttackDamageBuff(float multiplier)
    {
        baseDamage *= multiplier;
    }

    public void AppylAttackSpeedBuff(float multiplier)
    {
        attackSpeedMultiplier *= multiplier;
    }

    #endregion

    void RecalculateHealth()
    {
        MaxHealth = baseHealth * healthMultiplier;
        currentHealth = MaxHealth;
    }
}
