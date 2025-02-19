using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleMover", menuName = "Components/Movers/SimpleMover")]
//moves character with SimpleMove method left or right
internal class SimpleMover : Mover
{
    CharacterController controller;
    Animator animator;
    public override void Init(CharacterController character, Animator animator)
    {
        controller = character;
        this.animator = animator;
    }
    public override void Move(Vector2 positionToMoveTowards)
    {
        positionToMoveTowards.y = 0;
        positionToMoveTowards.x = positionToMoveTowards.x - controller.transform.position.x;
        controller.SimpleMove(positionToMoveTowards.normalized * speed); //I don't want to bother with gravity and create a proper Character Controller with acceleration etc.
        
        if (animator == null) 
            return;
        if (controller.velocity.x > 0)
            animator.transform.parent.rotation = Quaternion.Euler(0, 90, 0);
        else if (controller.velocity.x < 0)
            animator.transform.parent.rotation = Quaternion.Euler(0, 270, 0);
        animator.SetFloat("SpeedX", Mathf.Abs(positionToMoveTowards.x));
    }

    public override void Jump(bool up)
    {
        Debug.Log($"Jumping {(up ? "up" : "down")} remove this string later");
    }
}
