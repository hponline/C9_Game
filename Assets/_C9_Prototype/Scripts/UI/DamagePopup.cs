using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] TMP_Text damageTxt;
    public Vector3 offset = Vector3.up * 2.4f;

    Camera cam;
    DamagePopupManager damagePopupManager;
    RectTransform rectTransform;

    public void Init(DamagePopupManager damagePopupManager)
    {
        this.damagePopupManager = damagePopupManager;
        rectTransform = GetComponent<RectTransform>();
    }

    public void Play(DamageContext ctx, Camera cam)
    {
        this.cam = cam;
        Vector3 screenPos = cam.WorldToScreenPoint(ctx.hitPoint + offset);
        rectTransform.position = screenPos;

        Vector2 randomOffset = new Vector2(
            Random.Range(-30f, 80),
            Random.Range(-40f, 40));

        float targetRotation = Random.Range(-30, 30);
        rectTransform.localRotation = Quaternion.Euler(0,0, targetRotation);

        Vector2 startPos = rectTransform.anchoredPosition;
        Vector2 targetPos = startPos + randomOffset;

        damageTxt.SetText("{0}", Mathf.RoundToInt(ctx.amount));
        damageTxt.alpha = 1f;
        transform.localScale = Vector3.one;
        damageTxt.color = ctx.isCrit ? new Color32(216,219,121, 255) : Color.white;
        float scaleValue = ctx.isCrit ? 1.25f : 0.7f;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(rectTransform.DOAnchorPos(targetPos, 0.6f).SetEase(Ease.OutBack));
        sequence.Join(damageTxt.DOFade(0f, 0.6f));
        sequence.Join(rectTransform.DORotate(Vector3.zero, 0.6f));
        sequence.Join(rectTransform.DOScale(scaleValue, 0.2f).SetLoops(2, LoopType.Yoyo));
        sequence.OnComplete(() => damagePopupManager.ReturnToPool(this));
    }
}
