using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

/// <summary>
/// This is a superclass for all agents (or AI) in the game
/// This is a monoBehaviour subclass to allow all its subclass to be attachable to game objects in inspector
/// Code written by Antoine Kenneth Odi in 2017.
/// </summary>
public class AgentActor : MonoBehaviour {

    // When we want to make a prefab, we can't reference any specific object in a scene
    // This is because it could be used in multiple scenes

    // A list of behaviours that the agent will have
    protected List<IBehaviour> m_behaviours;

    // Physics
    protected Vector3 m_velocity;
    protected Vector3 m_acceleration;
    protected Vector3 m_force;

    // Current health of agent
    protected float currentHealth;

    // Maximum speed of agent
    public float MaxSpeed = 10.0f;

    // Updates behaviour and physics of agent
    protected void UpdateBehaviours()
    { 
        // Resets all the forces on the agent
        m_force = Vector3.zero;

        // Updates all behaviours of the agent
        foreach (IBehaviour behaviour in m_behaviours)
        {
            behaviour.UpdateBehaviour(this);
        }

        // Calculates resultant velocity due to forces
        m_velocity += m_force * Time.fixedDeltaTime;
        
        // Moves the agent in relation to the velocity on them
        transform.position += m_velocity * Time.fixedDeltaTime;
    }

    // Use to add forces applied on the agent
    public void AddForce(Vector3 force)
    {
        m_force += force;
    }
    

    //-----------------------------------------------------------
    // Properties

    // Velocity property
    public Vector3 Velocity
    {
        get
        {
            return m_velocity;
        }
        set
        {
            if (value != null)
                m_velocity = value;
        }
    }


    // Current health property
    public float CurrentHealth
    {
        get
        {
            return currentHealth;
        }
        set
        {
            currentHealth = value;
        }
    }
}
