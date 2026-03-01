using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private MoneyUI moneyUI;
    [SerializeField] private WorkerController workerController;
    
    protected override void Configure(IContainerBuilder builder)
    {
        // Managers
        builder.Register<ResourceManager>(Lifetime.Singleton).AsImplementedInterfaces();
        
        //Scene Objects
        builder.RegisterComponent(moneyUI);
        builder.RegisterComponent(workerController);
        
        Debug.Log("GameLifetimeScope: All dependencies registered");
    }
}
