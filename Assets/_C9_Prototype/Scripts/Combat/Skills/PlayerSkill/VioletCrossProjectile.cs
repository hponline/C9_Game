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

    SkillDataSO skillDataSO;
    PlayerRunTimeStats playerRunTimeStats;

    public void Init(Vector3 dir, SkillDataSO skillDataSO, PlayerRunTimeStats stats)
    {
        this.direction = dir;
        this.speed = skillDataSO.projectileSpeed;
        this.damage = skillDataSO.damage + stats.Damage;
        this.range = skillDataSO.skillRange;
        this.skillDataSO = skillDataSO;
        this.playerRunTimeStats = stats;

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

        Vector3 hitPoint = other.ClosestPoint(transform.position);
        Vector3 hitNormal = (other.transform.position - transform.position).normalized;

        if (other.TryGetComponent<Rigidbody>(out var rb))
        {
            StartCoroutine(PushOverTime(rb, hitPoint, 5, .25f));
        }

        var ctx = DamageCalculator.Calculate(skillDataSO, playerRunTimeStats, hitPoint, hitNormal);
        damageable.TakeDamage(ctx);
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
