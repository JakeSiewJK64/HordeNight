using System.Collections;
using TMPro;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    public Zombie zombie = new Zombie(100, .125f);

    [SerializeField]
    private float followRadius = 10f;

    [SerializeField]
    private TextMeshPro textMesh;

    private void HealthCheck()
    {
        if(zombie.health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        HealthCheck();
        textMesh.text = zombie.health.ToString();
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
    private void TakePlayerDamage(Collision collision)
    {
        StartCoroutine(DelayDamage(collision));
        if(collision.gameObject.tag == "Player")
        {
            if(collision.gameObject.GetComponent<PlayerHealthScript>().player.health > 0)
            {
                collision.gameObject.GetComponent<PlayerHealthScript>().player.TakeDamage(zombie.damage);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        TakePlayerDamage(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        TakePlayerDamage(collision);
    }

    public IEnumerator DelayDamage(Collision collision)
    {
        yield return new WaitForSeconds(2f);
        TakePlayerDamage(collision);
    }
}
