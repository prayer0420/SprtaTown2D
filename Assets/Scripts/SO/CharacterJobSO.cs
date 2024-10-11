using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterJob", menuName = "Character Job")]
public class CharacterJobSO : ScriptableObject
{
    public string characterJobName;                // ���� �̸�
    public Sprite characterImage;                // ���� �̹���
    
    //SO
    public StatSO baseStats;              
    public EquipmentItemSO startingWeapon;
    public AttackSO attackSO;
    
    public AnimatorOverrideController animatorOverrideController;

    //public RuntimeAnimatorController animatorController; // �ִϸ����� ��Ʈ�ѷ�
}
