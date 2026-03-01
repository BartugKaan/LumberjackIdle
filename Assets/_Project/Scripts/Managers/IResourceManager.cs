using UnityEngine;
using System;

public interface IResourceManager
{
  int CurrentMoney { get; }
  event Action<int> OnMoneyChanged; // Observer Pattern - Money changes will be observed by other classes (e.g., UI) through this event.

  void AddMoney(int amount);
  bool SpendMoney(int amount);
}
