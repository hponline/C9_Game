using UnityEngine;
using UnityEngine.UI;

public class PlayerUIHealthBar : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] Image playerHealthImage;

    private void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void OnEnable()
    {
        playerHealth.OnDamaged += OnDamaged;
    }
    private void OnDisable()
    {
        playerHealth.OnDamaged -= OnDamaged;
    }

    void OnDamaged(DamageContext ctx)
    {
        UpdateHealthBar(playerHealth.CurrentHealth, playerHealth.MaxHealth);
    }

    void UpdateHealthBar(float current, float max)
    {
        float target = current / max;
        playerHealthImage.fillAmount = target;        
    }
}
