using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class CameraEffectObserver : MonoBehaviour
{
    Volume volume;

    ChromaticAberration chromaticAberration;
    LensDistortion lens;
    //DepthOfField depthOfField;

    Coroutine effectCoroutine;
    Sequence effectSequence;

    private void Awake()
    {
        volume = GetComponent<Volume>();

        volume.profile.TryGet(out chromaticAberration);
        volume.profile.TryGet(out lens);
        //volume.profile.TryGet(out depthOfField);
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
                    ).SetEase(Ease.InOutSine));
        }

        if (data.useLens)
        {
            effectSequence.Join(
                DOTween.To(
                    () => lens.intensity.value,
                    x => lens.intensity.value = x,
                    data.lensIntensity,
                    data.duration
                    ).SetEase(Ease.InOutSine));
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
    }

    #region Yeni Skill için beklet Doðru çalýþýyorsa sil

    //void ApplyEffect(CameraEffectData data)
    //{
    //    if (effectCoroutine != null)
    //        StopCoroutine(effectCoroutine);

    //    effectCoroutine = StartCoroutine(EffectCoroutine(data));

    //    /*
    //    if (data.useChromatic)
    //    {
    //        //chromaticAberration.intensity.value = data.chromaticIntensity;
    //        //chromaticAberration.intensity.value = Mathf.Lerp(chromaticAberration.intensity.value, data.chromaticIntensity, Time.deltaTime * 2);
    //        //Debug.Log($"chromatic {chromaticAberration.intensity.value}");

    //        StartCoroutine(SmoothChromatic(chromaticAberration.intensity.value, data.chromaticIntensity, data.duration));
    //    }

    //    if (data.useLens)
    //    {
    //        //lens.intensity.value = data.lensIntensity;
    //        //lens.intensity.value = Mathf.Lerp(data.lensIntensity, lens.intensity.value, 1);
    //        StartCoroutine(SmoothLens(lens.intensity.value, data.lensIntensity, data.duration));
    //    }

    //    if (data.useDepthOfField)
    //    {
    //        //depthOfField.gaussianStart.value = data.depthOfFieldStart;
    //        //depthOfField.gaussianStart.value = Mathf.Lerp(data.depthOfFieldStart, depthOfField.gaussianStart.value, 1);
    //        StartCoroutine(SmoothDepth(depthOfField.gaussianStart.value, data.depthOfFieldStart, data.duration));
    //    }

    //    //StopAllCoroutines();
    //    StartCoroutine(ResetAfter(1));
    //    //StartCoroutine(ResetAfter(data.duration)); // diðer corotinler çalýþtýgý için burada tekrar resetliyo ve hiç baþlamýyor
    //    */
    //}


    //IEnumerator EffectCoroutine(CameraEffectData data)
    //{
    //    float time = 0f;

    //    float _chromatic = chromaticAberration.intensity.value;
    //    float _lensDistortion = lens.intensity.value;
    //    float _dof = depthOfField.gaussianStart.value;

    //    while (time < data.duration)
    //    {
    //        time += Time.deltaTime;
    //        float t = time / data.duration;

    //        if (data.useChromatic)
    //            chromaticAberration.intensity.value = Mathf.Lerp(_chromatic, data.chromaticIntensity, t);

    //        if (data.useLens)
    //            lens.intensity.value = Mathf.Lerp(_lensDistortion, data.lensIntensity, t);

    //        if (data.useDepthOfField)
    //            depthOfField.gaussianStart.value = Mathf.Lerp(_dof, data.depthOfFieldStart, t);

    //        yield return null;
    //    }

    //    chromaticAberration.intensity.value = Mathf.Lerp(_chromatic, data.chromaticIntensity, data.duration * Time.deltaTime);
    //    lens.intensity.value = Mathf.Lerp(_lensDistortion, data.lensIntensity, data.duration * Time.deltaTime);
    //    depthOfField.gaussianStart.value = 0f;
    //}
    //IEnumerator ResetAfter(float time)
    //{
    //    yield return new WaitForSeconds(time);
    //    chromaticAberration.intensity.value = 0f;
    //    lens.intensity.value = 0;
    //    depthOfField.gaussianStart.value = 0f;
    //}

    //IEnumerator SmoothChromatic(float from, float to, float duration) // diðer efektler içinde yap
    //{
    //    float time = 0;
    //    while (time < duration)
    //    {
    //        time += Time.deltaTime;
    //        float t = time / duration;

    //        chromaticAberration.intensity.value = Mathf.SmoothStep(from, to, t);
    //        yield return null;
    //    }
    //    chromaticAberration.intensity.value = to;
    //}

    //IEnumerator SmoothLens(float from, float to, float duration) // diðer efektler içinde yap
    //{
    //    float time = 0;
    //    while (time < duration)
    //    {
    //        time += Time.deltaTime;
    //        float t = time / duration;

    //        lens.intensity.value = Mathf.SmoothStep(from, to, t);
    //        yield return null;
    //    }
    //    lens.intensity.value = to;
    //}

    //IEnumerator SmoothDepth(float from, float to, float duration) // diðer efektler içinde yap
    //{
    //    float time = 0;
    //    while (time < duration)
    //    {
    //        time += Time.deltaTime;
    //        float t = time / duration;

    //        depthOfField.gaussianStart.value = Mathf.SmoothStep(from, to, t);
    //        yield return null;
    //    }
    //    depthOfField.gaussianStart.value = to;
    //}
    //
    #endregion
}
