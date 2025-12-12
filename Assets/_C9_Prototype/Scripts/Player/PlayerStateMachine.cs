using UnityEngine;

[RequireComponent(typeof(InputHandler))]
[RequireComponent(typeof(PlayerSkillController))]
public class PlayerStateMachine : MonoBehaviour
{
    [Header("References")]
    public InputHandler InputHandler;
    public PlayerMovement playerMovement;
    public PlayerSkillController playerSkillController;
    public Animator animator;
    public Health health;

    [Header("Skill State Setting")]
    public int pendingSkillIndex = 0;
    public IState idleState;
    IState runState;
    IState jumpState;
    IState skillState;
    IState basicAttackState;

    IState currentState;

    private void Awake()
    {
        idleState = new IdleState(this);
        runState = new RunState(this);
        jumpState = new JumpState(this);
        skillState = new SkillState(this);
        basicAttackState = new BasicAttackState(this);

        InputHandler = GetComponent<InputHandler>();
        animator = GetComponentInChildren<Animator>();
        health = GetComponent<Health>();
        playerSkillController = GetComponent<PlayerSkillController>();
    }

    private void Start()
    {
        ChangeState(idleState);
        // statemachine videosuna bak ve chatgpt örneðine bak 
    }

    private void Update()
    {
        if (health != null && !health.IsAlive)
        {
            // Karakter öldüyse burasý çalýþcak
            Debug.Log($"DeadState");
            return;
        }

        if (InputHandler.primaryAttackPressed)
        {
            if (currentState != basicAttackState) ChangeState(basicAttackState);
            Debug.Log($"basicAttackState");
        }

        if (InputHandler.skill1Pressed)
        {
            // Skill basýldýysa ("C") burasý çalýþcak
            if (currentState != skillState) ChangeState(skillState);
            Debug.Log($"SkillState");
        }

        else if (InputHandler.jumpPressed)
        {
            // Karakter Zýpladýysa burasý çalýþcak
            Debug.Log($"JumpState");
            if (currentState != jumpState) ChangeState(jumpState);
        }

        else if (InputHandler.moveInput.sqrMagnitude > 0.01f)
        {
            if (currentState != runState) ChangeState(runState);
            Debug.Log($"RunState");
        }

        else
        {
            if (currentState != idleState) ChangeState(idleState);
            Debug.Log($"IdleState");
        }

        currentState?.UpdateState();

        if (currentState == runState)
        {
            playerMovement?.SetMoveInput(InputHandler.moveInput);
        }
        else
        {
            playerMovement?.SetMoveInput(Vector2.zero);
        }

        InputHandler.ConsumeInputs();
    }

    public void ChangeState(IState newState)
    {
        if (newState == currentState) return;
        currentState?.ExitState();
        currentState = newState;
        currentState?.EnterState();
    }

    public void RequestSkill(int index)
    {
        pendingSkillIndex = index;
    }
}
