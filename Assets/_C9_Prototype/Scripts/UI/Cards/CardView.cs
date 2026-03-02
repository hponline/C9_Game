using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    public Image cardSprite;
    public TextMeshProUGUI cardTxt;
    public TextMeshProUGUI cardValue;

    CardInstance cardInstance;

    [Header("Button")]
    public Button cardButton;

    public void Setup(CardInstance card)
    {
        cardButton.onClick.RemoveAllListeners();

        cardInstance = card;
        ShowCard();

        cardButton.onClick.AddListener(OnCardSelected);
    }

    void ShowCard()
    {
        cardSprite.sprite = cardInstance.cardDataSO.cardSprite;
        cardTxt.SetText(cardInstance.cardDataSO.cardName);
        cardValue.SetText("{0:0}", cardInstance.randomValue);
    }

    void OnCardSelected()
    {
        PlayerRunTimeStats.Instance.UpgradeStats(cardInstance.cardDataSO.valueType, cardInstance.randomValue);
        UIManager.Instance.HideCardPanel();
    }
}
