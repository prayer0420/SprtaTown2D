using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyController : EnemyController
{
    [SerializeField][Range(0f, 100f)] private float followRange = 15f;
    [SerializeField][Range(0f, 100f)] private float shootRange = 10f;

    public AttackSO attackSO { get; set; } 


    private int layerMaskTarget;
    private int layerMaskLevel;

    protected override void Awake()
    {
        base.Awake();
        attackSO = GetComponent<AttackSO>();
    }
    protected override void Start()
    {
        base.Start();
        layerMaskLevel = LayerMask.NameToLayer("Level");
        layerMaskTarget = attackSO.target;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        float distanceToTarget = DistanceToTarget();
        Vector2 directionToTarget = DirectionToTarget();

        UpdateEnemyState(distanceToTarget, directionToTarget);


    }

    private void UpdateEnemyState(float distance, Vector2 direction)
    {
        IsAttacking = false;

        //시야 안에 있을경우
        if(distance < followRange)
        {
            CheckIfNear(distance, direction);
        }
    }

    //가까있으면 공격
    private void CheckIfNear(float distance, Vector2 direction)
    {
        if (distance <= shootRange)
            TryShootAtTarget(direction); //사정거리 안에 있으면 공격
        else
            CallMoveEvent(direction); //사정거리밖이지만 추적 범위 내에 있을 경우, 타겟 쪽으로 이동
    }

    private void TryShootAtTarget(Vector2 direction)
    {
        //origin(나가는 쪽), 방향, 범위
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, shootRange, GetLayerMaskForRayCast());
        //맞았으면
        if (hit.collider != null)
        {
            PerformAttackAction(direction);
        }
        //안맞았으면
        else
        {
            CallMoveEvent(direction);
        }

    }

    private int GetLayerMaskForRayCast()
    {
        //"Level"레이어와 타겟 레이어 모두를 포함하는 LayerMask를 반환
        return (1 << layerMaskTarget) | layerMaskTarget;
    }

    private bool IsTargetHit(RaycastHit2D hit)
    {
        // RaycastHit2D 결과를 바탕으로 실제 타겟을 명중했는지 확인
        return hit.collider != null & layerMaskTarget == (layerMaskTarget | (1<< hit.collider.gameObject.layer));
    }

    private void PerformAttackAction(Vector2 direction)
    {
        //타겟을 정확히 명중했을 경우의 행동을 정의합니다
        CallLookEvent(direction);
        CallMoveEvent(Vector2.zero); //공격중에는 이동을 멈춤
        IsAttacking = true; 
    }
}
