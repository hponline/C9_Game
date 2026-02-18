using DG.Tweening;
using UnityEngine;

public class YoneUltSkill : SkillBehaviour
{
    Rigidbody rb;
    YoneUltHitBox yoneUltHitbox;
    Character_AfterImage character_AfterImage;

    [SerializeField] float dashDistance;
    [SerializeField] float dashDuration;
    [SerializeField] ParticleSystem duskEffect;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        yoneUltHitbox = GetComponentInChildren<YoneUltHitBox>();
        character_AfterImage = GetComponentInChildren<Character_AfterImage>();
    }

    public override void Execute()
    {
        StartDash();
        PlayDuskEffect();
        TriggerCameraEffect();
        character_AfterImage.StartAfterImage();

        yoneUltHitbox.ResetHits();
        yoneUltHitbox.gameObject.SetActive(true);
    }

    public override void Stop()
    {
        character_AfterImage.StopAfterImage();
        StopDuskEffect();
        YoneUltDamage();

        yoneUltHitbox.gameObject.SetActive(false);
    }

    public void PlayDuskEffect()
    {
        duskEffect.Play();
    }
    public void StopDuskEffect()
    {
        duskEffect.Stop();
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
            var ctx = DamageCalculator.Calculate(skillData, playerRunTimeStats, hit.hitPoint, hit.hitNormal);
            hit.damageable.TakeDamage(ctx);
        }
        if (yoneUltHitbox.hitTargets.Count > 0)        
            HitStopManager.instance.PlayHitStop();        
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
            .SetEase(Ease.OutExpo)
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
