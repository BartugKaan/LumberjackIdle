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
            _controller.LogCarrier.DepositLog();

            int value = _controller.TargetTree.WoodValue;
            _controller.ResourceManager.AddMoney(value);
            _controller.MoneyUI.SpawnFloatingText(value, _controller.transform.position);

            Debug.Log($"State: Deposited! +{value} money");

            _controller.TargetTree = null;

            _controller.ChangeState(new IdleState(_controller));
        }

        public void Execute()
        { }

        public void Exit()
        { }
    }
}