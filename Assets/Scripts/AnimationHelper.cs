using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHelper : MonoBehaviour
{
    IAttackAnimationListener listener;
    internal void Init(IAttackAnimationListener listener)
    {
        this.listener = listener;
    }

    void ActivateDamageFrame(int isActive)
    {
        if (isActive>0)
            listener?.OnAttackStart();
        else
            listener?.OnAttackEnd();
    }
}
