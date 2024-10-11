using System;
using UnityEngine;

public class Player : Creature
{
    public CharacterJobSO characterJob;
    public InventorySystem inventorySystem;
    public ExperienceSystem experienceSystem;

    private WeaponSO equippedWeapon;

    public event Action OnStatusChanged;
    public event Action OnExperienceGained;

    public Sprite MainSprite;

    public AnimatorOverrideController animatorOverrideController;

    public int Gold = 10000;
    private void Start()
    {

        GameManager.Instance.OnPlayerInfoChanged += ChangeInfo;
        GameManager.Instance.Player = this;

        //ĳ���� ���� ��������
        characterJob = GameManager.Instance.GetCharacterJob();

        //ĳ���� �̸� ��������
        creatureName = GameManager.Instance.GetPlayerName();

        //ĳ���� �̹��� ��������
        MainSprite = characterJob.characterImage;
        MainSpriteObj.GetComponent<SpriteRenderer>().sprite = MainSprite;

        //ĳ���͸Ŵ����� �߰�
        CharacterManager.Instance.AddCharacter(this);

        // ���� �ʱ�ȭ
        statHandler = new CharacterStatsHandler();
        statHandler.InitializeStats();
        
        //TODO: UIManager.Instance.UpdateStatusUI();

        // �κ��丮 �ý��� �ʱ�ȭ �� ���� ����
        inventorySystem = new InventorySystem();
        inventorySystem.OnInventoryChanged += UIManager.Instance.UpdateInventoryUI;
        if (characterJob.startingWeapon != null)
        {
            inventorySystem.AddItem(characterJob.startingWeapon);
            inventorySystem.EquipItem(characterJob.startingWeapon);
        }

        // �ִϸ����� ���� (CharacterJobSO���� �ִϸ����� �������̵� ��Ʈ�ѷ� ����)
        animatorOverrideController = characterJob.animatorOverrideController;
        if (characterJob.animatorOverrideController != null)
        {
            Animator animator = MainSpriteObj.GetComponent<Animator>();
            animator.runtimeAnimatorController = characterJob.animatorOverrideController;
        }

        // ����ġ �ý��� �ʱ�ȭ
        experienceSystem = new ExperienceSystem();
        experienceSystem.onLevelUp += statHandler.LevelUp;
        experienceSystem.onLevelUp += UIManager.Instance.UpdateStatusUI;



    }

    //�̸�, ����, �ִϸ��̼� ��Ʈ�ѷ� ����
    public void ChangeInfo()
    {
        MainSpriteObj.GetComponent<SpriteRenderer>().sprite = MainSprite;
        SetCreatureName(GameManager.Instance.GetPlayerName());
        characterJob = GameManager.Instance.GetCharacterJob();

        if (characterJob.animatorOverrideController != null)
        {
            Animator animator = MainSpriteObj.GetComponent<Animator>();
            animator.runtimeAnimatorController = characterJob.animatorOverrideController;
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        OnStatusChanged?.Invoke();
    }

    public override void Heal(int amount)
    {
        base.Heal(amount);
        OnStatusChanged?.Invoke();
    }
    public void Attack()
    {
        if (equippedWeapon != null)
        {
            equippedWeapon.PerformAttack(gameObject);
        }
    }
    public void GainExperience(int amount)
    {
        experienceSystem.GainExperience(amount);
        OnExperienceGained?.Invoke();
    }
}
