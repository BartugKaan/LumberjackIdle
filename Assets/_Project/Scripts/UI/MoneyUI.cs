using System;
using TMPro;
using UnityEngine;
using VContainer;

public class MoneyUI : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI moneyText;
   private IResourceManager _resourceManager;

   [Inject]
   public void Construct(IResourceManager resourceManager)
   {
      _resourceManager = resourceManager;

      _resourceManager.OnMoneyChanged += UpdateMoneyText;

      UpdateMoneyText(_resourceManager.CurrentMoney);
   }

   private void UpdateMoneyText(int currentMoney)
   {
      moneyText.text = $"${currentMoney}";
   }

   private void OnDestroy()
   {
      if (_resourceManager != null)
      {
         _resourceManager.OnMoneyChanged -= UpdateMoneyText;
      }
   }
}
