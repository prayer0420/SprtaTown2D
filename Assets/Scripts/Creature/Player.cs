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

        //캐릭터 직업 가져오기
        characterJob = GameManager.Instance.GetCharacterJob();

        //캐릭터 이름 가져오기
        creatureName = GameManager.Instance.GetPlayerName();

        //캐릭터 이미지 가져오기
        MainSprite = characterJob.characterImage;
        MainSpriteObj.GetComponent<SpriteRenderer>().sprite = MainSprite;

        //캐릭터매니저에 추가
        CharacterManager.Instance.AddCharacter(this);

        // 스탯 초기화
        statHandler = new CharacterStatsHandler();
        statHandler.InitializeStats();
        
        //TODO: UIManager.Instance.UpdateStatusUI();

        // 인벤토리 시스템 초기화 및 무기 착용
        inventorySystem = new InventorySystem();
        inventorySystem.OnInventoryChanged += UIManager.Instance.UpdateInventoryUI;
        if (characterJob.startingWeapon != null)
        {
            inventorySystem.AddItem(characterJob.startingWeapon);
            inventorySystem.EquipItem(characterJob.startingWeapon);
        }

        // 애니메이터 설정 (CharacterJobSO에서 애니메이터 오버라이드 컨트롤러 설정)
        animatorOverrideController = characterJob.animatorOverrideController;
        if (characterJob.animatorOverrideController != null)
        {
            Animator animator = MainSpriteObj.GetComponent<Animator>();
            animator.runtimeAnimatorController = characterJob.animatorOverrideController;
        }

        // 경험치 시스템 초기화
        experienceSystem = new ExperienceSystem();
        experienceSystem.onLevelUp += statHandler.LevelUp;
        experienceSystem.onLevelUp += UIManager.Instance.UpdateStatusUI;



    }

    //이름, 직업, 애니메이션 컨트롤러 갱신
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
