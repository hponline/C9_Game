using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    MoveSpeed,
    AttackDamage,
    AttackSpeed,
    Health
}

public class BuffController : MonoBehaviour
{
    public static BuffController Instance;

    Dictionary<StatType, float> activeBuffs = new();
    Dictionary<StatType, Coroutine> activeCoroutines = new();


    private void Awake()
    {
        Instance = this;
    }

    public void ApplyBuffs(StatType statType, float multiplier, float duration)
    {
        if (activeCoroutines.ContainsKey(statType))
        {
            StopCoroutine(activeCoroutines[statType]);
            activeBuffs[statType] = multiplier;
        }
        else        
            activeBuffs.Add(statType, multiplier);

        activeCoroutines[statType] = StartCoroutine(BuffRoutine(statType, duration));
    }

    IEnumerator BuffRoutine(StatType statType, float duration)
    {
        yield return new WaitForSeconds(duration);
        activeBuffs.Remove(statType);
        activeCoroutines.Remove(statType);
    }

    public float GetMultiplier(StatType statType)
    {
        return activeBuffs.TryGetValue(statType, out var val) ? val : 1f;
    }
}
