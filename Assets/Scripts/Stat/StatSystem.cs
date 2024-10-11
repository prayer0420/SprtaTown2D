//using UnityEngine;

//[System.Serializable]
//public class StatSystem
//{
//    public int level = 1;
//    public int currentHealth;
//    public int currentMana;

//    // 기본 스탯
//    private StatSO baseStats;
//    public int maxHealth;
//    public int maxMana;
//    public int Attack;
//    public int Defense;

//    // 장비 및 버프에 의한 추가 스탯
//    private int healthBonus = 0;
//    private int manaBonus = 0;
//    private int AttackBonus = 0;
//    private int DefenseBonus = 0;

//    // 스탯 초기화
//    public void InitializeStats(StatSO statSO)
//    {
//        baseStats = statSO;

//        // StatSO의 값을 기반으로 스탯 설정
//        maxHealth = baseStats.GetHealth(level);
//        maxMana = baseStats.GetMana(level);
//        Attack = baseStats.GetAttackPower(level);
//        Defense = baseStats.GetDefense(level);

//        currentHealth = maxHealth;
//        currentMana = maxMana;
//    }

//    // 스탯 업데이트 (장비 및 보너스 적용)
//    public void UpdateStats()
//    {
//        int totalHealth = maxHealth + healthBonus;
//        int totalMana = maxMana + manaBonus;
//        int totalAttack = Attack + AttackBonus;
//        int totalDefense = Defense + DefenseBonus;

//        currentHealth = Mathf.Clamp(currentHealth, 0, totalHealth);
//        currentMana = Mathf.Clamp(currentMana, 0, totalMana);
//    }

//    // 최대 체력 반환 메서드
//    public int GetMaxHealth()
//    {
//        return maxHealth + healthBonus;
//    }

//    // 최대 마나 반환 메서드
//    public int GetMaxMana()
//    {
//        return maxMana + manaBonus;
//    }

//    // 레벨업 메서드
//    public void LevelUp()
//    {
//        level++;

//        // StatSO의 함수로 레벨업 시 스탯 증가
//        maxHealth = baseStats.GetHealth(level);
//        maxMana = baseStats.GetMana(level);
//        Attack = baseStats.GetAttackPower(level);
//        Defense = baseStats.GetDefense(level);

//        UpdateStats();
//        Debug.Log("레벨업! 현재 레벨: " + level);
//    }

//    // 장비 보너스 적용 메서드
//    public void ApplyEquipmentBonus(EquipmentItemSO item)
//    {
//        healthBonus += item.healthBonus;
//        manaBonus += item.manaBonus;
//        AttackBonus += item.AttackBonus;
//        DefenseBonus += item.DefenseBonus;

//        UpdateStats();
//    }

//    // 장비 보너스 제거 메서드
//    public void RemoveEquipmentBonus(EquipmentItemSO item)
//    {
//        healthBonus -= item.healthBonus;
//        manaBonus -= item.manaBonus;
//        AttackBonus -= item.AttackBonus;
//        DefenseBonus -= item.DefenseBonus;

//        UpdateStats();
//    }
//}
