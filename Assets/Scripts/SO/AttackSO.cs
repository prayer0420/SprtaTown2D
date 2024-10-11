using UnityEngine;

[CreateAssetMenu(fileName = "DefaultAttackSO", menuName = "TopDownController/Attacks/Default", order = 0)]
public class AttackSO : ScriptableObject
{
    [Header("Attack Info")]
    public float size;
    public float delay;
    public float power;
    public float speed;
    public LayerMask target;
    public Animation anim;

    [Header("Knock Back Info")]
    public bool isOnknockBack;
    public float knockbackPower;
    public float knockbackTime;

}
