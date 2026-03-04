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
    public float CardValue; // playerRuntimestats içinden % yüzdelik deðer çekilip variance aralýgýnda uygulanacak
    public ValueType valueType;
}
