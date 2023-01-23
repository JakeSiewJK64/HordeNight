using System.Collections;
using TMPro;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    public Zombie zombie = new Zombie(100, .125f);
    public GameObject player;

    [SerializeField]
    private float followRadius = 500f;

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
        float movementSpeed = Random.Range(0.5f, 3);
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
                    transform.LookAt(allPlayers[0].transform);
                }
            }
        }
    }

    private void TakePlayerDamage(Collision collision)
    {
        try
        {
            if (collision.gameObject.tag == "Player" && collision != null && gameObject != null)
            {
                if (collision.gameObject.GetComponent<PlayerHealthScript>().player.health > 0)
                {
                    collision.gameObject.GetComponent<PlayerHealthScript>().player.TakeDamage(zombie.damage);
                }
            }
        } catch { }
    }

    private void OnCollisionEnter(Collision collision)
    {
        TakePlayerDamage(collision);
        StartCoroutine(DelayDamage(collision));
    }

    private void OnCollisionStay(Collision collision)
    {
        TakePlayerDamage(collision);
        StartCoroutine(DelayDamage(collision));
    }

    public IEnumerator DelayDamage(Collision collision)
    {
        yield return new WaitForSeconds(2f);
        TakePlayerDamage(collision);
    }
}
