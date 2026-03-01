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

    // ===== PUBLIC PROPERTIES =====

    public bool IsAvailable { get; private set; } = true;
    public Transform ChopPoint => chopPoint;
    public float ChopDuration => chopDuration;
    public int WoodValue => woodValue;

    // ===== PUBLIC METHODS =====
    
    public void Reserve()
    {
        IsAvailable = false;
    }

    // The worker calls this WHEN CHOPPING IS FINISHED.
    public void Chop()
    {
        spriteRenderer.color = new Color(1f, 1f, 1f, 0.3f);
        
        RegrowAsync().Forget();
    }

    // ===== PRIVATE METHODS =====
    private async UniTaskVoid RegrowAsync()
    {
        // Wait for the specified duration
        await UniTask.WaitForSeconds(regrowDelay, cancellationToken: destroyCancellationToken);

        // Time is up, the tree has regrown
        spriteRenderer.color = Color.white;
        IsAvailable = true;
    }
}
