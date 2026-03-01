using PrimeTween;
using UnityEngine;

public class LogCarrier : MonoBehaviour
{
    [SerializeField] private SpriteRenderer logRenderer;

    private Vector3 _defaultLocalPos = new Vector3(0, 0.3f, 0);

    private void Awake()
    {
        if (logRenderer == null)
            logRenderer = transform.Find("UI/LogSprite")?.GetComponent<SpriteRenderer>();

        if (logRenderer != null)
            logRenderer.enabled = false;
    }

    public void ShowLog()
    {
        if (logRenderer == null) return;

        logRenderer.enabled = true;
        logRenderer.color = Color.white;
        logRenderer.transform.localPosition = _defaultLocalPos;
        logRenderer.transform.localScale = Vector3.one;
        Tween.PunchScale(logRenderer.transform, strength: new Vector3(0.3f, 0.3f, 0), duration: 0.3f);
    }

    public void DepositLog()
    {
        if (logRenderer == null || !logRenderer.enabled) return;

        Tween.LocalPosition(logRenderer.transform,
            endValue: _defaultLocalPos + new Vector3(0, 0.5f, 0), duration: 0.3f);
        Tween.Alpha(logRenderer, endValue: 0f, duration: 0.3f)
            .OnComplete(target: this, target =>
            {
                target.logRenderer.enabled = false;
                target.logRenderer.color = Color.white;
                target.logRenderer.transform.localPosition = target._defaultLocalPos;
            });
    }
}
