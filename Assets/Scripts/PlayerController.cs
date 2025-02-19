using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal.ShaderGUI;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour, IAttackAnimationListener
{
    [SerializeField] Mover mover;
    #region it should be a weapon, that references damage collider and all the other stuff, but implementing weapon system is way too out of the scope even for me)
    [Space]
    [SerializeField] DamageCollider damageCollider;
    [SerializeField] DamageStruct damageStruct;
    [SerializeField] List<DamageableTargetType> damageableTargetTypes = new List<DamageableTargetType> { DamageableTargetType.Enemy };
    #endregion

    CharacterController controller;
    Animator animator;
    HealthSystem healthSystem;
    [SerializeField] bool canDealDamage = false;
    [SerializeField] bool isAttacking = false;
    //in the future access to the player will be through proper game manager
    public static PlayerController Instance { get; private set; }

    HealthSystem targetHealthSystem;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(this);

        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        healthSystem = GetComponent<HealthSystem>();
        GetComponentInChildren<AnimationHelper>()?.Init(this);
        healthSystem.Init(
            () => { Debug.Log("PlayerController: Die"); },
            () => { Debug.Log("PlayerController: Hit taken"); }
            );
        mover.Init(controller, animator);
        damageCollider.Init(damageableTargetTypes, OnTargetHit);
    }

    //further down the road this can be moved to a weapon with different timings and ability to damage several targets or single target multiple times
    private void OnTargetHit(HealthSystem system)
    {
        if (!canDealDamage)
            return;
        canDealDamage = false;
        system.TakeHit(damageStruct);
    }

    AnimationClip attackClip;
    void Update()
    {
        //Jump through the platform
        if (controller.isGrounded && Input.GetButtonDown("Jump") && Input.GetAxis("Vertical") < 0)
        {
            //set layer to ignore platform
            mover.Jump(false);
        }
        if (controller.isGrounded && Input.GetButtonDown("Fire1") && !isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("Attack");
        }

        // Move left/right
        mover.Move(transform.position + transform.TransformDirection(Vector3.right) * Input.GetAxis("Horizontal"));
    }

    public void OnAttackStart()
    {
        canDealDamage = true;
    }

    public void OnAttackEnd()
    {
        canDealDamage = false;
        isAttacking = false;
    }
}

public interface IAttackAnimationListener
{
    void OnAttackStart();
    void OnAttackEnd();
}