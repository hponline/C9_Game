using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerRunTimeStats))]
public class PlayerSetup : MonoBehaviour
{
    [Header("Exp")]
    [SerializeField] int level = 1;
    [SerializeField] int currentExp;
    [SerializeField] int expToLevel = 10;
    [SerializeField] float expGrowthMultiplier = 1.2f;
    public Slider expSlider;
    public TextMeshProUGUI expTxt;
    public TextMeshProUGUI LvlTxt;

    [Header("Config")]
    [SerializeField] PlayerConfigSO playerConfig;

    [Header("References")]
    PlayerRunTimeStats runTimeStats;

    private void Awake()
    {
        runTimeStats = GetComponent<PlayerRunTimeStats>();
        //health = GetComponent<PlayerHealth>();

        runTimeStats.Init(playerConfig);
    }

    public void GainExperience(int amont)
    {
        currentExp += amont;
        if (currentExp >= expToLevel) 
        {
            LevelUp();
        }
        UpdateUI();
    }

    public void LevelUp()
    {
        level++;
        currentExp -= expToLevel;
        expToLevel = Mathf.RoundToInt(expToLevel * expGrowthMultiplier);
    }

    public void UpdateUI()
    {
        expSlider.maxValue = expToLevel;
        expSlider.value = currentExp;
        expTxt.SetText("%{0} ", currentExp);
        LvlTxt.SetText("Level {0} ", level);
    }

    private void OnEnable()
    {
        EnemyHealth.OnExpGain += GainExperience;
    }
    private void OnDisable()
    {
        EnemyHealth.OnExpGain -= GainExperience;
    }
}
