using UnityEngine;

public class EnemyGetHitVFXAutoRelease : MonoBehaviour
{
    ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();    
    }

    private void Update()
    {
        if (ps != null && !ps.IsAlive())
            EnemyHitVFXController.instance.ReturnPool(ps);
    }
}
