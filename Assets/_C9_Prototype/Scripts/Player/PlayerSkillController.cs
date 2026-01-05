using System.Collections;
using UnityEngine;

public class PlayerSkillController : MonoBehaviour
{
    [Header("References")]
    InputHandler inputHandler;
    [SerializeField] SkillBehaviour basicAttackSkill;
    [SerializeField] SkillBehaviour[] skills;


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

    void HandleSkillInput(int slot)
    {
        if (isBusy) return;
        if (slot < 0 || slot >= skills.Length) return;

        UseSkill(skills[slot]);
    }

    void UseSkill(SkillBehaviour skill)
    {
        isBusy = true;
        currentSkill = skill;
        animator?.SetTrigger(skill.PlayerSkillSOData.animationTriggerName);
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
