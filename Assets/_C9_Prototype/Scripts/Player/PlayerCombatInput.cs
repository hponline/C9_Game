using UnityEngine;

public class PlayerCombatInput : MonoBehaviour
{
    [SerializeField] PlayerCombat playerCombat;
    [SerializeField] InputHandler inputHandler;

    private void Update()
    {
        if (inputHandler.primaryAttackPressed)
        {
            playerCombat.DoAttack();
            inputHandler.ConsumeInputs();
        }
    }
}
