using System;
using UnityEngine;
using UnityEngine.AI;
using VContainer;

[RequireComponent(typeof(NavMeshAgent))]
public class WorkerController : MonoBehaviour
{
    [SerializeField] private Transform basePoint;

    private IResourceManager _resourceManager;

    [Inject]
    public void Construct(IResourceManager resourceManager)
    {
        _resourceManager = resourceManager;
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
    }

    private void Start()
    {
        ChangeState(new IdleState(this));
    }

    private void Update()
    {
        _currentState?.Execute();
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
}
