using System.Linq;
using UnityEngine;

public class PlayerSkillController : MonoBehaviour
{
    [SerializeField] SkillBehaviour basicAttackSkill;
    [SerializeField] SkillBehaviour[] skillSlots;

    [SerializeField] float basicAttackCooldownTimer;
    [SerializeField] float[] slotCooldownTimer;

    private void Awake()
    {
        slotCooldownTimer = new float[skillSlots.Length];
    }

    private void Update()
    {
        basicAttackCooldownTimer -= Time.time;
        for (int i = 0; i < slotCooldownTimer.Length; i++)
        {
            slotCooldownTimer[i] -= Time.deltaTime;
        }
    }

    public void UseBasicAttack(IAttackSource source)
    {
        if (basicAttackSkill == null) return;
        if (basicAttackCooldownTimer > 0f) return;

        basicAttackSkill.Execute(source);
        basicAttackCooldownTimer = basicAttackSkill.Data.cooldown;

        Debug.Log("Basic atak yapýldý");
    }

    public void UseSkillSlot(int index, IAttackSource source)
    {
        if (index < 0 || index >= skillSlots.Length) return;
        var skill = skillSlots[index];
        if (skill == null) return;
        if (slotCooldownTimer[index] > 0f) return;

        skill.Execute(source);
        slotCooldownTimer[index] = skill.Data.cooldown;

        Debug.Log("Skill atýldý");
    }
}
