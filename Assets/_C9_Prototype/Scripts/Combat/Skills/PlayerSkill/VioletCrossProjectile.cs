using UnityEngine;

public class VioletCrossProjectile : MonoBehaviour
{
    LayerMask enemyLayer;
    float damage;
    float speed;
    Vector3 direction;

    public void Init(Vector3 dir, float speed, float damage, LayerMask enemyLayer)
    {
        this.direction = dir;
        this.speed = speed;
        this.damage = damage;
        this.enemyLayer = enemyLayer;
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        int enemyLayer = LayerMask.NameToLayer("Enemy");

        if (other.gameObject.layer != enemyLayer) return;
        if (!other.TryGetComponent<IDamageable>(out var damageable)) return;

        damageable.TakeDamage(new DamageContext
        {
            amount = damage,
            hitPoint = other.transform.position,
            hitNormal = (other.transform.position - transform.position).normalized,
        });
    }
}
