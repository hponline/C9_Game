using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public EnemyManager enemyManager;
    [SerializeField] TextMeshProUGUI enemyCountTxt;
    [SerializeField] TextMeshProUGUI levelTxt;

    [Header("Level Up")]
    [SerializeField] int currentExp;
    [SerializeField] int expToLevel = 100;
    [SerializeField] float expGrowthMultiplier = 1.2f;
    public Slider expSlider;
    public TextMeshProUGUI expTxt;
    public TextMeshProUGUI LvlTxt;


    public event Action OnLevelUp;
    public int globalLevel = 1;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CursorLock();

        enemyManager.StartNextWave();
        ShowEnemyCount(enemyManager.AliveEnemyCount);
    }

    public void CursorOpen()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void CursorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    #region LevelUP

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
        globalLevel++;
        currentExp -= expToLevel;
        expToLevel = Mathf.RoundToInt(expToLevel * expGrowthMultiplier);

        OnLevelUp?.Invoke();
        CursorOpen();
    }

    public void UpdateUI()
    {
        expSlider.maxValue = expToLevel;
        expSlider.value = currentExp;

        float expPercent = (float) currentExp / expToLevel * 100f;

        expTxt.SetText("Exp %{0:0} ", expPercent);
        LvlTxt.SetText("{0} ", globalLevel);
    }

    #endregion


    public void ShowEnemyCount(int count)
    {
        enemyCountTxt.SetText("Enemy: {0}/{1} ", count, enemyManager.enemiesToSpawnPerLevel);        
    }

    public void ShowLevel()
    {
        levelTxt.SetText("Level: {0} ", globalLevel + 1);
    }

    private void OnEnable()
    {
        EnemyHealth.OnExpGain += GainExperience;
        enemyManager.OnAliveEnemyChanged += ShowEnemyCount;
    }
    private void OnDisable()
    {
        EnemyHealth.OnExpGain -= GainExperience;
        enemyManager.OnAliveEnemyChanged -= ShowEnemyCount;
    }
}
