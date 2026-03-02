using System;
using UnityEngine;

public class StatValue
{
    float _value;
    public event Action<float> OnValueChanged;

    public float Value
    {
        get => _value;
        set
        {
            if (_value == value) return;
            _value = value;
            OnValueChanged?.Invoke(value);
        }
    }
}
