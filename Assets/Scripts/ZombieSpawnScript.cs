using TMPro;
using UnityEngine;

public class ZombieSpawnScript : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public float spawnRadius = 20f;
    public float minDistanceFromPlayer = 20f;

    public int round = 0;
    public int maxZombies = 0;
    public int remainingZombies;

    public TextMeshProUGUI zombieCounter;
    public TextMeshProUGUI roundCounter;

    public bool spawning = true;

    public int bloodmoon;

    private void Start()
    {
        maxZombies = 0;
        round = 0;
        bloodmoon = 7;
        ChangeRound();    
    }

    private void SpawnZombies()
    {
        // Spawn the game object
        Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
        spawnPosition.y = 1;
        prefabToSpawn.gameObject.GetComponent<ZombieScript>().health = 100 + (100 * (round / bloodmoon));
        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    }

    private void ChangeRound()
    {
        round++;
        maxZombies += round * 2;
        spawning = true;
    }

    private void Update()
    {
        remainingZombies = GameObject.FindGameObjectsWithTag("Zombie").Length;

        zombieCounter.text = "Remaining Zombies: " + remainingZombies;
        roundCounter.text = "Round: " + round;

        if((remainingZombies < maxZombies + 1) && spawning)
        {
            if(remainingZombies >= maxZombies) {
                spawning = false;
                return;
            }
            SpawnZombies();
        } else if (remainingZombies == 0)
        {
            ChangeRound();
        }
    }
}
