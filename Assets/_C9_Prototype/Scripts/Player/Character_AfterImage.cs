using System.Collections;
using UnityEngine;

public class Character_AfterImage : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform parentAfterImageHierarchy;
    [SerializeField] Transform playerAfterImage;
    [SerializeField] Material mat;
    SkinnedMeshRenderer[] skinnedMeshRenderers;    

    [Header("Variables")]
    [SerializeField] float activeTime = 2f;
    [SerializeField] float meshRefreshRate = 0.1f;
    [SerializeField] float meshDestroyDelay = 0.5f;
    [SerializeField] bool isTrailActive;

    Coroutine trailCoroutine;

    private void Awake()
    {
        if (skinnedMeshRenderers == null)
            skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
    }
    public void StartAfterImage()
    {
        if (isTrailActive) return;
        trailCoroutine = StartCoroutine(ActiveTrail(activeTime));
    }

    public void StopAfterImage()
    {
        if (!isTrailActive) return;
        isTrailActive = false;
        StopCoroutine(trailCoroutine);
        trailCoroutine = null;
    }

    void SpawnAfterImage()
    {
        for (int i = 0; i < skinnedMeshRenderers.Length; i++)
        {
            SkinnedMeshRenderer meshRenderer = skinnedMeshRenderers[i];
            if (!meshRenderer.enabled) continue;

            GameObject renderer = new GameObject();
            renderer.transform.SetPositionAndRotation(playerAfterImage.position, playerAfterImage.rotation);
            renderer.transform.SetParent(parentAfterImageHierarchy, true);

            MeshRenderer mr = renderer.AddComponent<MeshRenderer>();
            MeshFilter mf = renderer.AddComponent<MeshFilter>();

            Mesh mesh = new();
            meshRenderer.BakeMesh(mesh);

            mf.mesh = mesh;
            mr.material = mat;

            Destroy(renderer, meshDestroyDelay);
            Destroy(mesh, meshDestroyDelay);
        }
    }

    IEnumerator ActiveTrail(float timeActive)
    {
        isTrailActive = true;

        float timer = 0f;
        while (timeActive > 0f && isTrailActive)
        {
            timeActive -= Time.deltaTime;
            timer += Time.deltaTime;

            if (timer > meshRefreshRate)
            {
                timer = 0f;
                SpawnAfterImage();
            }
            yield return null;
        }
        isTrailActive = false;
    }
}
