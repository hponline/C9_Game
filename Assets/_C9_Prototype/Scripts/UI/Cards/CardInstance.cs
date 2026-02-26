using UnityEngine;

public class CardInstance
{
    public CardDataSO cardDataSO;
    public float randomValue;
    float variance = 0.2f;

    public CardInstance(CardDataSO cardDataSO)
    {
        this.cardDataSO = cardDataSO;
        Calculate();
    }

    void Calculate()
    {
        float min = cardDataSO.CardValue * (1 - variance);
        float max = cardDataSO.CardValue * (1 + variance);
        randomValue = Random.Range(min, max);
    }
}
