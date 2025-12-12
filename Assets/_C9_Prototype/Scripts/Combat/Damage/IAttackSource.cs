using UnityEngine;

public interface IAttackSource
{
    Transform AttackOrigin { get; }
    GameObject Owner { get; }
}
