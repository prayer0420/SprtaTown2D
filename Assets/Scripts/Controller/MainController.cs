using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent; //Action�� ������ void�� ��ȯ�ؾ���. (��ȯ���� �ִٸ� Func)
    public event Action<Vector2> OnLookEvent;
    public event Action<AttackSO> OnAttackEvent;

    protected bool IsAttacking { get; set; }

    //�����������κ��� ���ʰ� ��������
    private float timeSinceLastAttack = float.MaxValue;

    protected AttackSO attackSO { get; set; }

    protected virtual void Awake()
    {
        attackSO = GameManager.Instance.GetCharacterJob().attackSO;
    }


    private void Update()
    {
        //0.2�ʸ��� Attack
        HandleAttackDelay();
    }

    private void HandleAttackDelay()
    {
        if (timeSinceLastAttack < attackSO.delay)
        {
            timeSinceLastAttack += Time.deltaTime;
        }
        else if (IsAttacking && timeSinceLastAttack >= attackSO.delay)
        {
            timeSinceLastAttack = 0f;
            CallAtackEvent(attackSO);
        }
    }


    public void CallMoveEvent(Vector2 direction)
    {

        OnMoveEvent?.Invoke(direction);     //null�̸� ������ϰ�, null�� �ƴϸ� ����

    }
    public void CallLookEvent(Vector2 direction)
    {
        OnLookEvent?.Invoke(direction);
    }

    private void CallAtackEvent(AttackSO attackSO)
    {
        OnAttackEvent?.Invoke(attackSO);

    }


}
