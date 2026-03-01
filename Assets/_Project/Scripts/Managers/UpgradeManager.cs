using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : IUpgradeManager
{

    private readonly IResourceManager _resourceManager;

    private Dictionary<UpgradeType, int> _levels = new();
    
    public event Action<UpgradeData, int> OnUpgradePurchased;

    public UpgradeManager(IResourceManager resourceManager)
    {
        _resourceManager = resourceManager;
        Debug.Log("UpgradeManager: Initialized");
    }
    
    public int GetLevel(UpgradeData data)
    {
        _levels.TryGetValue(data.upgradeType, out int level);
        return level;
    }

    public int GetCost(UpgradeData data)
    {
        int level = GetLevel(data);
        return Mathf.RoundToInt(data.baseCost * Mathf.Pow(data.costMultiplier, level));
    }

    public bool CanPurchase(UpgradeData data)
    {
        return GetLevel(data) < data.maxLevel && _resourceManager.CurrentMoney >= GetCost(data);
    }

    public bool TryPurchase(UpgradeData data)
    {
        if (GetLevel(data) >= data.maxLevel) return false;

        int cost = GetCost(data);
        if (!_resourceManager.SpendMoney(cost)) return false;

        int newLevel = GetLevel(data) + 1;
        _levels[data.upgradeType] = newLevel;
        
        Debug.Log($"Upgrade: {data.upgradeName} -> Level {newLevel} (Cost: {cost})");
        OnUpgradePurchased?.Invoke(data, newLevel);
        return true;
    }
}
