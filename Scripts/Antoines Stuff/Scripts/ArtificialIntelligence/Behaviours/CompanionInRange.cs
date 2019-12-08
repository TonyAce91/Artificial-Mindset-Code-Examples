using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class states the behaviour of the companion when it's not chasing the player
/// 
/// Code written by Antoine Kenneth Odi in 2017.
/// </summary>

public class CompanionInRange : IBehaviour
{

    private GameObject m_playerReference;
    private FPSController playerScript;
    private float randomViewTimer = 3;
    private float viewTime = 3;
    private Quaternion randomRotation;

    public BehaviourResult UpdateBehaviour(AgentActor agent)
    {
        agent.Velocity = Vector3.zero;
        
        // Randomly looks around
        randomViewTimer -= Time.deltaTime;
        if (randomViewTimer <= 0)
        {
            Vector3 euler = new Vector3(0, 0, 0);
            euler.y = Random.Range(0f, 360f);
            randomRotation = Quaternion.Euler(euler);
            randomViewTimer = viewTime;
        }

        // Slows down the companion's turn
        agent.gameObject.transform.rotation = Quaternion.Slerp(agent.gameObject.transform.rotation, randomRotation, Time.time *0.001f);

        return BehaviourResult.SUCCESS;
    }

    public void SetPlayer(GameObject player)
    {
        m_playerReference = player;
    }


}
