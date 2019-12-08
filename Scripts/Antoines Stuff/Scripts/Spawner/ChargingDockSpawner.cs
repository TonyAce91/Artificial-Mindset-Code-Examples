using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is used to spawn the robots in game
/// This uses object pooling to reduce the amount of instantiation and destruction of objects and free up memory
/// Code written by Antoine Kenneth Odi in 2017.
/// </summary>

public class ChargingDockSpawner : MonoBehaviour
{

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnTime = 5.0f; // seconds between spawns
    [SerializeField] private int maximumNumberOfSpawns = 2;

    private FPSController player;
    private Vector3 playerPosition;
    private int numberOfSpawns = 0;

    private float spawnTimer; // The timer that counts and controls spawning
    public Vector3 patrolPointA = new Vector3(20, 3, 12);
    public Vector3 patrolPointB = new Vector3(-20, 3, 12);

    // This is a list of robot instances that can be turned on and off
    // This is used for object pooling
    private List<GameObject> robotInstance = new List<GameObject>();

    private float rangeFromPlayer = 10.0f;

    // This initialises the spawners
    void Start()
    {
        spawnTimer = spawnTime;
        player = GameObject.FindObjectOfType<FPSController>();
        playerPosition = player.transform.position;
        
        // This creates and set game objects inactive
        // This is for object pooling purposes
        for(int i = 0; i < maximumNumberOfSpawns; i++)
        {
            GameObject drone = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            drone.GetComponent<EnemyRobot>().SetPatrolPoints(patrolPointA, patrolPointB);
            drone.GetComponent<EnemyRobot>().SpawnerReference = this;
            drone.SetActive(false);
            robotInstance.Add(drone);
        }

    }

    // This updates the spawners specifying when and where to spawn robots
    void FixedUpdate()
    {
        // Count our timer down each frame
        if (numberOfSpawns < maximumNumberOfSpawns)
        {
            spawnTimer -= Time.fixedDeltaTime;
        }

        if (spawnTimer <= 0)
        {
            if ((transform.position - playerPosition).sqrMagnitude > rangeFromPlayer * rangeFromPlayer)
            {
                // reset the timer
                spawnTimer = spawnTime;

                foreach(GameObject robot in robotInstance)
                {
                    if (!robot.activeSelf)
                    {
                        robot.transform.position = gameObject.transform.position;
                        robot.SetActive(true);
                        numberOfSpawns++;
                        break;
                    }
                }
            }
        }
    }

    // This is used by the robots to tell the spawner that a robot die and ready to be respawn in the dock again
    public void RobotDied()
    {
        numberOfSpawns--;
    }

}
