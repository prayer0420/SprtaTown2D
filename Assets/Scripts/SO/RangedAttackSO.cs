using UnityEngine;

[CreateAssetMenu(fileName = "RangedAttackSO", menuName = "TopDownController/Attacks/Ranged", order = 1)]
public class RangedAttackSO : AttackSO
{
    [Header("Ranged Attack Info")]
    public string bulletNameTag;
    public float duration;
    public float spread;
    public int numberOfProjectilesPerShot; //한번 나갈때 얼마나 나갈지
    public float multipleProjectilesAngle; //몇도 만큼 떨어져있는지 
    public Color projectileColor;
}