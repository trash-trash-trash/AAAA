using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIWalkToPlayerState : EnemyAIStateBase
{
    //TODO: make this smarter
    public Transform playerTransform;

    private bool huntingPlayer = false;
    
    public override void OnEnable()
    {
        base.OnEnable();
        obs.enabled = false;
        rb.useGravity = false;
        IPlayer player = PlayerMiddleManager.Instance;
        playerTransform = player.ReturnTransform();
        StartCoroutine(WaitUntilStopped());
    }

    IEnumerator WaitUntilStopped()
    {
        yield return new WaitForFixedUpdate();

        while (rb.linearVelocity.sqrMagnitude > 0.1f &&
               rb.angularVelocity.sqrMagnitude > 0.1f)
        {
            yield return new WaitForFixedUpdate();
            yield return null;
        }       
        agent.enabled = true;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 5f, NavMesh.AllAreas))
        {
            agent.Warp(hit.position);
        }
        Vector3 direction = agent.desiredVelocity.normalized;
        if (direction != Vector3.zero)
        {
            agent.transform.rotation = Quaternion.LookRotation(direction);
        }
        agent.SetDestination(playerTransform.position);
        agent.isStopped = false;
        huntingPlayer = true;
    }
    
    void FixedUpdate()
    {
        if (!huntingPlayer || !agent.enabled) return;

        if (Vector3.Distance(agent.destination, playerTransform.position) > 0.5f)
        {
            agent.SetDestination(playerTransform.position);
        }

        Vector3 direction = agent.desiredVelocity.normalized;
        if (direction != Vector3.zero)
        {
            agent.transform.rotation = Quaternion.LookRotation(direction);
        }
    }


    void OnDisable()
    {
        StopAllCoroutines();

        if (agent.enabled)
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            agent.enabled = false;
        }
    }
}
