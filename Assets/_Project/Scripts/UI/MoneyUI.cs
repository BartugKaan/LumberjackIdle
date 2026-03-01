using System;
using TMPro;
using UnityEngine;
using VContainer;

public class MoneyUI : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI moneyText;
   [SerializeField] private FloatingText floatingTextPrefab;

   private IResourceManager _resourceManager;

   public RectTransform MoneyTextRect => moneyText.rectTransform;
   public Canvas Canvas => GetComponent<Canvas>();

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

   public void SpawnFloatingText(int amount, Vector3 worldPosition)
   {
      if (floatingTextPrefab == null) return;

      FloatingText ft = Instantiate(floatingTextPrefab, transform);
      ft.Initialize($"+${amount}", worldPosition, moneyText.rectTransform, Canvas);
   }

   private void OnDestroy()
   {
      if (_resourceManager != null)
      {
         _resourceManager.OnMoneyChanged -= UpdateMoneyText;
      }
   }
}
