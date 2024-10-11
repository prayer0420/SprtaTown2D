using UnityEngine;

[CreateAssetMenu(fileName = "NewStat", menuName = "Stats/BaseStat")]
public class StatSO : ScriptableObject
{
    public int baseHealth;
    public int baseAttack;
    public int baseDefense;
    public int baseMana;
    public int baseSpeed;
    // ������ ���� ���� ��� �Լ��� ���ŵǾ����ϴ�.
}
