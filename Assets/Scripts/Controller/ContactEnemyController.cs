using System;
using UnityEngine;

public class ContactEnemyController : EnemyController
{
    [SerializeField][Range(0f, 100f)] private float followRange;
    [SerializeField] private string targetTag = "Player";
    private bool isCollidingWithTarget;

    [SerializeField] private SpriteRenderer characterRenderer;
    
    
    HealthSystem healthSystem;

    private HealthSystem collidingTargetHealthSystem;
    private Movement collidingMovement;
    
    protected override void Start()
    {
        base.Start();
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDamage += OnDamage;
    }

    private void OnDamage()
    {
        //공격 받으면 무조건 따라가도록
        followRange = 100f;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        //타겟과 닿았다면
        if(isCollidingWithTarget)
        {
            ApplyHealthChange();
        }

        Vector2 direction = Vector2.zero;

        //타겟(플레이어)와의 거리가 일정범위보다 작다면 (followRawnge범위안에 들어왔따면)
        if(DistanceToTarget() < followRange)
        {
            //방향을 타겟 방향으로!
            direction = DirectionToTarget();
        }
        //이동!
        CallMoveEvent(direction);
        //방향에 맞게 회전
        Rotate(direction); 


    }

    private void ApplyHealthChange()
    {
        bool isAttackable = collidingTargetHealthSystem.ChangeHealth(-attackSO.power);
        //공격이 가능한 상태일때 넉백 처리
        if (isAttackable && attackSO.isOnknockBack)
        {
            if(collidingMovement!=null)
            {
                collidingMovement.ApplyKnockback(transform, attackSO.knockbackPower, attackSO.knockbackTime);
            }
        }

    }

    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        characterRenderer.flipX = Mathf.Abs(rotZ) > 90f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //닿은 타겟
        GameObject receiver = collision.gameObject;

        //targetTag(==Player)가 아니면 리턴
        if (!receiver.CompareTag(targetTag))
        {
            return;
        }

        //닿은 타겟의 헬스시스템과 이동시스템을 가져옴
        collidingTargetHealthSystem = collision.GetComponent<HealthSystem>();

        if(collidingTargetHealthSystem != null)
        {
            isCollidingWithTarget = true;
        }

        collidingMovement = collision.GetComponent<Movement>();


    }

    //닿았다가 떨어지면
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!collision.CompareTag(targetTag)) return;
        isCollidingWithTarget = false;
    }


}
