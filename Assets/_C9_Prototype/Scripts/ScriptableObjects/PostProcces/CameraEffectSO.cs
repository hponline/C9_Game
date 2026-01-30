using UnityEngine;

[CreateAssetMenu (menuName = "C9/Camera Effect")]
public class CameraEffectSO : ScriptableObject
{
    [Header("Chromatic Aberration")]
    public bool useChromatic;
    public float chromaticIntensity;

    [Header("Lens Distortion")]
    public bool useLens;
    public float lensIntensity;

    [Header("Depth Of Field")]
    public bool useDepthOfField;
    public float depthOfFieldStart;

    [Header("Common")]
    public float duration = 0.25f;
}
