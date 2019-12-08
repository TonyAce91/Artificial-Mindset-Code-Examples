using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class specifies the shoot behaviour of AI
/// 
/// Code written by Antoine Kenneth Odi in 2017.
/// </summary>

public class ShootBehaviour : IBehaviour {

    private float damageTime = 0.1f; // seconds between spawns
    private float damageTimer; // The timer that counts and controls spawning
    private bool bulletDamageSet = false;
    private WeaponType m_weaponType = WeaponType.HitScanType;
    private float m_damage;
    private GameObject m_target;
    private List<GameObject> m_guns;

    // This is used to specify what type of weapon is used
    public enum WeaponType
    {
        HitScanType,
        ProjectileType,
    }


    // Updates the behaviour
    public BehaviourResult UpdateBehaviour(AgentActor agent)
    {
        // Resets the agent's velocity
        agent.Velocity = Vector3.zero;

        // Looks at the target
        agent.gameObject.transform.LookAt(m_target.transform);
        damageTimer -= Time.fixedDeltaTime;

        // This controls the damage per second of the robots
        if (damageTimer < 0)
        {
            // reset the timer
            damageTimer = damageTime;

            if (m_weaponType == WeaponType.HitScanType)
            {
                foreach (GameObject gun in m_guns)
                {
                    // Updates the gun particle system script
                    gun.GetComponent<GunParticleSystem>().UpdateParticleSystems(m_target);
                }
            }

            else if (m_weaponType == WeaponType.ProjectileType)
            {
                foreach (GameObject gun in m_guns)
                {
                    gun.GetComponent<BulletSpawner>().FireAProjectile();
                }
            }
            else
                Debug.Log("Weapon type not recognised");
        }
        return BehaviourResult.SUCCESS;
    }


    // This function is used to set target of the agent
    public void SetTarget(GameObject target)
    {
        m_target = target;
    }

    // This is to specify which child game objects are guns
    public void SetGuns(List<GameObject> guns)
    {
        m_guns = guns;
    }

    // This is used to set the weapon type
    public void SetWeaponType(WeaponType type)
    {
        m_weaponType = type;
    }
}
