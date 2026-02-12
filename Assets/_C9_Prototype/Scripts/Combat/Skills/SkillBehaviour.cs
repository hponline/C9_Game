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
            chromaticDuration = cameraEffectSO.chromaticDuration,

            useLens = cameraEffectSO.useLens,
            lensIntensity = cameraEffectSO.lensIntensity,
            lensDuration = cameraEffectSO.lensDuration,

            useDepthOfField = cameraEffectSO.useDepthOfField,
            depthFocusDistance = cameraEffectSO.depthFocusDistance, 
            depthDuration = cameraEffectSO.depthDuration,
            
            useMotionBlur = cameraEffectSO.useMotionBlur,
            motionBlurIntensity = cameraEffectSO.motionBlurIntensity,
            motionBlurClamp = cameraEffectSO.motionBlurClamp,
            motionBlurDuration = cameraEffectSO.motionBlurDuration,

            useCameraShake = cameraEffectSO.useCameraShake,
            cameraShakeAmplitude = cameraEffectSO.cameraShakeAmplitude,
            cameraShakeFrequency = cameraEffectSO.cameraShakeFrequency,
            cameraShakeDuration = cameraEffectSO.cameraShakeDuration,
        };
        SkillEffectEvents.OnSkillCameraEffect?.Invoke(data);
    }

    //public abstract void Execute(IAttackSource source);
    //Player/Enemy skill atarsa farklý skiller atan birimler eklenirse dmg kim tarafýndan atýlýyor bilmek için
}
