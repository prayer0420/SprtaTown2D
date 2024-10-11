using System;
using UnityEngine;

[System.Serializable]
public class ExperienceSystem
{
    public enum EquipmentType
    {
        Weapon,
        Armor,
        Accessory
    }


    public int currentLevel = 1;
    public int currentExperience = 0;
    public int experienceToNextLevel = 100;

    public Action onLevelUp;

    public void GainExperience(int amount)
    {
        currentExperience += amount;
        while (currentExperience >= experienceToNextLevel)
        {
            currentExperience -= experienceToNextLevel;
            LevelUp();
        }
    }


    private void LevelUp()
    {
        currentLevel++;
        experienceToNextLevel = Mathf.RoundToInt(experienceToNextLevel * 1.5f);
        onLevelUp?.Invoke();
        Debug.Log($"레벨업! 현재 레벨: {currentLevel}");
    }
}
