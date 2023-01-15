using TMPro;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    public float followRadius = 10f;
    public float health = 100f;
    public TextMeshPro textMesh;

    void HealthCheck()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        HealthCheck();
        textMesh.text = health.ToString();
        float movementSpeed = Random.Range(1, 5);
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
           
        foreach (GameObject player in allPlayers)
        {
            if(player)
            {
                float distanceFromTarget = Vector3.Distance(transform.position, allPlayers[0].transform.position);
                if (distanceFromTarget <= followRadius)
                {
                    Vector3 targetPosition = allPlayers[0].transform.position;
                    transform.position = Vector3.MoveTowards(
                        transform.position,
                        targetPosition,
                        movementSpeed * Time.deltaTime);
                }
            }
        }
    }
}
