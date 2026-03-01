using _Project.Scripts.Entities.States;
using UnityEngine;

public class IdleState : IState
{
    
    private WorkerController _controller;
    
    public IdleState(WorkerController controller)
    {
        _controller = controller;
    }

    public void Enter()
    {
        _controller.Agent.isStopped = true;
        Debug.Log("State: Idle - Looking for a tree...");
    }

    public void Execute()
    {
        Tree tree = _controller.FindNearestTree();

        if (tree != null)
        {
            tree.Reserve();
            _controller.TargetTree = tree;
            
            _controller.ChangeState(new MovingToTreeState(_controller));
        }
    }

    public void Exit()
    { }
}
