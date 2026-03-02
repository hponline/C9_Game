using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    MoveSpeed,
    AttackDamage,
    AttackSpeed,
    Health,
    CritChange
}

public class BuffController : MonoBehaviour
{
    public static BuffController Instance;

    public event Action OnBuffAdded;
    public event Action OnBuffRemoved;

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

        OnBuffAdded?.Invoke();
    }

    IEnumerator BuffRoutine(StatType statType, float duration)
    {
        yield return new WaitForSeconds(duration);
        activeBuffs.Remove(statType);
        activeCoroutines.Remove(statType);

        OnBuffRemoved?.Invoke();
    }

    public float GetMultiplier(StatType statType)
    {
        return activeBuffs.TryGetValue(statType, out var val) ? val : 1f;
    }
}
