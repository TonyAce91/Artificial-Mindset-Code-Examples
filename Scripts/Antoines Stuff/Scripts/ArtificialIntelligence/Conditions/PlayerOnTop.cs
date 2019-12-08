using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// 
/// Code written by Antoine Kenneth Odi in 2017.
/// </summary>

public class PlayerOnTop : Condition
{
    // Use this for initialization


    private GameObject m_target;

    // Update is called once per frame
    public override BehaviourResult UpdateBehaviour(AgentActor agent)
    {
        if ((agent.transform.position - m_target.transform.position).sqrMagnitude <= 2
             && m_target.transform.position.y > agent.transform.position.y)
            return BehaviourResult.SUCCESS;
        return BehaviourResult.FAILURE;
    }

    public void SetTarget(GameObject target)
    {
        m_target = target;
    }

}