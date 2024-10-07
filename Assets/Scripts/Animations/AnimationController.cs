using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : BaseAnimationController
{
    private static readonly int isWalking = Animator.StringToHash("isWalking");


    private readonly float magnituteThreshold = 0.5f;


    protected override void Awake()
    {
        base.Awake();

    }


    private void Start()
    {
        mainController.OnMoveEvent += MoveAnim;
    }


    private void MoveAnim(Vector2 vector)
    {
        animator.SetBool(isWalking, vector.magnitude > magnituteThreshold);
    }


}
