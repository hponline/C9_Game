using System;
using UnityEngine;

public class PlayerVFX : MonoBehaviour
{
    [SerializeField] ParticleSystem slashVFX;
    [SerializeField] TrailRenderer whirlWindSkillTrailRenderer;

    public void AttackSlashEffect()
    {
        slashVFX.Play();
    }

    public void StartWhirlwindTrail()
    {
        whirlWindSkillTrailRenderer.Clear();
        whirlWindSkillTrailRenderer.emitting = true;
    }
    public void EndWhirlwindTrail()
    {
        whirlWindSkillTrailRenderer.emitting = false;
    }
}
