using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to play the explosion when the enemy robot dies
/// 
/// Code written by Antoine Kenneth Odi in 2017.
/// </summary>

[RequireComponent(typeof(AudioSource))]
public class ExplosionScript : MonoBehaviour {

    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private float explosionTime = 2;
    [SerializeField] private GameObject m_explosionParticle;

    private AudioSource m_explosionAudioSource;

    private float m_timer;

    // Use this for initialization
    void Start() {
        m_explosionAudioSource = gameObject.GetComponent<AudioSource>();
        if (explosionSound != null)
        {
            m_explosionAudioSource.clip = explosionSound;
        }
        m_timer = explosionTime;
    }

    // Update is called once per frame
    void Update () {
        if (!m_explosionAudioSource.isPlaying && explosionSound != null)
        {
            m_explosionAudioSource.Play();
        }
        if (m_timer <= 0)
            Destroy(gameObject);
        else
            m_timer -= Time.deltaTime;
	}
}
