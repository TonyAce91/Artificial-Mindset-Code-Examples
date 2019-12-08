using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class specifies the action of robots in game during patrol state
/// 
/// Code written by Antoine Kenneth Odi in 2017.
/// </summary>

public class PatrolBehaviour : IBehaviour
{
    private Vector3 patrolPointA;
    private Vector3 patrolPointB;
    private Vector3 currentTarget;
    private Transform pointTransform;

    // This sets the patrol points of AI
    public void SetPatrolPoints(Vector3 pointA, Vector3 pointB)
    {
        patrolPointA = pointA;
        patrolPointB = pointB;
    }

    // Initialisation
    public void StartUp()
    {
        currentTarget = patrolPointA;
    }

    // Updates the behaviour
    public BehaviourResult UpdateBehaviour(AgentActor agent)
    {
        Vector3 agentPos = agent.gameObject.transform.position;

        // Swaps the patrol points when one is reached
        if ((agentPos - currentTarget).sqrMagnitude <= 10)
        {
            if (currentTarget == patrolPointA)
                currentTarget = patrolPointB;
            else
                currentTarget = patrolPointA;
        }

        Vector3 lookAtTarget = currentTarget - agent.transform.position;

        // This is used to slow down the turn of robot
        Quaternion angleToPlayer = Quaternion.LookRotation(lookAtTarget, Vector3.up);
        agent.gameObject.transform.rotation = Quaternion.Slerp(agent.gameObject.transform.rotation, angleToPlayer, 0.7f);

        // This is used to add force to the agent to move
        Vector3 force = (currentTarget - agentPos).normalized * agent.MaxSpeed;
        agent.AddForce(force - agent.Velocity);

        return BehaviourResult.SUCCESS;
    }
}
