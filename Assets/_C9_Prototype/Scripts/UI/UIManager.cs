using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("References")]
    [SerializeField] GameManager gameManager;
    [SerializeField] PlayerRunTimeStats playerRuntimeStats;
    [SerializeField] PlayerHealth playerHealth;

    [Header("Card UI")]
    [SerializeField] List<CardDataSO> cardDataSOList;
    [SerializeField] CardView cardPrefab;
    [SerializeField] Transform cardContainer;
    [SerializeField] int cardCounter;
    [SerializeField] Transform cardUIPanel;

    [Header("Player Panel Stat UI")]
    [SerializeField] TextMeshProUGUI attackDamageValue;
    [SerializeField] TextMeshProUGUI attackSpeedValue;
    [SerializeField] TextMeshProUGUI critChangeValue;
    [SerializeField] TextMeshProUGUI healthValue;

    [Header("Player Panel Stat UI")]
    [SerializeField] GameObject[] panels;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateStatUI();
    }

    public void DeadPanel()
    {
        CloseAllPanels();
        gameManager.CursorOpen();
        panels[1].gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
    void CloseAllPanels()
    {
        foreach (var panel in panels)
            panel.SetActive(false);
        
        Time.timeScale = 1f;
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
        Time.timeScale = 0f;
        cardUIPanel.gameObject.SetActive(true);
    }

    public void HideCardPanel()
    {
        CloseAllPanels();
    }

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
        playerHealth.OnDied += DeadPanel;
    }
    private void OnDisable()
    {
        gameManager.OnLevelUp -= CardSpawn;
        playerRuntimeStats.OnStatsChanged -= UpdateStatUI;
        playerHealth.OnDied -= DeadPanel;
    }
}
