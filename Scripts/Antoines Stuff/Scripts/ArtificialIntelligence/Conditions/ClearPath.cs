using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is to check if there is a clear path between the target and the agent
///  
/// Code written by Antoine Kenneth Odi in 2017.
/// </summary>

public class ClearPath : Condition {

    private GameObject m_target;                        // Target of the object

    // Updates the behaviour and returns success if nothing is in the way between the target and the agent
    public override BehaviourResult UpdateBehaviour(AgentActor agent)
    {
        if (m_target == null)
            return BehaviourResult.ERROR;
        if (Physics.Linecast(agent.gameObject.transform.position, m_target.transform.position))
            return BehaviourResult.FAILURE;
        return BehaviourResult.SUCCESS;
    }

    // This function sets the target 
    public void SetTarget(GameObject target)
    {
        m_target = target;
    }

}
