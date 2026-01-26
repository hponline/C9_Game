using UnityEngine;

public class PlayerVFX : MonoBehaviour
{
    [SerializeField] ParticleSystem slashVFX;
    [SerializeField] TrailRenderer trailRenderer;

    public void AttackSlashEffect()
    {
        slashVFX.Play();
    }

    public void StartTrail()
    {
        trailRenderer.Clear();
        trailRenderer.emitting = true;
    }
    public void EndTrail()
    {
        trailRenderer.emitting = false;
    }
}
