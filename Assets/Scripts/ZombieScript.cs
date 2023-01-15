using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    public float followRadius = 10f;

    void Update()
    {
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
