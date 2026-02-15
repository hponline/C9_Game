using System.Collections.Generic;
using UnityEngine;

public class DamagePopupManager : MonoBehaviour
{
    [SerializeField] DamagePopup damagePopupPrefab;
    [SerializeField] RectTransform damagePopupContainer;
    [SerializeField] int poolSize = 50;
    [SerializeField] float critChance = 0.15f; // test

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

    void Spawn(float damage, Transform target)
    {
        if (pool.Count == 0) return;
        bool isCrit = Random.value < critChance;

        DamagePopup damagePopup = pool.Dequeue();
        damagePopup.gameObject.SetActive(true);
        damagePopup.Play(damage, target.position, cam, isCrit);

        // crit ctx içinden verilecek
        // 
    }

    public void ReturnToPool(DamagePopup damagePopup)
    {
        damagePopup.gameObject.SetActive(false);
        pool.Enqueue(damagePopup);
    }

    private void OnEnable()
    {
        DamageEvents.OnDamageDealt += Spawn;
    }
    private void OnDisable()
    {
        DamageEvents.OnDamageDealt -= Spawn;
    }
}
