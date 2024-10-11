using UnityEngine;

[CreateAssetMenu(fileName = "NewConsumable", menuName = "Items/Consumable")]
public class ConsumableSO : ItemSO
{
    public int healthRecovery;
    public int manaRecovery;

    public void Consume(Creature target)
    {
        target.statHandler.currentHealth += healthRecovery;
        target.statHandler.currentMana += manaRecovery;
        Debug.Log($"{itemName}을(를) 사용했습니다.");
    }
}
