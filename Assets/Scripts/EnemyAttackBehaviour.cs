using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackBehaviour : MonoBehaviour, IAttackAnimationListener
{
    [SerializeField] List<DamageableTargetType> targetType = new List<DamageableTargetType>() { DamageableTargetType.Player };

    [Space, Header("Weapon")] //same case as with player. This should be implemented through weapon prefab
    [SerializeField] DamageCollider damageCollider;
    [SerializeField] internal float attackRange = 1f;
    [SerializeField] internal DamageStruct damageStruct;

    Animator animator;
    bool canDealDamage;
    bool canAttack = true;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        GetComponentInChildren<AnimationHelper>()?.Init(this);
        if (damageCollider != null)
        {
            damageCollider.Init(targetType, OnTargetHit);
        }
    }


    public void Init(Animator animator)
    {
        this.animator = animator;
    }

    private void OnTargetHit(HealthSystem system)
    {
        if (canDealDamage)
        {
            canDealDamage = false;
            system.TakeHit(damageStruct);
        }
    }
    internal void Attack()
    {
        if (!canAttack)
            return;
        canAttack = false;
        animator.SetTrigger("Attack");
    }

    public void OnAttackStart()
    {
        canDealDamage = true;
    }

    public void OnAttackEnd()
    {
        canAttack = true;
        canDealDamage = false;
    }
}
