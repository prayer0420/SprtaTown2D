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
        //캐릭터에서 마우스를 바라보는 각도(라디안)  * 라디안->각도를 곱해줌
        float rotZ = MathF.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //-90~90도가 오른쪽 방향으로 바라보고있는것, -90도미만, 90도 초과라면 왼쪽을 바라보는 것을 절대값기준으로~
        characterRenderer.flipX = Mathf.Abs(rotZ) > 90f;
        armRenderer.flipY = characterRenderer.flipX;


        //얻은 각도를 활용해 armPivot의 각도로 맞춰줌
        armPivot.rotation = Quaternion.Euler(0, 0, rotZ);
    }

}
