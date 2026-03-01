using UnityEngine;

public interface IState
{
    void Enter(); // Works in first when Enter a state
    void Execute(); // Works every frame in the state
    void Exit(); // Works when exiting state
    
}
