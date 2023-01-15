using UnityEngine;

public class ZombieSpawnScript : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public float spawnRadius = 5f;
    public float spawnInterval = 60f;
    public float minDistanceFromPlayer = 5f;

    private GameObject player;
    private float timeUntilNextSpawn;

    private void Start()
    {
        // Initialize the time until the next spawn
        timeUntilNextSpawn = spawnInterval;
        player = gameObject;
    }

    private void Update()
    {
        // Decrement the time until the next spawn
        timeUntilNextSpawn -= Time.deltaTime;

        // Check if it's time to spawn
        if (timeUntilNextSpawn <= 0)
        {
            // Spawn the game object
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
            spawnPosition.y = 1;
            
            float distanceFromPlayer = Vector3.Distance(spawnPosition, player.transform.position);
            
            if(distanceFromPlayer < minDistanceFromPlayer)
            {
                timeUntilNextSpawn = spawnInterval;
                return;
            }

            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
            timeUntilNextSpawn = spawnInterval;
        }
    }
}
