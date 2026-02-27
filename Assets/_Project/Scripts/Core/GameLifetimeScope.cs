using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        // Will be registered Managers and Services that will be used in the game.
        Debug.Log("GameLifetimeScope Configure, VContainer will register Managers and Services for the game.");
    }
}
