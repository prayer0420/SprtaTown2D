//using UnityEngine;

//[System.Serializable]
//public class StatSystem
//{
//    public int level = 1;
//    public int currentHealth;
//    public int currentMana;

//    // �⺻ ����
//    private StatSO baseStats;
//    public int maxHealth;
//    public int maxMana;
//    public int Attack;
//    public int Defense;

//    // ��� �� ������ ���� �߰� ����
//    private int healthBonus = 0;
//    private int manaBonus = 0;
//    private int AttackBonus = 0;
//    private int DefenseBonus = 0;

//    // ���� �ʱ�ȭ
//    public void InitializeStats(StatSO statSO)
//    {
//        baseStats = statSO;

//        // StatSO�� ���� ������� ���� ����
//        maxHealth = baseStats.GetHealth(level);
//        maxMana = baseStats.GetMana(level);
//        Attack = baseStats.GetAttackPower(level);
//        Defense = baseStats.GetDefense(level);

//        currentHealth = maxHealth;
//        currentMana = maxMana;
//    }

//    // ���� ������Ʈ (��� �� ���ʽ� ����)
//    public void UpdateStats()
//    {
//        int totalHealth = maxHealth + healthBonus;
//        int totalMana = maxMana + manaBonus;
//        int totalAttack = Attack + AttackBonus;
//        int totalDefense = Defense + DefenseBonus;

//        currentHealth = Mathf.Clamp(currentHealth, 0, totalHealth);
//        currentMana = Mathf.Clamp(currentMana, 0, totalMana);
//    }

//    // �ִ� ü�� ��ȯ �޼���
//    public int GetMaxHealth()
//    {
//        return maxHealth + healthBonus;
//    }

//    // �ִ� ���� ��ȯ �޼���
//    public int GetMaxMana()
//    {
//        return maxMana + manaBonus;
//    }

//    // ������ �޼���
//    public void LevelUp()
//    {
//        level++;

//        // StatSO�� �Լ��� ������ �� ���� ����
//        maxHealth = baseStats.GetHealth(level);
//        maxMana = baseStats.GetMana(level);
//        Attack = baseStats.GetAttackPower(level);
//        Defense = baseStats.GetDefense(level);

//        UpdateStats();
//        Debug.Log("������! ���� ����: " + level);
//    }

//    // ��� ���ʽ� ���� �޼���
//    public void ApplyEquipmentBonus(EquipmentItemSO item)
//    {
//        healthBonus += item.healthBonus;
//        manaBonus += item.manaBonus;
//        AttackBonus += item.AttackBonus;
//        DefenseBonus += item.DefenseBonus;

//        UpdateStats();
//    }

//    // ��� ���ʽ� ���� �޼���
//    public void RemoveEquipmentBonus(EquipmentItemSO item)
//    {
//        healthBonus -= item.healthBonus;
//        manaBonus -= item.manaBonus;
//        AttackBonus -= item.AttackBonus;
//        DefenseBonus -= item.DefenseBonus;

//        UpdateStats();
//    }
//}
