using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is a condition subclass that checks if the agent is dead or not
/// 
/// Code written by Antoine Kenneth Odi in 2017.
/// </summary>

public class IsDead : Condition
{
    // Updates the behaviour and returns success if agent's health is 0 or below
    public override BehaviourResult UpdateBehaviour(AgentActor agent)
    {
        if (agent.CurrentHealth <= 0)
            return BehaviourResult.SUCCESS;
        return BehaviourResult.FAILURE;
    }

}
