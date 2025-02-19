using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    List<DamageableTargetType> potentialTargets = new List<DamageableTargetType>();

    HealthSystem targetHealthSystem;
    Action<HealthSystem> onHit;
    public void Init(List<DamageableTargetType> potentialTargets, Action<HealthSystem> onHit)
    {
        this.potentialTargets = potentialTargets;
        this.onHit = onHit;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Collided with {other.gameObject.name}");
        if (other.gameObject.TryGetComponent(out HealthSystem targetHealthSystem))
        {
            if (potentialTargets.Intersect(targetHealthSystem.DamageableTargetTypes).Any())
            {
                onHit?.Invoke(targetHealthSystem);
            }
        }
    }
}
