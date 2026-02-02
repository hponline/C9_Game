using UnityEngine;

public class VioletCrossSkill : SkillBehaviour
{
    [SerializeField] VioletCrossProjectile violetCrossProjectilePrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] ParticleSystem[] violetCrossParticle;


    public override void Execute()
    {
        var projectile = Instantiate(violetCrossProjectilePrefab, firePoint.position, firePoint.rotation);
        StartShockWawe();
        projectile.Init(firePoint.forward, skillData.projectileSpeed, skillData.damage, skillData.skillRange);
    }

    public override void Stop()
    {
        //
        StopShockWawe();
    }
    void StartShockWawe()
    {
        foreach (var item in violetCrossParticle)
        {
            item.Play();
        }
    }
    void StopShockWawe()
    {
        foreach (var item in violetCrossParticle)
        {
            item.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }
}
