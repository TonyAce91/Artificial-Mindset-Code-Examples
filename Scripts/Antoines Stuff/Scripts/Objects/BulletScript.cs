using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is used to control the gun projectile if projectile is used
/// 
/// Code written by Antoine Kenneth Odi in 2017.
/// </summary>

public class BulletScript : MonoBehaviour {

    private float m_speed = 10;
    private Vector3 m_direction;
    private float m_lifetime = 2;
    private int m_damage;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += m_direction.normalized * m_speed * Time.deltaTime;
        m_lifetime -= Time.deltaTime;
        if (m_lifetime < 0)
        {
            Destroy(gameObject);
        }
    }

    // This check if the player is hit and apply damage if so
    void OnTriggerEnter(Collider hit)
    {
        GameObject objectHit = hit.gameObject;
        if (objectHit.tag == "Player")
        {
            //Debug.Log("Player hit recognised");
            objectHit.GetComponent<FPSController>().ApplyDamage(m_damage);

        }
        if (objectHit.layer != 9)
            Destroy(gameObject);
    }

    // This sets the parameters of the projectile
    public void SetBulletParameters (float speed, Vector3 direction, float lifetime, int damage)
    {
        m_speed = speed;
        m_direction = direction;
        m_lifetime = lifetime;
        m_damage = damage;
    }
}
