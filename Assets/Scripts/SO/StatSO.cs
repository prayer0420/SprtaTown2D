using UnityEngine;

[CreateAssetMenu(fileName = "NewStat", menuName = "Stats/BaseStat")]
public class StatSO : ScriptableObject
{
    public int baseHealth;
    public int baseAttack;
    public int baseDefense;
    public int baseMana;
    public int baseSpeed;
    // 레벨에 따른 스탯 계산 함수는 제거되었습니다.
}
