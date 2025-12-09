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

    IState idleState;
    IState runState;
    IState jumpState;
    IState skillState;

    IState currentState;

    private void Awake()
    {
        idleState = new IdleState(this);
        runState = new RunState(this);
        jumpState = new JumpState(this);
        skillState = new SkillState(this);

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

        if (InputHandler.skill1Pressed)
        {
            // Skill basýldýysa ("C") burasý çalýþcak
            if (currentState != skillState) ChangeState(skillState);
            Debug.Log($"SkillState");
            return;
        }

        if (InputHandler.jumpPressed)
        {
            // Karakter Zýpladýysa burasý çalýþcak
            if (currentState != jumpState) ChangeState(jumpState);
            Debug.Log($"JumpState");
            return;
        }

        if (InputHandler.moveInput.sqrMagnitude > 0.01f)
        {
            if (currentState != runState) ChangeState(runState);
            Debug.Log($"RunState");
            return;
        }

        if (currentState != idleState)
        {
            ChangeState(idleState);
            Debug.Log($"IdleState");
        }
    }

    public void ChangeState(IState newState)
    {
        if (newState == currentState) return;
        currentState?.ExitState();
        currentState = newState;
        currentState?.EnterState();
    }
}
