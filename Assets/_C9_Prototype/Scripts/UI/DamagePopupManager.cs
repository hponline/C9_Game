using System.Collections.Generic;
using UnityEngine;

public class DamagePopupManager : MonoBehaviour
{
    [SerializeField] DamagePopup damagePopupPrefab;
    [SerializeField] RectTransform damagePopupContainer;
    [SerializeField] int poolSize = 50;

    Queue<DamagePopup> pool = new();
    Camera cam;

    private void Awake()
    {
        cam = Camera.main;
        CreatePool();
    }

    void CreatePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            DamagePopup damagePopup = Instantiate(damagePopupPrefab, damagePopupContainer);
            damagePopup.gameObject.SetActive(false);
            damagePopup.Init(this);
            pool.Enqueue(damagePopup);
        }
    }

    void Spawn(DamageContext ctx)
    {
        if (pool.Count == 0) return;

        DamagePopup damagePopup = pool.Dequeue();
        damagePopup.gameObject.SetActive(true);
        damagePopup.Play(ctx, cam);
    }

    public void ReturnToPool(DamagePopup damagePopup)
    {
        damagePopup.gameObject.SetActive(false);
        pool.Enqueue(damagePopup);
    }

    private void OnEnable()
    {
        DamageEvents.OnDamagePopup += Spawn;
    }
    private void OnDisable()
    {
        DamageEvents.OnDamagePopup -= Spawn;
    }
}
