using UnityEngine;

public class CardInstance
{
    public CardDataSO cardDataSO;
    public int randomValue;
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
        int temp = (int)Random.Range(min, max);
        randomValue = temp;
    }
}
