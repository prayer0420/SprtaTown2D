using UnityEngine;


public class Enemy : Creature
{
    public int experienceReward;
    public LootTableSO lootTable;

    private void Start()
    {
        //TODO : �ִϸ�����
    }

    protected override void Die()
    {
        base.Die();

        // �÷��̾�� ����ġ ����
        GameManager.Instance.Player.GainExperience(experienceReward);

        // ������ ���
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
                Debug.Log($"{droppedItem.itemName}��(��) ����߽��ϴ�.");
            }
        }
    }
}
