using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterJob", menuName = "Character Job")]
public class CharacterJobSO : ScriptableObject
{
    public string characterJobName;                // 직업 이름
    public Sprite characterImage;                // 직업 이미지
    
    //SO
    public StatSO baseStats;              
    public EquipmentItemSO startingWeapon;
    public AttackSO attackSO;
    
    public AnimatorOverrideController animatorOverrideController;

    //public RuntimeAnimatorController animatorController; // 애니메이터 컨트롤러
}
