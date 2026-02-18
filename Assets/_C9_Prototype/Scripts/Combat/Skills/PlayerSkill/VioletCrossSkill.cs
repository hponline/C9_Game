using UnityEngine;

public class VioletCrossSkill : SkillBehaviour
{
    [SerializeField] VioletCrossProjectile violetCrossProjectilePrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] ParticleSystem[] violetCrossParticle;


    public override void Execute()
    {
        var projectile = Instantiate(violetCrossProjectilePrefab, firePoint.position, firePoint.rotation);
        StartParticle();
        TriggerCameraEffect();
        projectile.Init(firePoint.forward, skillData, playerRunTimeStats);
    }

    public override void Stop()
    {
        StopParticle();
    }
    void StartParticle()
    {
        foreach (var item in violetCrossParticle)
        {
            item.Play();
        }
    }
    void StopParticle()
    {
        foreach (var item in violetCrossParticle)
        {
            item.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }
}
