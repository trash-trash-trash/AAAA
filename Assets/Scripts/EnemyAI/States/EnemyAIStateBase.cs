using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIStateBase : MonoBehaviour
{
    public NavMeshAgent agent;
    public NavMeshObstacle obs;
    public Rigidbody rb;

    public virtual void OnEnable()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        rb = GetComponentInParent<Rigidbody>();
        obs = GetComponentInParent<NavMeshObstacle>();
    }
}
