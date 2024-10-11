using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private MainController mainController;

    [SerializeField]
    private Rigidbody2D rb;

    private Vector2 movementDirection = Vector2.zero;
    private Vector2 knockback = Vector2.zero;

    private float knockbackDuration = 0.0f;

    private CharacterStatsHandler characterStatsHandler;

    private void Awake()
    {
        mainController = GetComponent<MainController>();
        rb = GetComponent<Rigidbody2D>();
        characterStatsHandler = GetComponent<CharacterStatsHandler>();
        mainController.OnMoveEvent += Move;
    }

    private void Start()
    {
    }

    //movementDirection에게 어디로 갈지 지시만 함
    private void Move(Vector2 direction)
    {
        movementDirection = direction;
    }

    //실제 movementDirection
    private void ApplyMovement(Vector2 direction)
    {
        direction = direction * characterStatsHandler.currentStat.Maxspeed;
        
        if(knockbackDuration >0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime;
        }

        rb.velocity = direction;
    }


    //FixedUpdate마다 실제로 움직임
    private void FixedUpdate()
    {
        ApplyMovement(movementDirection);
    }

    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        knockback = -(other.position - transform.position).normalized * power;
    }
}

