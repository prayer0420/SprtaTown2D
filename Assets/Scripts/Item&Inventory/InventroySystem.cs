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

    // �̺�Ʈ ����
    public event Action OnInventoryChanged;

    public void AddItem(ItemSO item)
    {
        items.Add(item);
        OnInventoryChanged?.Invoke();
        Debug.Log($"{item.itemName}��(��) ȹ���߽��ϴ�.");
    }

    public void RemoveItem(ItemSO item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            OnInventoryChanged?.Invoke();
            Debug.Log($"{item.itemName}��(��) �����߽��ϴ�.");
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
        Debug.Log($"{item.itemName}��(��) �����߽��ϴ�.");
    }

    public void UnequipItem(EquipmentType type)
    {
        if (equippedItems.ContainsKey(type))
        {
            EquipmentItemSO item = equippedItems[type];
            equippedItems.Remove(type);
            GameManager.Instance.Player.statHandler.RemoveEquipmentBonus(item);
            OnInventoryChanged?.Invoke();
            Debug.Log($"{item.itemName}��(��) �����߽��ϴ�.");
        }
    }

}
