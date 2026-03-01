using PrimeTween;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;

    private void Awake()
    {
        if (text == null)
            text = GetComponent<TextMeshPro>();
    }

    public void Initialize(string message)
    {
        text.text = message;
        text.alpha = 1f;

        Tween.LocalPosition(transform, endValue: new Vector3(0, 1f, 0), duration: 0.8f, ease: Ease.OutQuad);
        Tween.Custom(target: text, startValue: 1f, endValue: 0f, duration: 0.8f, ease: Ease.InQuad,
                onValueChange: (t, value) => t.alpha = value)
            .OnComplete(target: gameObject, go => Destroy(go));
    }
}
