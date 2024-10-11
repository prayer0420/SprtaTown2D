using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent; //Action은 무조건 void만 봔환해야함. (반환값이 있다면 Func)
    public event Action<Vector2> OnLookEvent;
    public event Action<AttackSO> OnAttackEvent;

    protected bool IsAttacking { get; set; }

    //지난공격으로부터 몇초가 지났는지
    private float timeSinceLastAttack = float.MaxValue;

    protected AttackSO attackSO { get; set; }

    protected virtual void Awake()
    {
        attackSO = GameManager.Instance.GetCharacterJob().attackSO;
    }


    private void Update()
    {
        //0.2초마다 Attack
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

        OnMoveEvent?.Invoke(direction);     //null이면 실행안하고, null이 아니면 실행

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
