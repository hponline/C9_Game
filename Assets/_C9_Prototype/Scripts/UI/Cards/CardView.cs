using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    public Image cardSprite;
    public TextMeshProUGUI cardTxt;
    public TextMeshProUGUI cardValue;

    CardInstance cardInstance;

    public void Setup(CardInstance card)
    {
        cardInstance = card;
        ShowCard();
    }

    void ShowCard()
    {
        cardSprite.sprite = cardInstance.cardDataSO.cardSprite;
        cardTxt.text = cardInstance.cardDataSO.cardName;
        cardValue.text = cardInstance.randomValue.ToString("0");
    }
}
