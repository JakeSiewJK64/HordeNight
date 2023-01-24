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

    public int spawnYAxis = 4;
    
    public int remainingZombies;

    public TextMeshProUGUI zombieCounter;
    public TextMeshProUGUI roundCounter;

    public Vector3 spawnPos;

    private float lastZombieSpawnTime;

    private float spawnDelay = 2f;
    private void Checkspawnpad()
    {
        spawnPads = new List<GameObject>();
        // Get all GameObjects in radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, spawnRadius);

        // Check each GameObject for the "Spawnpad" tag
        foreach (Collider col in hitColliders)
        {
            // if (col.CompareTag("Spawnpad") && Vector3.Distance(col.transform.position, transform.position) > restrictedSpawnRadius)
            if (col.CompareTag("Spawnpad"))
            {
                spawnPads.Add(col.gameObject);
            }
        }
    }

    private void SpawnZombies()
    {
        if(Time.time - lastZombieSpawnTime >= spawnDelay)
        {
            Checkspawnpad();
            GameObject spawnPad = spawnPads[Random.Range(0, spawnPads.Count - 1)];
            spawnPos = new Vector3(spawnPad.transform.position.x, spawnPad.transform.position.y + 3, spawnPad.transform.position.z);
            Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
            lastZombieSpawnTime = Time.time;
        }
    }

    private void CheckZombies()
    {
        remainingZombies = GameObject.FindGameObjectsWithTag("Zombie").Length;

        zombieCounter.text = "Remaining Zombies: " + remainingZombies;
        roundCounter.text = "Round: " + GetComponent<ZombiesKillCounterScript>().round;

        int zombiesKilled = GetComponent<ZombiesKillCounterScript>().GetZombiesKilled();
        int maxZombies = GetComponent<ZombiesKillCounterScript>().GetMaxZombies();

        if (remainingZombies <= maxZombies && remainingZombies + zombiesKilled == maxZombies) 
        {
            if(remainingZombies == 0)
            {
                GetComponent<ZombiesKillCounterScript>().ChangeRound();
            }
        } else
        {
            SpawnZombies();
        }

    }
    
    private void Update()
    {
        CheckZombies();
    }
}
