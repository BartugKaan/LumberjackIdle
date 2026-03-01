using PrimeTween;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private void Awake()
    {
        if (text == null)
            text = GetComponent<TextMeshProUGUI>();
    }

    public void Initialize(string message, Vector3 worldPosition, RectTransform targetUI, Canvas canvas)
    {
        text.text = message;
        text.alpha = 1f;

        RectTransform rt = GetComponent<RectTransform>();

        // Convert world position to canvas local position
        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPosition);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform, screenPos, canvas.worldCamera, out Vector2 startPos);
        rt.anchoredPosition = startPos;

        // Target position (money text)
        Vector2 endPos = targetUI.anchoredPosition;

        // Fly to money UI
        Tween.UIAnchoredPosition(rt, endValue: endPos, duration: 0.7f, ease: Ease.InQuad);

        // Scale down as it approaches
        Tween.Scale(rt, startValue: 1f, endValue: 0.5f, duration: 0.7f, ease: Ease.InQuad);

        // Fade out in last 30%
        Tween.Custom(target: text, startValue: 1f, endValue: 0f, duration: 0.3f,
                ease: Ease.InQuad, startDelay: 0.4f,
                onValueChange: (t, value) => t.alpha = value)
            .OnComplete(target: gameObject, go => Destroy(go));
    }
}
