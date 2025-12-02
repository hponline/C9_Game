using UnityEngine;

public enum EnemyRace
{
    Orc,
    Elf,
    Human
}

[CreateAssetMenu(menuName = "C9/EnemyConfig")]
public class EnemyConfigSO : ScriptableObject
{
    public EnemyRace enemyRace;
    public CharacterStatSO characterStat;
    public GameObject prefab;
}
