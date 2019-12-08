using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is an abstract class which is parent class for all conditions
/// 
/// Code written by Antoine Kenneth Odi in 2017.
/// </summary>

public abstract class Condition : IBehaviour {

    // Update function of behaviours
    public abstract BehaviourResult UpdateBehaviour(AgentActor agent);
}
