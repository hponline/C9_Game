using System.Collections;
using UnityEngine;

public class HitStopManager : MonoBehaviour
{
    public static HitStopManager instance;
    public float duration = .5f;
    public float slowScale = 0.2f;

    private void Awake()
    {
        instance = this;
    }

    public void PlayHitStop()
    {
        StartCoroutine(HitStopCoroutine(duration, slowScale));
    }

    IEnumerator HitStopCoroutine(float duration, float slowScale)
    {
        float originScale = Time.timeScale;

        Time.timeScale = slowScale;
        Time.fixedDeltaTime = 0.02f * slowScale;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = originScale;
        Time.fixedDeltaTime = 0.02f;
    }
}
