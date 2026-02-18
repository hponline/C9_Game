using UnityEngine;

public static class DamageCalculator
{
    public static DamageContext Calculate(SkillDataSO skillDataSO, PlayerRunTimeStats stats, Vector3 hitPoint, Vector3 hitNormal)
    {
        float totalBaseDamage = skillDataSO.damage + stats.Damage;

        float minDamage = totalBaseDamage * (1 - skillDataSO.variance);
        float maxDamage = totalBaseDamage * (1 + skillDataSO.variance);

        float rollDamage = Random.Range(minDamage, maxDamage);

        bool isCrit = Random.value < stats.CritChange;
        if (isCrit)
            rollDamage *= stats.CritMultiplier;

        return new DamageContext(rollDamage, isCrit, hitPoint, hitNormal);
    }

    public static DamageContext EnemyCalculate(EnemyConfigSO EnemyDataSO, EnemyRunTimeStats stats, Vector3 hitPoint, Vector3 hitNormal)
    {
        float damage = EnemyDataSO.baseDamage + stats.Damage;
        return new DamageContext(damage, false, hitPoint, hitNormal);
    }
}
