using System;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] List<DamageableTargetType> damageableTargetTypes = new List<DamageableTargetType>();
    [SerializeField] float maxHealth = 100;

    public List<DamageableTargetType> DamageableTargetTypes { get => damageableTargetTypes; }

    float currentHealth;
    private Action onDeath;
    private Action onTakeHit;

    public void Init(Action onDeath, Action onTakeHit = null)
    {
        this.onDeath = onDeath;
        this.onTakeHit = onTakeHit;
    }

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeHit(DamageStruct damageStruct)
    {
        Debug.Log($"{gameObject.name}: HealthSystem.TakeHit: " + damageStruct.damage);
        currentHealth -= damageStruct.damage; // Damage struct is a useful way of implementing damage with some potential development down the road
        if (currentHealth <= 0)
            Die();
        else
            TakeHit();
    }

    private void TakeHit()
    {
        onTakeHit?.Invoke(); //This is a useful way to pass decision for when HealthSystem takes a hit (additional animations, persistent stats, achievements, etc.)
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name}: HealthSystem.Die from Bonk");
        onDeath?.Invoke();
    }
}
