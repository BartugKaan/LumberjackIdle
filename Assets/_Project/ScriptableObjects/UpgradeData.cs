using UnityEngine;

public enum UpgradeType
{
    WorkerSpeed,
    HireWorker,
    TreeCount
}


[CreateAssetMenu(fileName = "NewUpgrade", menuName = "LumberjackIdle/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    [Header("Info")] 
    public string upgradeName;
    [TextArea] public string description;
    public UpgradeType upgradeType;

    [Header("Cost")] 
    public int baseCost = 50;
    public float costMultiplier = 1.5f;

    [Header("Limits")] 
    public int maxLevel = 10;

    [Header("Effect")] 
    public float valuePerLevel = 0.5f;
}
