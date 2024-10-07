using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private MainController mainController;

    private Rigidbody2D rigidbody2D;

    private Vector2 movementDirection = Vector2.zero;


    private void Awake()
    {
        mainController = GetComponent<MainController>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        mainController.OnMoveEvent += Move;
    }

    //movementDirection에게 어디로 갈지 지시만 함
    private void Move(Vector2 direction)
    {
        movementDirection = direction;
    }

    //실제 movementDirection
    public void ApplyMovement(Vector2 direction)
    {
        //TODO : CharacterStat
        direction = direction * 5;

        rigidbody2D.velocity = direction;
    }

    //FixedUpdate마다 실제로 움직임
    private void FixedUpdate()
    {
        ApplyMovement(movementDirection);
    }
}

