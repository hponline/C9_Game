using UnityEngine;

[System.Serializable]
public class SkillSlot
{
    public SkillBehaviour skillBehaviour;
    public float CooldownRemaining;
}

public class PlayerSkillController : MonoBehaviour
{
    [Header("References")]
    InputHandler inputHandler;
    [SerializeField] SkillBehaviour basicAttackSkill;
    [SerializeField] SkillSlot[] skillSlots;

    public SkillSlot[] GetSkillSlots() => skillSlots;

    SkillBehaviour currentSkill;
    Animator animator;

    // Skill
    public bool isBusy;


    private void Awake()
    {
        inputHandler = GetComponent<InputHandler>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        foreach (var slot in skillSlots)
        {
            if (slot.CooldownRemaining > 0f)
                slot.CooldownRemaining -= Time.deltaTime;
        }
    }

    #region Basic Attack
    public void UseBasicAttack()
    {
        animator.SetTrigger(GameTags.PlayerAnimationTags.PLAYER_ATTACK_TAG);
    }

    public void OnAnimationHit()
    {
        basicAttackSkill.Execute();
    }

    #endregion

    #region Skills

    void HandleSkillInput(int slotIndex)
    {
        if (isBusy) return;
        if (slotIndex < 0 || slotIndex >= skillSlots.Length) return;

        UseSkill(skillSlots[slotIndex]);
    }

    void UseSkill(SkillSlot slot)
    {
        if (slot.CooldownRemaining > 0f) return;

        isBusy = true;
        currentSkill = slot.skillBehaviour;
        slot.CooldownRemaining = slot.skillBehaviour.PlayerSkillSOData.cooldown;
        animator.SetTrigger(slot.skillBehaviour.PlayerSkillSOData.animationTriggerName);
    }

    public void OnSkillStart()
    {
        currentSkill?.Execute();
    }

    public void OnSkillEnd()
    {
        currentSkill?.Stop();
        currentSkill = null;
        isBusy = false;
    }

    #endregion

    private void OnEnable()
    {
        inputHandler.OnSkillInput += HandleSkillInput;
    }
    private void OnDisable()
    {
        inputHandler.OnSkillInput -= HandleSkillInput;
    }
}
