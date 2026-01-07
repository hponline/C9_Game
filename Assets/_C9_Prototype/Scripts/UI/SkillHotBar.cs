using UnityEngine;

public class SkillHotBar : MonoBehaviour
{
    [SerializeField] PlayerSkillController skillController;
    [SerializeField] SkillSlotUI[] skillSlots;

    private void Start()
    {
        var getSkillSlot = skillController.GetSkillSlots();
        for (int i = 0; i < skillSlots.Length; i++)
        {
            skillSlots[i].Bind(getSkillSlot[i]);
        }
    }
}
