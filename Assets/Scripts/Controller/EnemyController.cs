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

    //�Ÿ�
    protected float DistanceToTarget()
    {
        //����(��)���κ��� Ÿ��(�÷��̾�)������ �Ÿ�
        return Vector3.Distance(transform.position, ClosetTarget.position);
    }

    //����
    protected Vector2 DirectionToTarget()
    {
        //Ÿ��(�÷��̾�) -  ����(��)
        return ClosetTarget.position - transform.position;
    }

}
