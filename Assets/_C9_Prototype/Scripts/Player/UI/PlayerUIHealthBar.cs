using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIHealthBar : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] Image playerHealthImage;

    [SerializeField] TextMeshProUGUI playerHealthTxt;
    [SerializeField] PlayerRunTimeStats playerRunTimeStats;

    private void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerHealthTxt = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        playerHealth.OnDamaged += OnDamaged;
        playerHealth.OnHealthChanged += UpdateHealthBar;
    }
    private void OnDisable()
    {
        playerHealth.OnDamaged -= OnDamaged;
        playerHealth.OnHealthChanged -= UpdateHealthBar;
    }

    void OnDamaged(DamageContext ctx)
    {
        UpdateHealthBar(playerHealth.CurrentHealth, playerHealth.MaxHealth);
    }

    void UpdateHealthBar(float current, float max)
    {
        float target = current / max;
        playerHealthImage.fillAmount = target;  
        
        ShowHealthTxt();
    }

    void ShowHealthTxt()
    {
        playerHealthTxt.SetText("{0:0} / {1}", playerHealth.CurrentHealth, playerHealth.MaxHealth);
    }
}
