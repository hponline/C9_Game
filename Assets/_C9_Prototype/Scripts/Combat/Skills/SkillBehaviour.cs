using UnityEngine;

public abstract class SkillBehaviour : MonoBehaviour
{
    [SerializeField] protected SkillDataSO skillData;

    public SkillDataSO Data => skillData;
    public abstract void Execute(IAttackSource source);
}
