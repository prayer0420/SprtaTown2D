using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MainController
{
    protected Transform ClosetTarget {  get; private set; }


    protected override void Awake()
    {
        base.Awake();
    }

    protected virtual void Start()
    {
        ClosetTarget = GameManager.Instance.Player.transform;
    }

    protected virtual void FixedUpdate()
    {
    }

    //거리
    protected float DistanceToTarget()
    {
        //본인(적)으로부터 타겟(플레이어)까지의 거리
        return Vector3.Distance(transform.position, ClosetTarget.position);
    }

    //방향
    protected Vector2 DirectionToTarget()
    {
        //타겟(플레이어) -  본인(적)
        return ClosetTarget.position - transform.position;
    }

}
