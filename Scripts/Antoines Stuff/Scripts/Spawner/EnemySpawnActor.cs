using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This was used for testing enemy spawning and how the behave
/// 
/// Code written by Antoine Kenneth Odi in 2017.
/// </summary>

public class EnemySpawnActor : MonoBehaviour {

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnTime = 5.0f; // seconds between spawns
    [SerializeField] private float spawnRadius = 10.0f; // distance from player to spawn
    [SerializeField] private float spawnHeight = 10.0f;
    [SerializeField] private int maximumNumberOfSpawns = 100;
    private FPSController player;
    private int numberOfSpawns = 0;
    //private PlayerActor player;
    private float spawnTimer; // The timer that counts and controls spawning


	// Use this for initialization
	void Start () {
        spawnTimer = spawnTime;
        player = GameObject.FindObjectOfType<FPSController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        // Count our timer down each frame
        spawnTimer -= Time.fixedDeltaTime;

        if (spawnTimer < 0 && numberOfSpawns < maximumNumberOfSpawns)
        {

            // reset the timer
            spawnTimer = spawnTime;

            // Pick a random angle in radians and set the spawn point
            float spawn_angle = Random.Range(0, 2 * Mathf.PI);

            Vector3 spawn_direction = new Vector3(Mathf.Sin(spawn_angle), 0, Mathf.Cos(spawn_angle));
            spawn_direction *= spawnRadius;

            Vector3 spawn_point = player.transform.position + spawn_direction;
            spawn_point.y = spawnHeight;

            // Spawn the enemy at the desired location
            Instantiate(enemyPrefab, spawn_point, Quaternion.identity);
            numberOfSpawns++;
            
        }
	}
}
