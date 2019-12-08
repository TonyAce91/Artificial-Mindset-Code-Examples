using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class controls the companion after it is hacked
/// 
/// Code written by Antoine Kenneth Odi in 2017.
/// </summary>

public class CompanionAgent : AgentActor
{
    // Reference of player
    private GameObject player;
    private FPSController playerScript;

    // Steering behaviours
    private SteeringBehaviour seekBehaviour;
    private SteeringBehaviour fleeBehaviour;
    private SteeringBehaviour wanderBehaviour;

    // Forces that produces steering behaviours
    private SeekForce m_seekForce;
    private HorizontalFleeForce m_fleeForce;
    private WanderForce m_wanderForce;

    // Texture after the companion has been hacked
    [SerializeField] private Texture companionTexture;

    // Use for initialising the companion
    void Start()
    {
        // Turns off companion hack script, change the emission texture of the companion and make it moveable
        CompanionHackScript companionHack = gameObject.GetComponent<CompanionHackScript>();
        companionHack.enabled = false;
        GetComponent<Renderer>().material.SetTexture("_EmissionMap", companionTexture);
        GetComponent<Rigidbody>().isKinematic = false;


        // Initialise behaviour list
        m_behaviours = new List<IBehaviour>();

        // Find the player game object
        player = FindObjectOfType<FPSController>().gameObject;
        playerScript = player.GetComponent<FPSController>();

        //-----------------------------------------------------------------
        // The Flee Sequence

        // Set up the flee force and flee force parameter
        m_fleeForce = new HorizontalFleeForce();
        m_fleeForce.SetTarget(player/*.transform.position + new Vector3 (2, 2, 2)*/);

        // Set up the flee behaviour
        fleeBehaviour = new SteeringBehaviour();
        fleeBehaviour.Constructor();
        fleeBehaviour.AddNewForce(m_fleeForce);

        // Set up condition for flee sequence
        WithinRange fleeCondition = new WithinRange();
        fleeCondition.SetParameters(player, 2.0f);

        // Set up flee sequence
        Sequence fleeSequence = new Sequence();
        fleeSequence.addBehaviour(fleeCondition);
        fleeSequence.addBehaviour(fleeBehaviour);




        //-----------------------------------------------------------------
        // The Chase Sequence

        // Set up the seek force and seek force parameter
        m_seekForce = new SeekForce();
        m_seekForce.SetTarget(player/*.transform.position + new Vector3 (2, 2, 2)*/);

        // Set up the seek behaviour
        seekBehaviour = new SteeringBehaviour();
        seekBehaviour.Constructor();
        seekBehaviour.AddNewForce(m_seekForce);

        // Set up condition for chase sequence
        WithinRange chaseCondition = new WithinRange();
        chaseCondition.SetParameters(player, 3.5f);

        // Set up the reverse condition
        NotCondition notChase = new NotCondition();
        notChase.SetCondition(chaseCondition);

        // Set up chase sequence
        Sequence chaseSequence = new Sequence();
        chaseSequence.addBehaviour(notChase);
        chaseSequence.addBehaviour(seekBehaviour);


        //-----------------------------------------------------------------
        // The In Range Sequence

        // Companion in range behaviour

        CompanionInRange inRangeBehaviour = new CompanionInRange();
        inRangeBehaviour.SetPlayer(player);


        //----------------------------------------------------------------
        // The Main Selector

        // Set up main selector
        Selector mainSelector = new Selector();
        mainSelector.addBehaviour(fleeSequence);
        mainSelector.addBehaviour(chaseSequence);
        mainSelector.addBehaviour(inRangeBehaviour);


        // Add all sequences to behaviour list
        m_behaviours.Add(mainSelector);

        // Setting the forward direction
        transform.forward = new Vector3(0, 0, 1);

    }

    // Updates the companion
    void FixedUpdate()
    {
        UpdateBehaviours();
    }

}
