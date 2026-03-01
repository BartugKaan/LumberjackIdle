using UnityEngine;

namespace _Project.Scripts.Entities.States
{
    public class ChopingState : IState
    {
        
        private WorkerController _controller;
        private float _chopTimer;
        private float _shakeTimer;

        public ChopingState(WorkerController controller)
        {
            _controller = controller;
        }
        
        public void Enter()
        {
            _controller.Agent.isStopped = true;

            _chopTimer = _controller.TargetTree.ChopDuration;
            Debug.Log("State: Choping Tree");
        }

        public void Execute()
        {
            _chopTimer -= Time.deltaTime;
            _shakeTimer -= Time.deltaTime;

            if (_shakeTimer <= 0f)
            {
                _controller.TargetTree.ShakeTree();
                _shakeTimer = 0.5f;
            }

            if (_chopTimer <= 0f)
            {
                _controller.TargetTree.Chop();
                _controller.ChangeState(new MovingToBaseState(_controller));
            }
        }

        public void Exit()
        {
            _controller.Agent.isStopped = false;
        }
    }
}