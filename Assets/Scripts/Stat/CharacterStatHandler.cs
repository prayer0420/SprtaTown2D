using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsHandler : MonoBehaviour
{
    public int level = 1;
    public int currentHealth;
    public int currentMana;

    [SerializeField] private StatSO baseStats;

    public CharacterStat currentStat { get; private set; }
    public List<CharacterStat> statModifiers = new List<CharacterStat>();

    private void Awake()
    {
    }

    public void InitializeStats()
    {

        baseStats = GameManager.Instance.GetCharacterJob().baseStats;

        currentStat = new CharacterStat();

        UpdateBaseStats();

        ApplyStatModifiers();

        // ���� ü�°� ������ �ִ�ġ�� ����
        currentHealth = currentStat.maxHealth;
        currentMana = currentStat.maxMana;
    }

    private void Start()
    {
        InitializeStats();


    }
    private void UpdateBaseStats()
    {
        // ������ ���� ���� ����� ���⼭ ó���մϴ�.
        currentStat.maxHealth = baseStats.baseHealth + ((level-1) * 10);
        currentStat.maxMana = baseStats.baseMana + (level - 1) * 5;
        currentStat.attack = baseStats.baseAttack + (level - 1) * 2;
        currentStat.defense = baseStats.baseDefense + (level - 1) ;
        currentStat.Maxspeed = baseStats.baseSpeed;
    }

    private void ApplyStatModifiers()
    {
        // ������ �⺻������ �缳��
        UpdateBaseStats();

        // ������ ����
        foreach (var modifier in statModifiers)
        {
            currentStat.maxHealth += modifier.maxHealth;
            currentStat.maxMana += modifier.maxMana;
            currentStat.attack += modifier.attack;
            currentStat.defense += modifier.defense;
            // �ʿ��� �ٸ� ���� �����ڵ� ����
        }

        // ���� ü�°� ������ �ִ�ġ�� Ŭ����
        currentHealth = Mathf.Clamp(currentHealth, 0, currentStat.maxHealth);
        currentMana = Mathf.Clamp(currentMana, 0, currentStat.maxMana);
    }

    public void LevelUp()
    {
        level++;
        UpdateStats();
        Debug.Log("������! ���� ����: " + level);
    }

    private void UpdateStats()
    {
        UpdateBaseStats();
        ApplyStatModifiers();
    }

    public int GetMaxHealth()
    {
        return currentStat.maxHealth;
    }

    public int GetMaxMana()
    {
        return currentStat.maxMana;
    }

    public void ApplyEquipmentBonus(EquipmentItemSO item)
    {
        CharacterStat modifier = new CharacterStat();
        modifier.sourceItem = item;
        modifier.maxHealth = item.healthBonus;
        modifier.maxMana = item.manaBonus;
        modifier.attack = item.AttackBonus;
        modifier.defense = item.DefenseBonus;
        // �ʿ��� �ٸ� ���ȵ� �߰�
        statModifiers.Add(modifier);
        ApplyStatModifiers();
    }

    public void RemoveEquipmentBonus(EquipmentItemSO item)
    {
        CharacterStat modifierToRemove = null;
        foreach (var modifier in statModifiers)
        {
            if (modifier.sourceItem == item)
            {
                modifierToRemove = modifier;
                break;
            }
        }
        if (modifierToRemove != null)
        {
            statModifiers.Remove(modifierToRemove);
            ApplyStatModifiers();
        }
    }

    // ���� ü�°� ������ �����ϴ� �߰� �޼��� ����:
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, currentStat.maxHealth);
    }

    public void UseMana(int amount)
    {
        currentMana -= amount;
        currentMana = Mathf.Clamp(currentMana, 0, currentStat.maxMana);
    }
}
