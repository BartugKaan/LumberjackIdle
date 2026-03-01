using UnityEngine;

namespace _Project.Scripts.Entities.States
{
    public class MovingToBaseState: IState
    {
        private WorkerController _controller;
        
        public MovingToBaseState(WorkerController controller)
        {
            _controller = controller;
        }
        
        
        public void Enter()
        {
            _controller.Agent.isStopped = false;
            _controller.Agent.SetDestination(_controller.BasePoint.position);
            Debug.Log("State: Moving To Base");
        }

        public void Execute()
        {
            if (!_controller.Agent.pathPending && _controller.Agent.remainingDistance < 0.5f)
            {
                _controller.ChangeState(new DepositState(_controller));
            }
        }

        public void Exit()
        { }
    }
}