using UnityEngine;

namespace _Project.Scripts.Entities.States
{
    public class MovingToTreeState : IState
    {
        
        private WorkerController _controller;

        public MovingToTreeState(WorkerController controller)
        {
            _controller = controller;
        }
        
        public void Enter()
        {
            _controller.Agent.isStopped = false;
            _controller.Agent.SetDestination(_controller.TargetTree.ChopPoint.position);
            _controller.StartWobble();
            Debug.Log("State: Moving To Tree");
        }

        public void Execute()
        {
            if (!_controller.Agent.pathPending && _controller.Agent.remainingDistance < 0.5f)
            {
                _controller.ChangeState(new ChopingState(_controller));
            }
        }

        public void Exit()
        {
            _controller.StopWobble();
        }
    }
}