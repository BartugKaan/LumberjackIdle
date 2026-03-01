using System;
using PrimeTween;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class Tree : MonoBehaviour
{
    

    [Header("Settings")]
    [SerializeField] private float chopDuration = 2f;   // Chop down seconds
    [SerializeField] private float regrowDelay = 7f;     // Regrowth time
    [SerializeField] private int woodValue = 10;         // Gaining money

    [Header("References")]
    [SerializeField] private Transform chopPoint;        // The point where the worker will stand (child object)
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Vector3 _originalScale;

    // ===== PUBLIC PROPERTIES =====

    public bool IsAvailable { get; private set; } = true;
    public Transform ChopPoint => chopPoint;
    public float ChopDuration => chopDuration;
    public int WoodValue => woodValue;
    public event Action<Tree> OnRegrown;

    private void Awake()
    {
        _originalScale = transform.localScale;
    }

    // ===== PUBLIC METHODS =====
    
    
    public void Reserve()
    {
        IsAvailable = false;
    }

    public void ShakeTree()
    {
        Tween.ShakeLocalPosition(transform, strength: new Vector3(0.05f, 0.02f, 0), duration: 0.3f);
    }

    // The worker calls this WHEN CHOPPING IS FINISHED.
    public void Chop()
    {
        Tween.Scale(transform, endValue: 0f, duration: 0.3f);
        Tween.Alpha(spriteRenderer, endValue: 0f, duration: 0.3f);

        RegrowAsync().Forget();
    }

    public void ResetTree()
    {
        IsAvailable = true;
        transform.localScale = _originalScale;
        spriteRenderer.color = Color.white;
    }

    // ===== PRIVATE METHODS =====
    private async UniTaskVoid RegrowAsync()
    {
        // Wait for the specified duration
        await UniTask.WaitForSeconds(regrowDelay, cancellationToken: destroyCancellationToken);

        // Time is up, the tree has regrown
        transform.localScale = _originalScale;
        spriteRenderer.color = Color.white;
        IsAvailable = true;
        
        OnRegrown?.Invoke(this);
    }
}
