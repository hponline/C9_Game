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

    public GameObject skillPrefab;
    public string animationTriggerName;
}
