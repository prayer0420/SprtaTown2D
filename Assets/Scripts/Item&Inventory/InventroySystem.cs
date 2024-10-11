using System.Collections.Generic;
using System;
using UnityEngine;
public enum EquipmentType
{
    Weapon,
}
public class InventorySystem
{
    public List<ItemSO> items = new List<ItemSO>();
    public Dictionary<EquipmentType, EquipmentItemSO> equippedItems = new Dictionary<EquipmentType, EquipmentItemSO>();

    // 이벤트 선언
    public event Action OnInventoryChanged;

    public void AddItem(ItemSO item)
    {
        items.Add(item);
        OnInventoryChanged?.Invoke();
        Debug.Log($"{item.itemName}을(를) 획득했습니다.");
    }

    public void RemoveItem(ItemSO item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            OnInventoryChanged?.Invoke();
            Debug.Log($"{item.itemName}을(를) 제거했습니다.");
        }
    }

    public void EquipItem(EquipmentItemSO item)
    {
        if (equippedItems.ContainsKey(item.equipmentType))
        {
            UnequipItem(item.equipmentType);
        }
        equippedItems[item.equipmentType] = item;
        GameManager.Instance.Player.statHandler.ApplyEquipmentBonus(item);
        OnInventoryChanged?.Invoke();
        Debug.Log($"{item.itemName}을(를) 장착했습니다.");
    }

    public void UnequipItem(EquipmentType type)
    {
        if (equippedItems.ContainsKey(type))
        {
            EquipmentItemSO item = equippedItems[type];
            equippedItems.Remove(type);
            GameManager.Instance.Player.statHandler.RemoveEquipmentBonus(item);
            OnInventoryChanged?.Invoke();
            Debug.Log($"{item.itemName}을(를) 해제했습니다.");
        }
    }

}
