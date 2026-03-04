using UnityEngine;

public class CardInstance
{
    public CardDataSO cardDataSO;
    public float randomValue;
    public string displayValue;

    public CardInstance(CardDataSO cardDataSO)
    {
        this.cardDataSO = cardDataSO;
        Calculate();
    }

    void Calculate()
    {
        float min;
        float max;
        float percent;
        float minVariance = 0.05f;
        float maxVariance = 0.20f;
        PlayerRunTimeStats stats = PlayerRunTimeStats.Instance;

        switch (cardDataSO.valueType)
        {
            case ValueType.AttackDamage:
                min = stats.baseDamage * minVariance;
                max = stats.baseDamage * maxVariance;
                randomValue = Mathf.Round(Random.Range(min, max));
                percent = (randomValue / stats.baseDamage) * 100;
                displayValue = $"+{percent:0}%";
                break;

            case ValueType.AttackSpeed:
                min = stats.baseAttackSpeed * minVariance;
                max = stats.baseAttackSpeed * maxVariance;
                randomValue = Random.Range(min, max);
                percent = (randomValue / stats.baseAttackSpeed) * 100;
                displayValue = $"+{percent:0}%";
                break;

            case ValueType.Health:
                min = stats.baseHealth * minVariance;
                max = stats.baseHealth * maxVariance;
                randomValue = Random.Range(min, max);
                percent = (randomValue / stats.baseHealth) * 100;
                displayValue = $"+{percent:0}%";
                break;

            case ValueType.CritChange:
                min = stats.baseCritChance * minVariance;
                max = stats.baseCritChance * maxVariance;
                randomValue = Random.Range(min, max);
                percent = (randomValue / stats.baseCritChance) * 100;
                displayValue = $"+{percent:0}%"; // randomValue ile displayvalue uyuþuyor mu bak
                break;
        }
    }
}
