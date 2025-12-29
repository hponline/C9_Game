using UnityEngine;
using UnityEngine.UI;

public class EnemyCanvasHealthBar : MonoBehaviour
{
    [SerializeField] Image healthImage;
    [SerializeField] EnemyHealth enemyHealth;

    Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;        
    }
    private void LateUpdate()
    {
        transform.LookAt(_camera.transform);
    }

    private void OnEnable()
    {
        enemyHealth.OnDamaged += OnDamaged;
    }
    private void OnDisable()
    {
        enemyHealth.OnDamaged -= OnDamaged;
    }

    void OnDamaged(DamageContext ctx)
    {
        UpdateHealthBar(enemyHealth.CurrentHealth, enemyHealth.MaxHealth);
    }

    void UpdateHealthBar(float current, float max)
    {
        float target = current / max;
        healthImage.fillAmount = target;
        if (healthImage.fillAmount == 0)
        {
            gameObject.SetActive(false);
        }
    }

}
