using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class is used to determine what AI would do when it dies
/// 
/// Code written by Antoine Kenneth Odi in 2017.
/// </summary>

public class DieBehaviour : IBehaviour
{

    private ParticleSystem m_explosionParticle;
    private Vector3 m_respawnPosition = new Vector3(0, 3, 0);
    private float m_explosionTime;
    private float m_explosionTimer;
    private float m_maxHealth;

    private GameObject m_explosionGameObject;
    private GameObject m_ammoPack;

    private ChargingDockSpawner m_spawnerReference;

    // Update is called once per frame
    public BehaviourResult UpdateBehaviour(AgentActor agent)
    {

        if (agent.CurrentHealth <= 0)
        {
            // Instantiate explosion with audio and ammo packs that the player can pick up
            UnityEngine.Object.Instantiate(m_explosionGameObject, agent.transform.position, Quaternion.identity);
            UnityEngine.Object.Instantiate(m_ammoPack, agent.transform.position, Quaternion.identity);

            // Resets the current health of agent
            agent.CurrentHealth = m_maxHealth;

            // Specific code for enemy robot
            if (agent.gameObject.GetComponent<EnemyRobot>()!= null)
            {
                agent.gameObject.GetComponent<EnemyRobot>().SpawnerReference.RobotDied();
            }
            agent.gameObject.SetActive(false);
        }

        // Turn on the Explosion particle system for couple of secounds before resetting position of the enemy
        agent.gameObject.GetComponent<Renderer>().enabled = false;
        m_explosionParticle.Play();
        if (m_explosionTimer <= 0)
        {
            agent.gameObject.GetComponent<Renderer>().enabled = true;
            agent.CurrentHealth = m_maxHealth;

            if (m_spawnerReference != null)
                m_spawnerReference.RobotDied();
            else

            agent.gameObject.SetActive(false);

        }

        return BehaviourResult.SUCCESS;
    }

    public void SetParticleSystem(ParticleSystem explosion)
    {
        m_explosionParticle = explosion;
    }

    // This is specific to enemy robot
    public void SetSpawnerReference(ChargingDockSpawner spawner)
    {
        m_spawnerReference = spawner;
    }

    public void SetExplosionTimer(float time)
    {
        m_explosionTime = time;
        m_explosionTimer = time;
    }

    public void SetMaxHealth(float maxHealth)
    {
        m_maxHealth = maxHealth;
    }
}
