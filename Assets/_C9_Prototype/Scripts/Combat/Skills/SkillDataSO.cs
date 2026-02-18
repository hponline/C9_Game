using UnityEngine;

[CreateAssetMenu(menuName = "C9/SkillData")]
public class SkillDataSO : ScriptableObject
{
    [Header("GamePlay")]
    public int damage = 50;
    public float cooldown = 5f;
    public float skillRange = 10f;
    public float damageRadius = 5f;
    public float pullRadius = 10f;
    public float pullForce = 5f;
    public float tickInterval = 0.15f;
    public float projectileSpeed = 5f;
    public float variance = 0.2f;

    [Header("Presentation")]
    public Sprite skillIcon;
    public string animationTriggerName;
    public GameObject skillPrefab; // VFX/Projectile prefab
    public LayerMask hitMask;
    public AudioClip soundEffect;
}
