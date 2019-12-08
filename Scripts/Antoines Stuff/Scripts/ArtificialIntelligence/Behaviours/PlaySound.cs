using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This behaviour is used to play sound
/// 
/// Code written by Antoine Kenneth Odi in 2017.
/// </summary>

public class PlaySound : IBehaviour
{
    private AudioSource m_source;
    private AudioClip m_clip;
    private float m_time = 100.0f;
    private float m_timer = 0.0f;
    private float m_clipLength;

    // Updates the behaviour
    public BehaviourResult UpdateBehaviour(AgentActor agent)
    {
        // Return error if no sound clip or sound source
        if (m_source == null || m_clip == null)
        {
            return BehaviourResult.ERROR;
        }

        if (!m_source.isPlaying && m_timer <= 0)
        {
            m_source.clip = m_clip;
            m_source.Play();
            m_timer = m_time;
        }

        // Timer makes sure robot don't use the same speech over and over too quickly during one state
        m_timer -= Time.deltaTime;

        return BehaviourResult.SUCCESS;
    }
    
    // Sets the audio source where the clip is going to be played
    public void SetAudioSource (AudioSource source)
    {
        m_source = source;
    }

    // Sets the audio clip that will be played
    public void SetAudioClip (AudioClip soundClip)
    {
        m_clip = soundClip;
    }

    // Sets timer between each audio clip so it doesn't crowd the player's speaker
    public void SetTimer (float time)
    {
        m_time = time;
    }

}
