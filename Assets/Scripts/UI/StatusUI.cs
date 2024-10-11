using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    public Slider healthBar;
    public Slider manaBar;
    public Text levelText;
    public Text experienceText;

    private Player player;

    private void Start()
    {
        player = GameManager.Instance.Player;
        UpdateStatusUI();
    }

    private void Update()
    {
        UpdateStatusUI();
    }

    public void UpdateStatusUI()
    {
        CharacterStatsHandler characterStatsHandler = player.statHandler;
        ExperienceSystem expSystem = player.experienceSystem;

        healthBar.maxValue = characterStatsHandler.currentStat.maxHealth;
        healthBar.value = characterStatsHandler.currentHealth;

        manaBar.maxValue = characterStatsHandler.currentStat.maxMana;
        manaBar.value = characterStatsHandler.currentMana;

        levelText.text = "Level: " + expSystem.currentLevel;
        experienceText.text = "EXP: " + expSystem.currentExperience + " / " + expSystem.experienceToNextLevel;
    }
}
