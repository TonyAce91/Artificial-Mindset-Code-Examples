using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is used to control bullet projectile spawns
/// 
/// Code written by Antoine Kenneth Odi in 2017.
/// </summary>

public class BulletSpawner : MonoBehaviour {

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float speed = 10;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float lifetime = 2;
    [SerializeField] private int damage = 1;


    private Vector3 spawnPoint;
    
    // This is used to fire a bullet using prefab by instantiating the bullet with parameters
    public void FireAProjectile()
    {
        //Debug.Log("Fire a bullet called");
        if (bulletPrefab == null)
            Debug.Log("prefab not found");
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetBulletParameters(speed, transform.forward, lifetime, damage);
    }
}
