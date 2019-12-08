using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the main class used to control the enemy AI
/// 
/// Code written by Antoine Kenneth Odi in 2017.
/// </summary>

public class EnemyRobot : AgentActor {

    private GameObject player;
    private FPSController playerScript;

    // Steering behaviour of the enemy AI
    private SteeringBehaviour seekBehaviour;
    private SteeringBehaviour fleeBehaviour;

    // Forces that controls the steering behaviours
    private SeekForce m_seekForce;
    private HorizontalFleeForce m_fleeForce;

    // Spawner reference
    private ChargingDockSpawner m_spawnerReference;

    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private float chaseRange = 20;
    [SerializeField] private float attackRange = 10;
    [SerializeField] private Vector3 m_patrolPointA = new Vector3 (20, 3, 12);
    [SerializeField] private Vector3 m_patrolPointB = new Vector3(-20, 3, 12);
    [SerializeField] private Vector3 maximumPossiblePoint  = new Vector3 (100, 30, 100);
    [SerializeField] private Vector3 minimumPossiblePoint = new Vector3(-100, 0, -100);
    [SerializeField] private float maxHealth = 1;
    [SerializeField] private AudioSource stateAudioSource;
    [SerializeField] private AudioClip attackStateClip;
    [SerializeField] private AudioClip chaseStateClip;
    [SerializeField] private AudioClip patrolStateClip;




    // Use this for initialization
    void Start () {
        // Initialise behaviour list
        m_behaviours = new List<IBehaviour>();

        // Find the player game object
        player = FindObjectOfType<FPSController>().gameObject;
        playerScript = player.GetComponent<FPSController>();

        currentHealth = maxHealth;        

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
        fleeCondition.SetParameters(player, 1.5f);

        // Set up flee sequence
        Sequence fleeSequence = new Sequence();
        fleeSequence.addBehaviour(fleeCondition);
        fleeSequence.addBehaviour(fleeBehaviour);

        //-----------------------------------------------------------------
        // The Attack Sequence

        // Set up the attack behaviour
        ShootBehaviour attackBehaviour = new ShootBehaviour();
        attackBehaviour.SetTarget(player);
        attackBehaviour.SetWeaponType(ShootBehaviour.WeaponType.HitScanType);
        List<GameObject> guns = new List<GameObject>();
        
        // Give a list of bullet spawners to the shoot behaviour
        foreach (Transform child in transform)
        {
            GunParticleSystem gun;
            gun = child.gameObject.GetComponent<GunParticleSystem>();
            if (gun != null)
                guns.Add(gun.gameObject); 
        }
        attackBehaviour.SetGuns(guns);

        // Set up condition for the attack sequence
        WithinRange attackCondition = new WithinRange();
        attackCondition.SetParameters(player, attackRange);

        // Set up play sound behaviour
        PlaySound playAttackSound = new PlaySound();

        if (stateAudioSource != null && attackStateClip != null)
        {
            playAttackSound.SetAudioSource(stateAudioSource);
            playAttackSound.SetAudioClip(attackStateClip);
            playAttackSound.SetTimer(15.0f);
        }

        // Set up attack sequence
        Sequence attackSequence = new Sequence();
        attackSequence.addBehaviour(attackCondition);
        if (stateAudioSource != null && attackStateClip != null)
            attackSequence.addBehaviour(playAttackSound);
        attackSequence.addBehaviour(attackBehaviour);



        //-----------------------------------------------------------------
        // The Chase Sequence

        // Set up the seek force and seek force parameter
        m_seekForce = new SeekForce();
        m_seekForce.SetTarget(player);
        
        // Set up the seek behaviour
        seekBehaviour = new SteeringBehaviour();
        seekBehaviour.Constructor();
        seekBehaviour.AddNewForce(m_seekForce);

        // Set up condition for chase sequence
        WithinRange chaseCondition = new WithinRange();
        chaseCondition.SetParameters(player, chaseRange);

        // Set up play sound behaviour
        PlaySound playChaseSound = new PlaySound();

        if (stateAudioSource != null && chaseStateClip != null)
        {
            playChaseSound.SetAudioSource(stateAudioSource);
            playChaseSound.SetAudioClip(chaseStateClip);
            playChaseSound.SetTimer(15.0f);
        }

        // Set up chase sequence
        Sequence chaseSequence = new Sequence();
        chaseSequence.addBehaviour(chaseCondition);
        if (stateAudioSource != null && chaseStateClip != null)
            chaseSequence.addBehaviour(playChaseSound);
        chaseSequence.addBehaviour(seekBehaviour);



        //----------------------------------------------------------------
        // The Patrol Sequence

        // Set up patrol behaviour
        PatrolBehaviour patrol = new PatrolBehaviour();
        patrol.SetPatrolPoints(m_patrolPointA, m_patrolPointB);
        patrol.StartUp();

        // Set up play sound behaviour
        PlaySound playPatrolSound = new PlaySound();

        if (stateAudioSource != null && chaseStateClip != null)
        {
            playPatrolSound.SetAudioSource(stateAudioSource);
            playPatrolSound.SetAudioClip(patrolStateClip);
            playPatrolSound.SetTimer(15.0f);
        }

        // Set up patrol sequence
        Sequence patrolSequence = new Sequence();
        if (stateAudioSource != null && chaseStateClip != null)
            chaseSequence.addBehaviour(playPatrolSound);
        patrolSequence.addBehaviour(patrol);

        //----------------------------------------------------------------
        // The Main Selector

        // Set up main selector
        Selector mainSelector = new Selector();
        mainSelector.addBehaviour(attackSequence);
        mainSelector.addBehaviour(chaseSequence);
        mainSelector.addBehaviour(patrolSequence);



        // Add all sequences to behaviour list
        m_behaviours.Add(mainSelector);

        // Setting the forward direction
        transform.forward = new Vector3(0, 0, 1);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        UpdateBehaviours();
        if (currentHealth <= 0)
        {
            currentHealth = maxHealth;
            m_spawnerReference.RobotDied();
            gameObject.SetActive(false);
        }
        //transform.LookAt(player.transform);
    }

    // Sets the patrol points of each robot
    public void SetPatrolPoints(Vector3 patrolPointA, Vector3 patrolPointB)
    {
        m_patrolPointA = patrolPointA;
        m_patrolPointB = patrolPointB;
    }

    // Applies the damage when the enemy is hit
    public void ApplyDamage (float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
    }

    // Spawner reference property so that behaviour tree can have access to it and also for the charging dock spawner to set itself up
    public ChargingDockSpawner SpawnerReference
    {
        get
        {
            return m_spawnerReference;
        }
        set
        {
            m_spawnerReference = value;
        }
    }
    
}
