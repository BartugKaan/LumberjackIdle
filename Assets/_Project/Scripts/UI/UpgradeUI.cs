using UnityEngine;
using VContainer;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] private UpgradeButton[] upgradeButtons;

    private IUpgradeManager _upgradeManager;
    private IResourceManager _resourceManager;

    [Inject]
    public void Construct(IUpgradeManager upgradeManager, IResourceManager resourceManager)
    {
        _upgradeManager = upgradeManager;
        _resourceManager = resourceManager;

        foreach (UpgradeButton button in upgradeButtons)    
        {
            button.Initialize(_upgradeManager, _resourceManager);
        }
    }
}
