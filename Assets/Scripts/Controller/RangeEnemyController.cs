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

        //�þ� �ȿ� �������
        if(distance < followRange)
        {
            CheckIfNear(distance, direction);
        }
    }

    //���������� ����
    private void CheckIfNear(float distance, Vector2 direction)
    {
        if (distance <= shootRange)
            TryShootAtTarget(direction); //�����Ÿ� �ȿ� ������ ����
        else
            CallMoveEvent(direction); //�����Ÿ��������� ���� ���� ���� ���� ���, Ÿ�� ������ �̵�
    }

    private void TryShootAtTarget(Vector2 direction)
    {
        //origin(������ ��), ����, ����
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, shootRange, GetLayerMaskForRayCast());
        //�¾�����
        if (hit.collider != null)
        {
            PerformAttackAction(direction);
        }
        //�ȸ¾�����
        else
        {
            CallMoveEvent(direction);
        }

    }

    private int GetLayerMaskForRayCast()
    {
        //"Level"���̾�� Ÿ�� ���̾� ��θ� �����ϴ� LayerMask�� ��ȯ
        return (1 << layerMaskTarget) | layerMaskTarget;
    }

    private bool IsTargetHit(RaycastHit2D hit)
    {
        // RaycastHit2D ����� �������� ���� Ÿ���� �����ߴ��� Ȯ��
        return hit.collider != null & layerMaskTarget == (layerMaskTarget | (1<< hit.collider.gameObject.layer));
    }

    private void PerformAttackAction(Vector2 direction)
    {
        //Ÿ���� ��Ȯ�� �������� ����� �ൿ�� �����մϴ�
        CallLookEvent(direction);
        CallMoveEvent(Vector2.zero); //�����߿��� �̵��� ����
        IsAttacking = true; 
    }
}
