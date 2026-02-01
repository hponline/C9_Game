using System.Collections;
using UnityEngine;

public class VioletCrossSkill : SkillBehaviour
{
    [SerializeField] VioletCrossProjectile violetCrossProjectilePrefab;
    [SerializeField] Transform firePoint;


    public override void Execute()
    {
        var projectile = Instantiate(violetCrossProjectilePrefab, firePoint.position, firePoint.rotation);

        projectile.Init(firePoint.forward, skillData.projectileSpeed, skillData.damage, skillData.hitMask);
    }

    public override void Stop()
    {
        //
    }
}
