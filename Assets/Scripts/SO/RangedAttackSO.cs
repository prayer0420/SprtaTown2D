using UnityEngine;

[CreateAssetMenu(fileName = "RangedAttackSO", menuName = "TopDownController/Attacks/Ranged", order = 1)]
public class RangedAttackSO : AttackSO
{
    [Header("Ranged Attack Info")]
    public string bulletNameTag;
    public float duration;
    public float spread;
    public int numberOfProjectilesPerShot; //�ѹ� ������ �󸶳� ������
    public float multipleProjectilesAngle; //� ��ŭ �������ִ��� 
    public Color projectileColor;
}