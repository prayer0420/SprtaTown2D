using UnityEngine;

public enum StatsChangeType
{
    Add,      //0
    Multiple, //1
    Override  //2
}

//데이터 폴더처럼 사용할 수 있게 만들어주는 속성
[System.Serializable]

public class CharacterStat
{
    public StatsChangeType statsChangeType; //스탯을 바꾸는 타입
    public AttackSO attackSO;
    public int maxHealth;
    public int maxMana;
    public float Maxspeed;
    public int attack;
    public int defense;
    public EquipmentItemSO sourceItem; // 이 수정자를 제공한 아이템에 대한 참조
}