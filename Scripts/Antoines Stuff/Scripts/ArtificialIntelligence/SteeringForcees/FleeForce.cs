using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

/// <summary>
/// This is used to create force to flee from target
/// 
/// Code written by Antoine Kenneth Odi in 2017.
/// </summary>

public class FleeForce : SteeringForce
{

    private GameObject m_target;
    private Vector3 m_vectorTarget;
    private bool m_fleeObject = true;

    public override Vector3 GetForce(AgentActor agent)
    {
        Vector3 force = new Vector3(0, 0, 0);

        // This is used to set whether to flee from a position or from a game object
        if (!m_fleeObject)
        {
            agent.gameObject.transform.LookAt(-m_vectorTarget);
            force = (agent.gameObject.transform.position - m_vectorTarget).normalized * agent.MaxSpeed;
        }
        else
        {
            Vector3 lookAtTarget = m_target.transform.position - agent.transform.position;
            Quaternion angleToPlayer = Quaternion.LookRotation(-lookAtTarget, Vector3.up);

            agent.gameObject.transform.rotation = Quaternion.Slerp(agent.gameObject.transform.rotation, angleToPlayer, 0.7f);
            force = (agent.gameObject.transform.position - m_target.transform.position).normalized * agent.MaxSpeed;
        }
        return (force - agent.Velocity);
    }

    // This functions sets the game object target
    public void SetTarget(GameObject target)
    {
        m_target = target;
        m_fleeObject = true;
    }

    // This functions sets the target as a vector
    public void SetTarget(Vector3 target)
    {
        m_vectorTarget = target;
        m_fleeObject = false;
    }


}
