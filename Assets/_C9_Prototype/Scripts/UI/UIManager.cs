using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("References")]
    [SerializeField] GameManager gameManager;
    [SerializeField] PlayerRunTimeStats playerRuntimeStats;

    [Header("Card UI")]
    [SerializeField] List<CardDataSO> cardDataSOList;
    [SerializeField] CardView cardPrefab;
    [SerializeField] Transform cardContainer;
    [SerializeField] int cardCounter;
    [SerializeField] GameObject[] cardUIPanel;

    [Header("Player Panel Stat UI")]
    [SerializeField] TextMeshProUGUI attackDamageValue;
    [SerializeField] TextMeshProUGUI attackSpeedValue;
    [SerializeField] TextMeshProUGUI critChangeValue;
    [SerializeField] TextMeshProUGUI healthValue;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateStatUI();
    }

    void CardSpawn()
    {
        ClearCards();

        for (int i = 0; i < cardCounter; i++)
        {
            CardDataSO randomData = cardDataSOList[Random.Range(0, cardDataSOList.Count)];
            CardInstance instance = new CardInstance(randomData);
            CardView cardView = Instantiate(cardPrefab, cardContainer);
            cardView.Setup(instance);
        }

        ShowCardPanel();
    }

    void ClearCards()
    {
        foreach (Transform card in cardContainer)
        {
            Destroy(card.gameObject);
        }
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


    void UpdateStatUI()
    {
        attackDamageValue.SetText("{0:0}", playerRuntimeStats.Damage);
        attackSpeedValue.SetText("{0:0.0}", playerRuntimeStats.AttackSpeed);
        critChangeValue.SetText("%{0:0}", playerRuntimeStats.CritChange * 100);
        healthValue.SetText("{0:0}", playerRuntimeStats.MaxHealth);
    }

    private void OnEnable()
    {
        gameManager.OnLevelUp += CardSpawn;
        playerRuntimeStats.OnStatsChanged += UpdateStatUI;
    }
    private void OnDisable()
    {
        gameManager.OnLevelUp -= CardSpawn;
        playerRuntimeStats.OnStatsChanged -= UpdateStatUI;
    }
}
