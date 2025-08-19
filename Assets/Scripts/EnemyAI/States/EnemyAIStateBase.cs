using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIStateBase : MonoBehaviour
{
    public NavMeshAgent agent;
    public EnemyAIBrain brain;
    public NavMeshObstacle obs;
    public Rigidbody rb;

    public virtual void OnEnable()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        brain = GetComponentInParent<EnemyAIBrain>();
        rb = GetComponentInParent<Rigidbody>();
        obs = GetComponentInParent<NavMeshObstacle>();
    }
}
