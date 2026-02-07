using UnityEngine;

public abstract class SkillBehaviour : MonoBehaviour
{
    public abstract void Execute();
    public abstract void Stop();

    [SerializeField] protected SkillDataSO skillData;
    [SerializeField] protected CameraEffectSO cameraEffectSO;
    public SkillDataSO PlayerSkillSOData => skillData;

    protected void TriggerCameraEffect()
    {
        if (cameraEffectSO == null) return;

        CameraEffectData data = new CameraEffectData
        {
            useChromatic = cameraEffectSO.useChromatic,
            chromaticIntensity = cameraEffectSO.chromaticIntensity,

            useLens = cameraEffectSO.useLens,
            lensIntensity = cameraEffectSO.lensIntensity,

            useDepthOfField = cameraEffectSO.useDepthOfField,
            depthOfFieldStart = cameraEffectSO.depthOfFieldStart,            

            duration = cameraEffectSO.duration,            
        };
        SkillEffectEvents.OnSkillCameraEffect?.Invoke(data);
    }

    //public abstract void Execute(IAttackSource source);
    //Player/Enemy skill atarsa farklý skiller atan birimler eklenirse dmg kim tarafýndan atýlýyor bilmek için
}
