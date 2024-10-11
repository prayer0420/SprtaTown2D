using UnityEngine;

public enum StatsChangeType
{
    Add,      //0
    Multiple, //1
    Override  //2
}

//������ ����ó�� ����� �� �ְ� ������ִ� �Ӽ�
[System.Serializable]

public class CharacterStat
{
    public StatsChangeType statsChangeType; //������ �ٲٴ� Ÿ��
    public AttackSO attackSO;
    public int maxHealth;
    public int maxMana;
    public float Maxspeed;
    public int attack;
    public int defense;
    public EquipmentItemSO sourceItem; // �� �����ڸ� ������ �����ۿ� ���� ����
}