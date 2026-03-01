using UnityEngine;

namespace _Project.Scripts.Entities.States
{
    public class DepositState: IState
    {
        private WorkerController _controller;
        
        public DepositState(WorkerController controller)
        {
            _controller = controller;
        }
        
        public void Enter()
        {
            _controller.ResourceManager.AddMoney(_controller.TargetTree.WoodValue);
            Debug.Log($"State: Deposited! +{_controller.TargetTree.WoodValue} money");

            _controller.TargetTree = null;
            
            _controller.ChangeState(new IdleState(_controller));
        }

        public void Execute()
        { }

        public void Exit()
        { }
    }
}