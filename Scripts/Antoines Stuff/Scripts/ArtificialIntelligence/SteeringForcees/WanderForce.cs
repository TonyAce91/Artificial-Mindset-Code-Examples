using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

/// <summary>
/// This is used to create a force that causes the agent to wander
/// 
/// Code written by Antoine Kenneth Odi in 2017.
/// </summary>

public class WanderForce : SteeringForce
{
    Vector3 m_target;
    float m_radius = 1000.0f;
    float m_jitter = 10.0f;
    float m_distance = 1.0f;


    public override Vector3 GetForce(AgentActor agent)
    {
        Random number = new Random();
        Vector3 randomVector = Random.value * new Vector3(1, 1, 1);
        randomVector = randomVector.normalized* m_jitter;

        m_target += randomVector;

        m_target = m_target.normalized * m_radius;

        Vector3 tempVector = m_target + agent.transform.position + agent.Velocity.normalized * m_distance;
       
        agent.gameObject.transform.LookAt(tempVector);

        Vector3 force = (tempVector - agent.transform.position).normalized * agent.MaxSpeed;
        return (force - agent.Velocity);
    }

    // This functions sets the parameters that determines the strength and direction of the wander force
    public void SetStartParameter(Vector3 target, float radius = 1000.0f, float jitter = 10.0f, float distance = 1.0f)
    {
        m_radius = radius;
        m_jitter = jitter;
        m_distance = distance;
        m_target = target;
    }

}
