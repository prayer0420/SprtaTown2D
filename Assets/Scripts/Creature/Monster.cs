using UnityEngine;
public enum MonsterType
{
    Orc,
    Goblin,
    Zombie
    // 추가적인 몬스터 타입들은 여기에서 정의합니다.
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
        // MonsterSO를 이용한 기본 설정
        SetCreatureName(monsterData.monsterName);
        statHandler.InitializeStats();
        attackSO = monsterData.attackSO;

        // 몬스터의 스프라이트 설정
        GetComponentInChildren<SpriteRenderer>().sprite = monsterData.monsterSprite;

        // 컨트롤러 참조
        contactController = GetComponent<ContactEnemyController>();
        rangeController = GetComponent<RangeEnemyController>();

        //// 공격 타입에 따라 컨트롤러를 활성화 및 설정
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
        // 추가적인 사망 처리 로직 (예: 경험치 제공 등)
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
                Debug.Log($"{GetCreatureName()}이(가) {droppedItem.itemName}을(를) 드랍했습니다.");
            }
        }
    }
}
