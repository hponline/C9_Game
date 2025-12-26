using System.Collections;
using UnityEngine;

public class PlayerSkillController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] SkillDataSO basicAttackData;
    //[SerializeField] public SkillDataSO[] skillDataSO;
    [SerializeField] SkillBehaviour basicAttackSkill;
    //[SerializeField] SkillBehaviour[] skillSlots;

    public SkillDataSO BasicAttackData => basicAttackData;

    //[Header("Variables")]
    //[SerializeField] float basicAttackCooldownTimer;
    //[SerializeField] float[] slotCooldownTimer;

    Animator animator;
    // Pending state: anim beklenirken hangi skill hangi kaynak ile iliþkilendirildi
    int pendingIndex = -1; // -1 
    IAttackSource cachedSource = null; // Saldýranýn sahibi (player)

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        //slotCooldownTimer = new float[skillSlots.Length];
    }

    private void Update()
    {
        //SkillTimerLimit();
    }

    //public SkillDataSO GetSkillDataSO(int index)
    //{
    //    if (skillDataSO == null) return null;
    //    if (index < 0 || index >= skillDataSO.Length) return null;
    //    return skillDataSO[index];
    //}

    public void UseBasicAttack(IAttackSource source)
    {
        //if (basicAttackSkill == null) return;
        //if (basicAttackCooldownTimer > 0f) return;

        cachedSource = source;
        pendingIndex = -1; // basic attack

        animator.SetTrigger(basicAttackData.animationTriggerName);
        //basicAttackCooldownTimer = basicAttackData.cooldown;
    }

    //public void UseSkillSlot(int index, IAttackSource source, bool viaAnimation = true)
    //{
    //    if (index < 0 || index >= skillSlots.Length) return;
    //    var skill = skillSlots[index];
    //    if (skill == null || index >= skillSlots.Length) return;
    //    if (slotCooldownTimer[index] > 0f) return;

    //    var skillPrefab = skillSlots.Length > index ? skillSlots[index] : null;
    //    slotCooldownTimer[index] = skill.Data.cooldown;

    //    cachedSource = source;
    //    if (viaAnimation && !string.IsNullOrEmpty(skill.Data.animationTriggerName))
    //    {
    //        pendingIndex = index;
    //        animator.SetTrigger(skill.Data.animationTriggerName);

    //        if (skill.Data.hitDelay > 0f && skillPrefab != null)
    //            StartCoroutine(ExecuteAfterDelay(skill.Data.hitDelay, skillPrefab, source));
    //    }
    //    else
    //    {
    //        skillPrefab?.Execute(source);
    //        pendingIndex = -1;
    //        cachedSource = null;
    //    }

    //    Debug.Log("Skill atýldý");
    //}
    IEnumerator ExecuteAfterDelay(float delay, SkillBehaviour prefab, IAttackSource source)
    {
        yield return new WaitForSeconds(delay);
        if (prefab != null) prefab.Execute(source);

        pendingIndex = -1;
        cachedSource = null;
    }

    //void SkillTimerLimit()
    //{
    //    basicAttackCooldownTimer = Mathf.Max(0f, basicAttackCooldownTimer - Time.time);

    //    for (int i = 0; i < slotCooldownTimer.Length; i++)
    //    {
    //        slotCooldownTimer[i] = Mathf.Max(0f, slotCooldownTimer[i] - Time.deltaTime);
    //    }
    //}


    public void OnAnimationHit()
    {
        if (cachedSource == null) return;
        basicAttackSkill.Execute(cachedSource);

        pendingIndex = -1;
        cachedSource = null;
    }
}
