using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This 3 classes are the behaviour tree nodes that determines the decision making of AIs
/// 
/// Code written by Antoine Kenneth Odi in 2017.
/// </summary>

    // This class is the base class for both selector and sequencer
public abstract class CompositeBehaviour : IBehaviour {
    protected LinkedList<IBehaviour> m_childBehaviours = new LinkedList<IBehaviour>();

    // Updates the behaviour, subclass behaviours need to override this
    public abstract BehaviourResult UpdateBehaviour(AgentActor agent);

    // This function is used to add child behaviours
    public void addBehaviour(IBehaviour child)
    {
        m_childBehaviours.AddLast(child);
    }
}

// This class is used when doing a selection between multiple options
public class Selector : CompositeBehaviour
{
    // Updates the behaviour
    public override BehaviourResult UpdateBehaviour(AgentActor agent)
    {
        foreach (IBehaviour child in m_childBehaviours)
        {
            if (child.UpdateBehaviour(agent) == BehaviourResult.SUCCESS)
                return BehaviourResult.SUCCESS;
        }
        return BehaviourResult.FAILURE;
    }

}

// This class is used when doing a sequence of actions when the conditions are met
public class Sequence : CompositeBehaviour
{
    // This class is used to update behaviour
    public override BehaviourResult UpdateBehaviour(AgentActor agent)
    {
        foreach (IBehaviour child in m_childBehaviours)
        {
            if (child.UpdateBehaviour(agent) == BehaviourResult.FAILURE)
                return BehaviourResult.FAILURE;
        }
        return BehaviourResult.SUCCESS;
    }
}
