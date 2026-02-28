using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] GameManager gameManager;

    [SerializeField] List<CardDataSO> cardDataSOList;
    [SerializeField] CardView cardPrefab;
    [SerializeField] Transform cardContainer;
    [SerializeField] int cardCounter;

    [SerializeField] GameObject[] cardUIPanel;

    private void Awake()
    {
        Instance = this;
        Debug.Log("Her level aldýgýnda kartlar stackleniyor ");
        Debug.Log("level atladýgýnda max 'cardCounter' sayýsý kadar kart gösterilmeli ve resetlenmeli");
        Debug.Log("card Stat çarpanlarý yüzliðe çevrilecek critchange örnek: 0-1 arasý, attack damage %10-15");
        Debug.Log("Health arttýgýnda UI güncellenmiyor düzelt");
    }

    void CardSpawn()
    {
        for (int i = 0; i < cardCounter; i++)
        {
            CardDataSO randomData = cardDataSOList[Random.Range(0, cardDataSOList.Count)];
            CardInstance instance = new CardInstance(randomData);
            CardView cardView = Instantiate(cardPrefab, cardContainer);
            cardView.Setup(instance);
        }
        ShowCardPanel();
    }

    void ShowCardPanel()
    {
        StopTime();
        cardUIPanel[0].SetActive(true);
    }

    public void HideCardPanel()
    {
        foreach (var cardPanel in cardUIPanel)
        {
            cardPanel.SetActive(false);
        }
        StartTime();
    }

    void StartTime() => Time.timeScale = 1.0f;

    void StopTime() => Time.timeScale = 0f;


    private void OnEnable()
    {
        gameManager.OnLevelUp += CardSpawn;
    }
    private void OnDisable()
    {
        gameManager.OnLevelUp -= CardSpawn;
    }
}
