using UnityEngine;

[CreateAssetMenu (menuName = "C9/Camera Effect")]
public class CameraEffectSO : ScriptableObject
{
    [Header("Chromatic Aberration")]
    public bool useChromatic;
    public float chromaticIntensity;
    public float chromaticDuration = 0.5f;

    [Header("Lens Distortion")]
    public bool useLens;
    public float lensIntensity;
    public float lensDuration = 0.5f;

    [Header("Depth Of Field")]
    public bool useDepthOfField;
    public float depthFocusDistance;
    public float depthDuration = 0.5f;

    [Header("MotionBlur")]
    public bool useMotionBlur;
    public float motionBlurIntensity;
    public float motionBlurClamp;
    public float motionBlurDuration = 0.5f;

    [Header("CameraShake")]
    public bool useCameraShake;
    public float cameraShakeAmplitude;
    public float cameraShakeFrequency;
    public float cameraShakeDuration = 0.5f;
}
