using System;
using UnityEngine;

public class ResourceManager : IResourceManager
{
    public int CurrentMoney { get; private set; }
    public event Action<int> OnMoneyChanged;


    public ResourceManager()
    {
        CurrentMoney = 0;
        Debug.Log($"ResourceManager started. Current Money: {CurrentMoney}");
    }
    
    public void AddMoney(int amount)
    {
        CurrentMoney += amount;
        OnMoneyChanged?.Invoke(CurrentMoney);
    }

    public bool SpendMoney(int amount)
    {
        if (CurrentMoney >= amount)
        {
            CurrentMoney -= amount;
            OnMoneyChanged?.Invoke(CurrentMoney);
            return true;
        }
        Debug.Log("Invalid money");
        return false;
    }
}
