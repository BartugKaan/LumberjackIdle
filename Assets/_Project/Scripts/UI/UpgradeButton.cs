using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private UpgradeData upgradeData;

    [Header("UI References")] 
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private Button button;
    
    private IUpgradeManager _upgradeManager;
    private IResourceManager _resourceManager;

    public void Initialize(IUpgradeManager upgradeManager, IResourceManager resourceManager)
    {
        _upgradeManager = upgradeManager;
        _resourceManager = resourceManager;
        
        button.onClick.AddListener(OnClick);

        _resourceManager.OnMoneyChanged += HandleMoneyChanged;
        _upgradeManager.OnUpgradePurchased += HandleUpgradePurchased;

        UpdateUI();
    }

    private void OnClick()
    {
        if (_upgradeManager.TryPurchase(upgradeData))
            Tween.PunchScale(transform, strength: new Vector3(0.2f, 0.2f, 0), duration: 0.3f);
        else
            Tween.ShakeLocalPosition(transform, strength: new Vector3(5f, 0, 0), duration: 0.2f);
    }

    private void HandleMoneyChanged(int newMoney)
    {
        UpdateUI();
    }
    
    private void HandleUpgradePurchased(UpgradeData data, int newLevel)
    {
        UpdateUI();
    }
    
    private void UpdateUI()
    {
        int level = _upgradeManager.GetLevel(upgradeData);
        int cost = _upgradeManager.GetCost(upgradeData);

        nameText.text = $"{upgradeData.upgradeName} Lv.{level}";
        costText.text = $"${cost}";

        
        button.interactable = _upgradeManager.CanPurchase(upgradeData);
    }
    
    private void OnDestroy()
    {
        if (_resourceManager != null)
            _resourceManager.OnMoneyChanged -= HandleMoneyChanged;
        if (_upgradeManager != null)
            _upgradeManager.OnUpgradePurchased -= HandleUpgradePurchased;
    }
}
