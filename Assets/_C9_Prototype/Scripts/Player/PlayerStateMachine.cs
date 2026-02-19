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
    public PlayerHealth health;

    [Header("Skill State Setting")]
    public IState idleState;
    public IState runState;
    IState jumpState; // zýplama opsiyonel
    public IState basicAttackState;
    public IState deadState;

    public bool attackFinished;

    public bool CanUseAbilities => health.IsAlive && currentState != deadState;

    IState currentState;

    private void Awake()
    {
        // monobehavior olmadýgý için + null olmamasý için biz newliyoruz
        idleState = new IdleState(this);
        runState = new RunState(this);
        jumpState = new JumpState(this);
        basicAttackState = new BasicAttackState(this);
        deadState = new DeadState(this);

        InputHandler = GetComponent<InputHandler>();
        animator = GetComponentInChildren<Animator>();
        health = GetComponent<PlayerHealth>();
        playerSkillController = GetComponent<PlayerSkillController>();
    }

    private void Start()
    {
        ChangeState(idleState);
    }

    private void Update()
    {
        if (!health.IsAlive && currentState != deadState)
        {
            ChangeState(deadState);
            return;
        }

        currentState?.UpdateState();
        InputHandler.ConsumeInputs();
    }

    public void ChangeState(IState newState)
    {
        if (!health.IsAlive && newState != deadState) return;
        if (newState == currentState) return;

        currentState?.ExitState();
        currentState = newState;
        currentState?.EnterState();
    }
}
