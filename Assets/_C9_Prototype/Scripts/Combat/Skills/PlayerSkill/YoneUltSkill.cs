using DG.Tweening;
using UnityEngine;

public class YoneUltSkill : SkillBehaviour
{
    Rigidbody rb;
    YoneUltHitBox yoneUltHitbox;
    Character_AfterImage character_AfterImage;

    [SerializeField] float dashDistance;
    [SerializeField] float dashDuration;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        yoneUltHitbox = GetComponentInChildren<YoneUltHitBox>();
        character_AfterImage = GetComponentInChildren<Character_AfterImage>();
    }

    public override void Execute()
    {
        StartDash();

        character_AfterImage.StartAfterImage();

        yoneUltHitbox.ResetHits();
        yoneUltHitbox.gameObject.SetActive(true);
    }

    public override void Stop()
    {
        character_AfterImage.StopAfterImage();

        YoneUltDamage();
        yoneUltHitbox.gameObject.SetActive(false);
    }

    public void YoneUltDamage()
    {
        foreach (var hit in yoneUltHitbox.hitTargets)
        {
            if (hit.damageable is Component c &&
                c.TryGetComponent<Rigidbody>(out var rb))
            {
                Vector3 dir = (transform.position - rb.position).normalized;
                rb.AddForce(dir * skillData.pullForce, ForceMode.Impulse);
            }

            hit.damageable.TakeDamage(new DamageContext
            {
                amount = skillData.damage,
                hitPoint = hit.hitPoint,
                hitNormal = hit.hitNormal,
            });
        }
    }

    void StartDash()
    {
        rb.useGravity = false;
        rb.isKinematic = true;

        Vector3 direction = transform.forward;
        Vector3 targetPos = transform.position + (direction * dashDistance);
        Vector3 rayOrigin = transform.position + Vector3.up;
        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, direction, out hit, dashDistance, skillData.hitMask))
        {
            targetPos = hit.point - direction;
            targetPos.y = transform.position.y;
        }

        transform.DOMove(targetPos, dashDuration)
            .SetEase(Ease.InOutExpo)
            .OnComplete(() =>
            {
                rb.useGravity = true;
                rb.isKinematic = false;
            });
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.forward * dashDistance);
    }
}
