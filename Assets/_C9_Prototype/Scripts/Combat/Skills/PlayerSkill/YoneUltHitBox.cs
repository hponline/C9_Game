using System.Collections.Generic;
using UnityEngine;

public struct HitInfo
{
    public IDamageable damageable;
    public Vector3 hitPoint;
    public Vector3 hitNormal;
}

public class YoneUltHitBox : MonoBehaviour
{
    public List<HitInfo> hitTargets = new();

    public void ResetHits()
    {
        hitTargets.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<IDamageable>(out var dmg)) return;
        foreach (var hit in hitTargets)
            if (hit.damageable == dmg) return;

        Vector3 closestPoint = other.transform.position;
        Vector3 hitNormal = (closestPoint - transform.position).normalized;

        hitTargets.Add(new HitInfo
        {
            damageable = dmg,
            hitPoint = closestPoint,
            hitNormal = hitNormal
        });
    }
}
