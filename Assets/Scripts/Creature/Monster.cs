using UnityEngine;
public enum MonsterType
{
    Orc,
    Goblin,
    Zombie
    // �߰����� ���� Ÿ�Ե��� ���⿡�� �����մϴ�.
}
public enum AttackType
{
    Melee,
    Ranged
}

public class Monster : Creature
{
    [SerializeField] private MonsterSO monsterData;

    private ContactEnemyController contactController;
    private RangeEnemyController rangeController;
    protected AttackSO attackSO;

    protected override void Awake()
    {
        base.Awake();
        InitializeMonster();
    }

    private void InitializeMonster()
    {
        // MonsterSO�� �̿��� �⺻ ����
        SetCreatureName(monsterData.monsterName);
        statHandler.InitializeStats();
        attackSO = monsterData.attackSO;

        // ������ ��������Ʈ ����
        GetComponentInChildren<SpriteRenderer>().sprite = monsterData.monsterSprite;

        // ��Ʈ�ѷ� ����
        contactController = GetComponent<ContactEnemyController>();
        rangeController = GetComponent<RangeEnemyController>();

        //// ���� Ÿ�Կ� ���� ��Ʈ�ѷ��� Ȱ��ȭ �� ����
        //if (monsterData.attackType == AttackType.Melee && contactController != null)
        //{
        //    contactController.attackSO = attackSO;
        //    contactController.enabled = true;
        //}
        //else if (monsterData.attackType == AttackType.Ranged && rangeController != null)
        //{
        //    rangeController.attackSO = attackSO;
        //    rangeController.enabled = true;
        //}
    }

    protected override void Die()
    {
        base.Die();
        DropLoot();
        // �߰����� ��� ó�� ���� (��: ����ġ ���� ��)
        GameManager.Instance.Player.GainExperience(monsterData.experienceReward);
        Destroy(gameObject);
    }

    private void DropLoot()
    {
        if (monsterData.lootTable != null)
        {
            ItemSO droppedItem = monsterData.lootTable.GetDroppedItem();
            if (droppedItem != null)
            {
                Debug.Log($"{GetCreatureName()}��(��) {droppedItem.itemName}��(��) ����߽��ϴ�.");
            }
        }
    }
}
