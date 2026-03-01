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

            if (_controller.FloatingTextPrefab != null)
            {
                var ft = Object.Instantiate(_controller.FloatingTextPrefab,
                    _controller.transform.position, Quaternion.identity);
                ft.Initialize($"+${value}");
            }

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