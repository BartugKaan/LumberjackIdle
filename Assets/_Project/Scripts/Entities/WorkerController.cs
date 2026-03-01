using System;
using PrimeTween;
using UnityEngine;
using UnityEngine.AI;
using VContainer;

[RequireComponent(typeof(NavMeshAgent))]
public class WorkerController : MonoBehaviour
{
    [SerializeField] private Transform basePoint;
    [SerializeField] private Transform spriteTransform;

    private Tween _wobbleTween;
    private IResourceManager _resourceManager;
    private IUpgradeManager _upgradeManager;

    [Inject]
    public void Construct(IResourceManager resourceManager, IUpgradeManager upgradeManager)
    {
        _resourceManager = resourceManager;
        _upgradeManager = upgradeManager;
        
        _upgradeManager.OnUpgradePurchased += HandleUpgrade;
    }

    private IState _currentState;
    
    public NavMeshAgent Agent { get; private set; }
    public IResourceManager ResourceManager => _resourceManager;
    public Transform BasePoint => basePoint;
    
    public Tree TargetTree { get; set; }

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;

        if (spriteTransform == null)
            spriteTransform = transform.Find("UI/LumberJackUI");
    }

    private void Start()
    {
        ChangeState(new IdleState(this));
    }

    private void Update()
    {
        _currentState?.Execute();
    }
    
    private void HandleUpgrade(UpgradeData data, int newLevel)
    {
        if (data.upgradeType == UpgradeType.WorkerSpeed)
        {
            Agent.speed += data.valuePerLevel;
            Debug.Log($"Worker Speed: {Agent.speed}");
        }
    }

    private void OnDestroy()
    {
        if (_upgradeManager != null)
        {
            _upgradeManager.OnUpgradePurchased -= HandleUpgrade;
        }
    }

    public void ChangeState(IState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();
    }

    public Tree FindNearestTree()
    {
        Tree[] trees = FindObjectsByType<Tree>(FindObjectsSortMode.None);
        Tree nearestTree = null;
        float nearestDistance = float.MaxValue;

        foreach (var tree in trees)
        {
            if(!tree.IsAvailable) continue;

            float distance = Vector3.Distance(transform.position, tree.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestTree = tree;

            }
        }
        return nearestTree;
    }

    public void StartWobble()
    {
        _wobbleTween = Tween.LocalRotation(spriteTransform,
            startValue: new Vector3(0, 0, -5f),
            endValue: new Vector3(0, 0, 5f),
            duration: 0.25f,
            cycles: -1, cycleMode: CycleMode.Yoyo);
    }

    public void StopWobble()
    {
        _wobbleTween.Stop();
        spriteTransform.localRotation = Quaternion.identity;
    }

    public void SetBasePoint(Transform point)
    {
        basePoint = point;
    }

    
}
