using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public EnemyManager enemyManager;
    [SerializeField] TextMeshProUGUI enemyCountTxt;
    [SerializeField] TextMeshProUGUI levelTxt;

    public int currentLevel = 1;

    private void Start()
    {
        enemyManager.StartNextWave();
        ShowEnemyCount(enemyManager.AliveEnemyCount);
    }

    public void ShowEnemyCount(int count)
    {
        enemyCountTxt.SetText("Alive Enemy: {0} ", count);        
    }

    public void ShowLevel()
    {
        levelTxt.SetText("Level: {0} ", currentLevel + 1);
    }

    private void OnEnable()
    {
        enemyManager.OnAliveEnemyChanged += ShowEnemyCount;
    }
    private void OnDisable()
    {
        enemyManager.OnAliveEnemyChanged -= ShowEnemyCount;
    }
}
