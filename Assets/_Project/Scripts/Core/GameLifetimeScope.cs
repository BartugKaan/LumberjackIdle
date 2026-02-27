using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private MoneyUI moneyUI;
    
    protected override void Configure(IContainerBuilder builder)
    {
        
        builder.Register<ResourceManager>(Lifetime.Singleton).AsImplementedInterfaces();

        builder.RegisterComponent(moneyUI);
        
        Debug.Log("GameLifetimeScope: All dependencies registered");
    }
}
