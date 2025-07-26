using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIIdleState : EnemyAIStateBase
{
    public override void OnEnable()
    {
        base.OnEnable();
        obs.enabled = true;
        rb.useGravity = true;
    }

    private void OnDisable()
    {
    }
}
