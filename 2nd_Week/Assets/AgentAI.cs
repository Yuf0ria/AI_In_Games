using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class AgentAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform pointA, pointB, currentTarget;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(pointA.position);
    }

    void Update()
    {
        if (agent.pathPending) return;
        if (agent.remainingDistance <= agent.stoppingDistance && agent.velocity.sqrMagnitude == 0)
        {
           currentTarget = (currentTarget == pointA) ? pointB : pointA;
            agent.SetDestination(currentTarget.position);
        }
    }
}
