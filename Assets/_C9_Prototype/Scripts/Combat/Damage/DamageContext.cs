using UnityEngine;

public class DamageContext
{
    public float amount { get; }
    public bool isCrit { get; }
    public Vector3 hitPoint { get; }
    public Vector3 hitNormal { get; }

    public DamageContext(float amount, bool isCrit, Vector3 hitPoint, Vector3 hitNormal)
    {
        this.amount = amount;
        this.isCrit = isCrit;
        this.hitPoint = hitPoint;
        this.hitNormal = hitNormal;
    }
}
