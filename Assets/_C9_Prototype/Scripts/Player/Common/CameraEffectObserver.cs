using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class CameraEffectObserver : MonoBehaviour
{
    Volume volume;

    ChromaticAberration chromaticAberration;
    LensDistortion lens;
    DepthOfField depthOfField;

    Sequence effectSequence;

    private void Awake()
    {
        volume = GetComponent<Volume>();

        volume.profile.TryGet(out chromaticAberration);
        volume.profile.TryGet(out lens);
        volume.profile.TryGet(out depthOfField);
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

        if (data.useChromatic)
        {
            effectSequence.Append(
                DOTween.To(
                    () => chromaticAberration.intensity.value,
                    x => chromaticAberration.intensity.value = x,
                    data.chromaticIntensity,
                    data.duration
                    ).SetEase(Ease.OutBack));
        }

        if (data.useLens)
        {
            effectSequence.Join(
                DOTween.To(
                    () => lens.intensity.value,
                    x => lens.intensity.value = x,
                    data.lensIntensity,
                    data.duration
                    ).SetEase(Ease.OutBack));
        }

        if (data.useDepthOfField)
        {
            effectSequence.Join(
                DOTween.To(
                    () => depthOfField.gaussianStart.value,
                    x => depthOfField.gaussianStart.value = x,
                    data.depthOfFieldStart,
                    data.duration
                    ).SetEase(Ease.OutBack));
        }

        if (data.useChromatic)
        {            
            effectSequence.Append(
            DOTween.To(
                () => chromaticAberration.intensity.value,
                x => chromaticAberration.intensity.value = x,
                0,
                data.duration
                ).SetEase(Ease.InOutSine));
        }

        if (data.useLens)
        {
            effectSequence.Join(
            DOTween.To(
                () => lens.intensity.value,
                x => lens.intensity.value = x,
                0,
                data.duration
                ).SetEase(Ease.InOutSine));
        }

        if (data.useDepthOfField)
        {
            effectSequence.Join(
                DOTween.To(
                    () => depthOfField.gaussianStart.value,
                    x => depthOfField.gaussianStart.value = x,
                    50f,
                    data.duration
                    ).SetEase(Ease.InOutSine));
        }
    }
}
