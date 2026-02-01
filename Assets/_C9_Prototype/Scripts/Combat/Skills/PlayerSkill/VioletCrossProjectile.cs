using DG.Tweening;
using System.Collections;
using UnityEngine;

public class VioletCrossProjectile : MonoBehaviour
{
    Vector3 direction;
    Vector3 startPos;

    float damage;
    float speed;
    float range;
    bool isDying;

    public void Init(Vector3 dir, float speed, float damage, float range)
    {
        this.direction = dir;
        this.speed = speed;
        this.damage = damage;
        this.range = range;

        startPos = transform.position;
    }

    private void Update()
    {
        Projectile();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;
        if (!other.TryGetComponent<IDamageable>(out var damageable)) return;

        if (other.TryGetComponent<Rigidbody>(out var rb))
        {
            StartCoroutine(PushOverTime(rb, other.transform.localPosition, 5, .25f));
        }


        damageable.TakeDamage(new DamageContext
        {
            amount = damage,
            hitPoint = other.transform.position,
            hitNormal = (other.transform.position - transform.position).normalized,
        });
    }

    IEnumerator PushOverTime(Rigidbody rb, Vector3 dir, float force, float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            rb.AddForce(dir * force, ForceMode.Force);
            t += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

    void Projectile()
    {
        if (isDying) return;

        transform.position += direction * speed * Time.deltaTime;

        float distance = Vector3.Distance(startPos, transform.position);
        if (distance > range)
        {
            StartDestroy();
        }
    }

    void StartDestroy()
    {
        isDying = true;
        Vector3 targetPos = transform.position + transform.forward * speed + Vector3.down * 5;
        transform.DOMove(targetPos, .5f).SetEase(Ease.InOutSine).OnComplete(() => Destroy(gameObject));
    }
}
