using UnityEngine;

public enum ValueType
{
    AttackDamage,
    AttackSpeed,
    Health,
    CritChange
}

[CreateAssetMenu (menuName = "C9/CardData")]
public class CardDataSO : ScriptableObject
{
    public Sprite cardSprite;
    public string cardName;
    public float CardValue;
    public float minValue = 3;
    public float maxValue = 10;
    public ValueType valueType;
}
