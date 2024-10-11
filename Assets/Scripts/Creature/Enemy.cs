using UnityEngine;


public class Enemy : Creature
{
    public int experienceReward;
    public LootTableSO lootTable;

    private void Start()
    {
        //TODO : 애니메이터
    }

    protected override void Die()
    {
        base.Die();

        // 플레이어에게 경험치 전달
        GameManager.Instance.Player.GainExperience(experienceReward);

        // 아이템 드랍
        DropLoot();
        Destroy(gameObject);
    }

    private void DropLoot()
    {
        if (lootTable != null)
        {
            ItemSO droppedItem = lootTable.GetDroppedItem();
            if (droppedItem != null)
            {
                Debug.Log($"{droppedItem.itemName}을(를) 드랍했습니다.");
            }
        }
    }
}
