using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimRotation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer characterRenderer;
    //[SerializeField] private SpriteRenderer armRenderer;
    //[SerializeField] private Transform armPivot;

    private MainController mainController;

    private void Awake()
    {
        mainController = GetComponent<MainController>();
    }

    void Start()
    {
        mainController.OnLookEvent += OnAim;
    }

    private void OnAim(Vector2 direction)
    {
        RotateArm(direction);
    }


    private void RotateArm(Vector2 direction)
    {
        //y,x 따른 라디안 각도를 구하고 라디안각도를 디그리로 바꿔줌
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        characterRenderer.flipX = Mathf.Abs(rotZ) > 90f; //
        Debug.Log(Mathf.Abs(rotZ));
    }

}
