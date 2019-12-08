using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// IBehaviour is an interface for all behaviour of robots in game
/// 
/// Code written by Antoine Kenneth Odi in 2017.
/// </summary>

    // This enum specifies if the behaviour is a success, failure or error
public enum BehaviourResult
{
    SUCCESS,
    FAILURE,
    ERROR
}

public interface IBehaviour {

    BehaviourResult UpdateBehaviour(AgentActor agent);

}
