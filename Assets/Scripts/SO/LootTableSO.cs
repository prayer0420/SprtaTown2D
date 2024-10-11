using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLootTable", menuName = "Loot/LootTable")]
public class LootTableSO : ScriptableObject
{
    public List<LootItem> lootItems;

    public ItemSO GetDroppedItem()
    {
        int totalWeight = 0;
        foreach (var loot in lootItems)
        {
            totalWeight += loot.weight;
        }

        int randomValue = UnityEngine.Random.Range(0, totalWeight);
        int accumulatedWeight = 0;

        foreach (var loot in lootItems)
        {
            accumulatedWeight += loot.weight;
            if (randomValue < accumulatedWeight)
            {
                return loot.item;
            }
        }

        return null;
    }
}

[System.Serializable]
public class LootItem
{
    public ItemSO item;
    public int weight; // 드랍 확률을 위한 가중치
}
