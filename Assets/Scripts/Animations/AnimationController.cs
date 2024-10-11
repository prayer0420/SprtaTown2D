
using System;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    protected Animator animator;
    protected MainController mainController;

    private static readonly int isWalking = Animator.StringToHash("isWalking");
    private static readonly int isHit = Animator.StringToHash("isHit");
    private static readonly int Attack = Animator.StringToHash("Attack");

    private readonly float magnituteThreshold = 0.5f; //최소 0.5는 넘어야한다..

    private HealthSystem healthSystem;
    protected void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        mainController = GetComponent<MainController>();
        healthSystem = GetComponent<HealthSystem>();
    }

    private void Start()
    {
        mainController.OnAttackEvent += Attacking;
        mainController.OnMoveEvent += Move;
        if (healthSystem != null)
        {
            healthSystem.OnDamage += Hit;
            healthSystem.OnInvincibilityEnd += InvincibilityEnd;
        }
    }

    private void Move(Vector2 vector)
    {
        //vector의 크기가 0.5보다 클때만 true(이동중으로)
        animator.SetBool(isWalking, vector.magnitude > magnituteThreshold);
    }

    private void Attacking(AttackSO sO)
    {
        animator.SetTrigger(Attack);
    }

    private void Hit()
    {
        animator.SetBool(isHit, true);
    }

    //무적일때 
    private void InvincibilityEnd()
    {
        animator.SetBool(isHit, false);
    }

}