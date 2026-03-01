using System;
using UnityEngine;

public interface IUpgradeManager
{
    event Action<UpgradeData, int> OnUpgradePurchased;

    int GetLevel(UpgradeData data);
    int GetCost(UpgradeData data);
    bool CanPurchase(UpgradeData data);
    bool TryPurchase(UpgradeData data);
}
