using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using Unity.Cinemachine;

public class CameraEffectObserver : MonoBehaviour
{
    Volume volume;

    ChromaticAberration chromaticAberration;
    LensDistortion lens;
    DepthOfField depthOfField;
    MotionBlur motionBlur;

    [SerializeField] CinemachineBasicMultiChannelPerlin cameraShake;

    Sequence effectSequence;

    private void Awake()
    {
        volume = GetComponent<Volume>();

        volume.profile.TryGet(out chromaticAberration);
        volume.profile.TryGet(out lens);
        volume.profile.TryGet(out depthOfField);
        volume.profile.TryGet(out motionBlur);
    }

    private void OnEnable()
    {
        SkillEffectEvents.OnSkillCameraEffect += PlayEffect;
    }
    private void OnDisable()
    {
        SkillEffectEvents.OnSkillCameraEffect -= PlayEffect;
    }

    void PlayEffect(CameraEffectData data)
    {
        effectSequence?.Kill();

        effectSequence = DOTween.Sequence();
        Sequence rise = DOTween.Sequence();
        Sequence fall = DOTween.Sequence();

        UseChromatic(data, rise, fall);
        UseLens(data, rise, fall);
        UseDepthOfField(data, rise, fall);
        UseMotionBlur(data, rise, fall);
        UseCameraShake(data, rise, fall);

        effectSequence.Append(rise);
        effectSequence.Append(fall);
    }

    void UseChromatic(CameraEffectData data, Sequence rise, Sequence fall)
    {
        if (!data.useChromatic) return;

        float startChromatic = chromaticAberration.intensity.value;

        rise.Join(
                DOTween.To(
                () => chromaticAberration.intensity.value,
                x => chromaticAberration.intensity.value = x,
                data.chromaticIntensity,
                data.chromaticDuration
                ).SetEase(Ease.OutBack));
        fall.Join(
            DOTween.To(
            () => chromaticAberration.intensity.value,
            x => chromaticAberration.intensity.value = x,
            startChromatic,
            data.chromaticDuration
            ).SetEase(Ease.InOutSine));
    }

    void UseLens(CameraEffectData data, Sequence rise, Sequence fall)
    {
        if (!data.useLens) return;

        float startLens = lens.intensity.value;

        rise.Join(
            DOTween.To(
                () => lens.intensity.value,
                x => lens.intensity.value = x,
                data.lensIntensity,
                data.lensDuration
                ).SetEase(Ease.OutQuart));

        fall.Join(
            DOTween.To(
            () => lens.intensity.value,
            x => lens.intensity.value = x,
            startLens,
            data.lensDuration
            ).SetEase(Ease.OutQuart));
    }

    void UseDepthOfField(CameraEffectData data, Sequence rise, Sequence fall)
    {
        if (!data.useDepthOfField) return;

        float startDepth = depthOfField.focusDistance.value;

        rise.Join(DOTween.To(
                () => depthOfField.focusDistance.value,
                x => depthOfField.focusDistance.value = x,
                data.depthFocusDistance,
                data.depthDuration
                ).SetEase(Ease.OutBack));
        fall.Join(
            DOTween.To(
                () => depthOfField.focusDistance.value,
                x => depthOfField.focusDistance.value = x,
                startDepth,
                data.depthDuration
                ).SetEase(Ease.InOutSine));
    }

    void UseMotionBlur(CameraEffectData data, Sequence rise, Sequence fall)
    {
        if (!data.useMotionBlur) return;

        float startIntensity = motionBlur.intensity.value;
        float startClamp = motionBlur.clamp.value;

        rise.Join(
            DOTween.To(
                () => 0f,
                t =>
                {
                    motionBlur.intensity.value = Mathf.Lerp(startIntensity, data.motionBlurIntensity, t);
                    motionBlur.clamp.value = Mathf.Lerp(startClamp, data.motionBlurClamp, t);
                },
                1f,
                data.motionBlurDuration
            ).SetEase(Ease.InOutSine));

        fall.Join(
            DOTween.To(
                () => 0f,
                t =>
                {
                    motionBlur.intensity.value = Mathf.Lerp(data.motionBlurIntensity, startIntensity, t);
                    motionBlur.clamp.value = Mathf.Lerp(data.motionBlurClamp, startClamp, t);
                },
                1f,
                data.motionBlurDuration
            ).SetEase(Ease.InOutSine));
    }

    void UseCameraShake(CameraEffectData data, Sequence rise, Sequence fall)
    {
        float startAmplitudeGain = cameraShake.AmplitudeGain;
        float startFrequencyGain = cameraShake.FrequencyGain;

        rise.Join(
            DOTween.To(
                () => 0f,
                t =>
                {
                    cameraShake.AmplitudeGain = Mathf.Lerp(startAmplitudeGain, data.cameraShakeAmplitude, t);
                    cameraShake.FrequencyGain = Mathf.Lerp(startFrequencyGain, data.cameraShakeFrequency, t);
                },
                1f,
                data.cameraShakeDuration
            ).SetEase(Ease.InOutSine));

        fall.Join(
            DOTween.To(
                () => 0f,
                t =>
                {
                    cameraShake.AmplitudeGain = Mathf.Lerp(data.cameraShakeAmplitude, 0f, t);
                    cameraShake.FrequencyGain = Mathf.Lerp(data.cameraShakeFrequency, 0f, t);
                },
                1f,
                data.cameraShakeDuration
            ).SetEase(Ease.InOutSine));
    }
}
