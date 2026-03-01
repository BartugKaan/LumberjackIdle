using PrimeTween;
using UnityEngine;

public class LogCarrier : MonoBehaviour
{
    [SerializeField] private SpriteRenderer logRenderer;

    private void Awake()
    {
        if (logRenderer == null)
            logRenderer = transform.Find("UI/LogSprite")?.GetComponent<SpriteRenderer>();

        if (logRenderer != null)
            logRenderer.enabled = false;
    }

    public void ShowLog()
    {
        logRenderer.enabled = true;
        logRenderer.color = Color.white;
        logRenderer.transform.localPosition = Vector3.zero;
        Tween.PunchScale(logRenderer.transform, strength: new Vector3(0.3f, 0.3f, 0), duration: 0.3f);
    }

    public void DepositLog()
    {
        Tween.LocalPosition(logRenderer.transform, endValue: new Vector3(0, 0.5f, 0), duration: 0.3f);
        Tween.Alpha(logRenderer, endValue: 0f, duration: 0.3f)
            .OnComplete(target: this, target =>
            {
                target.logRenderer.enabled = false;
                target.logRenderer.color = Color.white;
                target.logRenderer.transform.localPosition = Vector3.zero;
            });
    }
}
