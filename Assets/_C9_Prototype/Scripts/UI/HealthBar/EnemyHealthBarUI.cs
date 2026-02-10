using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] Image fillImage;

    Transform target;
    CanvasGroup canvasGroup;
    RectTransform rect;
    Camera cam;

    public Vector3 offset = Vector3.up * 2.4f;
    public bool IsVisible => canvasGroup.alpha > 0f;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        cam = Camera.main;
    }

    private void LateUpdate()
    {
        if (!target) return;

        rect.position = cam.WorldToScreenPoint(target.position + offset);
    }

    public void Bind(Transform target)
    {
        this.target = target;
        gameObject.SetActive(true);
    }
    public void UnBind()
    {
        target = null;
        gameObject.SetActive(false);
    }

    public void Show()
    {
        canvasGroup.alpha = 1f;
    }

    public void Hide()
    {
        canvasGroup.alpha = 0f;
    }

    public void SetValue(float normalized)
    {
        fillImage.fillAmount = normalized;
    }
}
