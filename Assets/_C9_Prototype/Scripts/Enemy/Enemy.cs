using System;
using UnityEngine;

[RequireComponent(typeof(RunTimeStats))]
[RequireComponent(typeof(Health))]
public abstract class Enemy : MonoBehaviour, IAttackSource
{
    [SerializeField] EnemyConfigSO enemyConfigSO;
    [SerializeField] Transform attackOrigin;
    public Transform AttackOrigin => attackOrigin;

    protected RunTimeStats stats;
    protected Health health;

    protected virtual void Awake()
    {
        stats = GetComponent<RunTimeStats>();
        health = GetComponent<Health>();
        health.OnDied += HandleDeath;
    }

    protected abstract void HandleDeath(Health health);
}
