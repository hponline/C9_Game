using UnityEngine;

public class PlayerVFX : MonoBehaviour
{
    [SerializeField] ParticleSystem slashVFX;

    public void AttackSlashEffect()
    {
        slashVFX.Play();
    }
}
