using UnityEngine;

public enum SkillType
{
    Melee,
    Projectile,
    Area
}

[CreateAssetMenu(menuName = "C9/SkillData")]
public class SkillDataSO : ScriptableObject
{
    public SkillType skillType;
    public string displayName;
    public float damage = 50f;
    public float cooldown = 3f;
    public float skillRange = 10f;
    public float animDuration = 0.8f;
    public float hitDelay = 0.4f;

    public string animationTriggerName;
    public AnimationClip animationClip;
    public GameObject skillPrefab; // VFX/Projectile prefab
    public LayerMask hitMask;
    public AudioClip soundEffect;
}
