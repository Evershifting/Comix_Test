using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackBehaviour : MonoBehaviour
{
    [SerializeField] internal float attackRange = 1f;
    [SerializeField] internal DamageStruct damageStruct;
    [SerializeField] List<DamageableTargetType> targetType;
    [Header("References")]
    [SerializeField] DamageCollider damageCollider;

    Animator animator;
    private void Awake()
    {
        if (damageCollider!=null)
        {
            damageCollider.Init(targetType, OnTargetHit);
        }
    }

    private void OnTargetHit(HealthSystem system)
    {
        throw new NotImplementedException();
    }

    public void Init(Animator animator)
    {
        this.animator = animator;
    }
    internal void Attack()
    {
        throw new NotImplementedException();
    }
}
