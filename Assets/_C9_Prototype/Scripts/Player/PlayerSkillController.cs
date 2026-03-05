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
    [SerializeField] PlayerStateMachine playerStateMachine;
    [SerializeField] PlayerRunTimeStats playerRunTimeStats;
    [SerializeField] PlayerVFX playerVFX;

    public SkillSlot[] GetSkillSlots() => skillSlots;

    SkillBehaviour currentSkill;
    Animator animator;

    // Skill
    public bool isBusy;


    [Header("BasicAttack")]
    [SerializeField] float basicAttackTimer = 0;
    [SerializeField] float basicAttackCooldown;
    [SerializeField] bool isAttackLock;
    public bool IsAttackLocked => isAttackLock;


    private void Awake()
    {
        inputHandler = GetComponent<InputHandler>();
        animator = GetComponentInChildren<Animator>();
        playerStateMachine = GetComponent<PlayerStateMachine>();
        playerRunTimeStats = GetComponent<PlayerRunTimeStats>();
    }

    private void Start()
    {
        basicAttackCooldown = 1f / playerRunTimeStats.AttackSpeed;
    }

    private void Update()
    {
        foreach (var slot in skillSlots)
        {
            if (slot.CooldownRemaining > 0f)
                slot.CooldownRemaining -= Time.deltaTime;
        }

        if (basicAttackTimer > 0)
        {
            basicAttackTimer -= Time.deltaTime;
        }
        else
            isAttackLock = false;
    }

    #region Basic Attack
    public void UseBasicAttack()
    {
        if (isAttackLock) return;
        isAttackLock = true;

        animator.SetTrigger(GameTags.PlayerAnimationTags.PLAYER_ATTACK_TAG);
        animator.SetFloat("AttackSpeed", playerRunTimeStats.AttackSpeed);
        OnBasicAttackAnimationHit();

        basicAttackTimer = basicAttackCooldown;
    }
    public void SlashEffect()
    {
        playerVFX.AttackSlashEffect();
    }

    public void OnBasicAttackAnimationHit()
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
        if (!playerStateMachine.CanUseAbilities) return;
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
