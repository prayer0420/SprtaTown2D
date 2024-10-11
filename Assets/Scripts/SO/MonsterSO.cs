using UnityEngine;

[CreateAssetMenu(fileName = "NewMonster", menuName = "Monsters/Monster")]
public class MonsterSO : ScriptableObject
{
    public string monsterName;                  // 몬스터 이름
    public MonsterType monsterType;             // 몬스터 타입 (오크, 고블린 등)
    public AttackType attackType;               // 공격 타입 (근접 또는 원거리)
    public StatSO baseStats;                    // 몬스터의 기본 스탯 (체력, 공격력 등)
    public AttackSO attackSO;                   // 공격 정보를 담은 스크립터블 오브젝트
    public Sprite monsterSprite;                // 몬스터의 스프라이트 이미지
    public int experienceReward;                // 몬스터를 처치했을 때 주는 경험치
    public LootTableSO lootTable;               // 몬스터가 드랍하는 아이템 리스트
}
