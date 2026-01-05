using UnityEngine;

public abstract class SkillBehaviour : MonoBehaviour
{
    public abstract void Execute();
    public abstract void Stop();

    [SerializeField] protected SkillDataSO skillData;
    public SkillDataSO PlayerSkillSOData => skillData;

    //public abstract void Execute(IAttackSource source);
    //Player/Enemy skill atarsa farklý skiller atan birimler eklenirse dmg kim tarafýndan atýlýyor bilmek için
}
