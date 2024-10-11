using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimRotation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer armRenderer;
    [SerializeField] private Transform armPivot;
    [SerializeField] private SpriteRenderer characterRenderer;

    private MainController controller;

    private void Awake()
    {
        controller = GetComponent<MainController>();
    }

    private void Start()
    {
        controller.OnLookEvent += OnAim;
    }

    private void OnAim(Vector2 direction)
    {
        RotateArm(direction);
    }

    private void RotateArm(Vector2 direction)
    {
        //ĳ���Ϳ��� ���콺�� �ٶ󺸴� ����(����)  * ����->������ ������
        float rotZ = MathF.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //-90~90���� ������ �������� �ٶ󺸰��ִ°�, -90���̸�, 90�� �ʰ���� ������ �ٶ󺸴� ���� ���밪��������~
        characterRenderer.flipX = Mathf.Abs(rotZ) > 90f;
        armRenderer.flipY = characterRenderer.flipX;


        //���� ������ Ȱ���� armPivot�� ������ ������
        armPivot.rotation = Quaternion.Euler(0, 0, rotZ);
    }

}
