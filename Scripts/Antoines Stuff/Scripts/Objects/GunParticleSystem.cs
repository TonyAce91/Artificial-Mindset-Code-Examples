using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is used to control the gun particle system used by AI
/// 
/// Code written by Antoine Kenneth Odi in 2017.
/// </summary>

public class GunParticleSystem : MonoBehaviour {

    [SerializeField] private float m_damage = 1;
    [SerializeField] private float raycastDistance = 1000;
    [SerializeField] private ParticleSystem m_muzzleParticleSystem;
    [SerializeField] private ParticleSystem m_collisionParticleSystem;
    [SerializeField] private ParticleSystem m_bulletTracer;

    // This is used to update the particle system without being called every frame
    public void UpdateParticleSystems(GameObject target)
    {
        // This is used to play the bullet tracer if a reference to it exists
        if (m_bulletTracer != null)
        {
            m_bulletTracer.Play();
        }

        // This turns on the muzzle particle system if a reference to it exists
        if (m_muzzleParticleSystem != null)
        {
            m_muzzleParticleSystem.Play();

            // This is used to control the light flickering in accordance to the muzzle particle system
            m_muzzleParticleSystem.gameObject.GetComponentInChildren<LightEmission>().enabled = true;
            m_muzzleParticleSystem.gameObject.GetComponentInChildren<LightEmission>().ResetFlicker();
        }

        // This is used to fire at the enemy by using the direction the guns are facing
        Vector3 fireDirection = transform.forward;
        RaycastHit info;
        Ray fireRay = new Ray(transform.position, fireDirection);

        if (Physics.Raycast(fireRay, out info, raycastDistance))
        {
            // This applies damage to player if player is hit
            if (info.collider.tag == target.tag)
            {
                info.collider.gameObject.GetComponent<FPSController>().ApplyDamage(m_damage);
            }
        }

    }
}
