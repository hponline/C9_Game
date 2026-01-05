using UnityEngine;

public abstract class PlayerSkill : MonoBehaviour
{
    public float damage;
    public float cooldown;

    public abstract void Execute();
}
