using System.Collections;
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

    IAttackSource attackSource;
    SkillBehaviour currentSkill;

    Animator animator;
    IAttackSource cachedSource = null; // Saldýranýn sahibi (player)

    // Skill
    public bool isBusy;


    private void Awake()
    {
        inputHandler = GetComponent<InputHandler>();
        animator = GetComponentInChildren<Animator>();
        attackSource = GetComponent<IAttackSource>();
    }

    private void Start()
    {
        Debug.Log("Yeni skil ekle veya Mevcut skile efekt ekle");
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
    // Baþka yere taþýnabilir
    public void UseBasicAttack(IAttackSource source)
    {
        cachedSource = source;
        animator.SetTrigger(GameTags.PlayerAnimationTags.PLAYER_ATTACK_TAG);
    }

    public void OnAnimationHit()
    {
        if (cachedSource == null) return;
        basicAttackSkill.Execute();
        cachedSource = null;
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
