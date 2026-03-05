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
                randomValue = Mathf.RoundToInt(Random.Range(min, max));
                percent = (randomValue / stats.baseDamage) * 100;
                displayValue = $"+{percent:0}%";
                break;

            case ValueType.AttackSpeed:
                min = minVariance;
                max = maxVariance;
                randomValue = Random.Range(min, max);
                displayValue = $"+{randomValue * 100:0}%";
                break;

            case ValueType.Health:
                min = stats.baseHealth * minVariance;
                max = stats.baseHealth * maxVariance;
                randomValue = Mathf.RoundToInt(Random.Range(min, max));
                percent = (randomValue / stats.baseHealth) * 100;
                displayValue = $"+{percent:0}%";
                break;

            case ValueType.CritChange:
                min = minVariance;
                max = 0.10f;
                randomValue = Random.Range(min, max);
                displayValue = $"+{randomValue * 100:0}%";
                break;
        }
    }
}
