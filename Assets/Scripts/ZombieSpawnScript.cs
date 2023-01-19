using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ZombieSpawnScript : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public List<GameObject> spawnPads;

    public float spawnRadius = 1000f;
    public float restrictedSpawnRadius = 10f;
    public float minDistanceFromPlayer = 20f;

    public int round = 0;
    public int spawnYAxis = 4;
    public int maxZombies = 0;
    public int remainingZombies;

    public TextMeshProUGUI zombieCounter;
    public TextMeshProUGUI roundCounter;

    public int bloodmoon;

    public Vector3 spawnPos;


    public bool spawning;


    private void Start()
    {
        spawning = true;
        maxZombies = 0;
        round = 0;
        bloodmoon = 7;
        ChangeRound();
    }

    private void Checkspawnpad()
    {
        spawnPads = new List<GameObject>();
        // Get all GameObjects in radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, spawnRadius);

        // Check each GameObject for the "Spawnpad" tag
        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag("Spawnpad") && Vector3.Distance(col.transform.position, transform.position) > restrictedSpawnRadius)
            {
                spawnPads.Add(col.gameObject);
            }
        }
    }

    private void SpawnZombies()
    {
        Checkspawnpad();
        // Spawn the game object
        GameObject spawnPad = spawnPads[Random.Range(0, spawnPads.Count - 1)];
        spawnPos = new Vector3(spawnPad.transform.position.x, spawnPad.transform.position.y - 3, spawnPad.transform.position.z);
        prefabToSpawn.gameObject.GetComponent<ZombieScript>().zombie.health = 100 + (100 * (round / bloodmoon));
        Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
    }

    private void ChangeRound()
    {
        spawning = true;
        round++;
        maxZombies += round * 2;
    }

    private void CheckZombies()
    {
        remainingZombies = GameObject.FindGameObjectsWithTag("Zombie").Length;

        zombieCounter.text = "Remaining Zombies: " + remainingZombies;
        roundCounter.text = "Round: " + round;
        if ((remainingZombies < maxZombies + 1) && spawning) {
            if (remainingZombies >= maxZombies)
            {
                spawning = false;
                return;
            }
            SpawnZombies();
        }

        else if (remainingZombies == 0)
            {
                ChangeRound();
            }
    }

    private void Update()
    {
        CheckZombies();
    }
}
