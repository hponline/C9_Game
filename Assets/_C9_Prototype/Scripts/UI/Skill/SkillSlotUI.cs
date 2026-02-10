using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlotUI : MonoBehaviour
{
    [SerializeField] string skillKeyInput;
    public TextMeshProUGUI skillKeyTxt;

    [SerializeField] Image cooldownFill;
    [SerializeField] Image icon;

    SkillSlot slot;

    private void Start()
    {
        skillKeyTxt.text = skillKeyInput;
    }

    private void Update()
    {
        if (slot == null) return;

        float cd = slot.CooldownRemaining;
        float max = slot.skillBehaviour.PlayerSkillSOData.cooldown;

        cooldownFill.fillAmount = cd > 0 ? cd / max : 0;
    }

    public void Bind(SkillSlot skillSlot)
    {
        slot = skillSlot;
        icon.sprite = slot.skillBehaviour.PlayerSkillSOData.skillIcon;
    }
}
