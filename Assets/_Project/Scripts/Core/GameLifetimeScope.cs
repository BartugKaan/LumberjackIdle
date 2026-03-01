using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private MoneyUI moneyUI;
    [SerializeField] private WorkerController workerController;
    [SerializeField] private UpgradeUI upgradeUI;
    [SerializeField] private WorkerSpawner workerSpawner;
    [SerializeField] private TreePool treePool;
     
    
    protected override void Configure(IContainerBuilder builder)
    {
        // Managers
        builder.Register<ResourceManager>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<UpgradeManager>(Lifetime.Singleton).AsImplementedInterfaces();
        
        //Scene Objects
        builder.RegisterComponent(moneyUI);
        builder.RegisterComponent(workerController);
        builder.RegisterComponent(upgradeUI);
        builder.RegisterComponent(workerSpawner);
        builder.RegisterComponent(treePool);
        
        Debug.Log("GameLifetimeScope: All dependencies registered");
    }
}
