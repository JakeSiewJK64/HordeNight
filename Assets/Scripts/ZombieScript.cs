using System.Collections;
using TMPro;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    public Zombie zombie;
    private GameObject player;
    private Animator zombieController;

    [SerializeField]
    private float followRadius = 500f;

    [SerializeField]
    private TextMeshPro textMesh;

    private void Start()
    {
        zombieController = GetComponentInChildren<Animator>();
        player = GetPlayer();
        float health = 100 + 100 * (player.GetComponent<ZombiesKillCounterScript>().round / player.GetComponent<ZombiesKillCounterScript>().bloodmoon);
        float damage = .5f + .5f * (player.GetComponent<ZombiesKillCounterScript>().round / player.GetComponent<ZombiesKillCounterScript>().bloodmoon);

        zombie = new Zombie((float)Random.Range(0.5f, 3), health, damage);
    }

    private void HealthCheck()
    {
        if (zombie.health <= 0)
        {
            Destroy(gameObject);
            player.GetComponent<ZombiesKillCounterScript>().IncrementCounter();
        }
    }

    private GameObject GetPlayer()
    {
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in allPlayers)
        {
            if (player)
            {
                return player;                
            }
        }
        return null;
    }

    private void Update()
    {

        textMesh.text = zombie.health.ToString();

        HealthCheck();
        player = GetPlayer();

        float distanceFromTarget = Vector3.Distance(transform.position, player.transform.position);
        if (distanceFromTarget <= followRadius)
        {
            Vector3 targetPosition = player.transform.position;
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                2 * Time.deltaTime);
            transform.LookAt(player.transform);
        }
    }

    private void TakePlayerDamage(Collision collision)
    {
        try
        {
            if (collision.gameObject.tag == "Player" && collision != null && gameObject != null)
            {
                zombieController.SetBool("Attacking", true);
                if (collision.gameObject.GetComponent<PlayerHealthScript>().player.health > 0)
                {
                    collision.gameObject.GetComponent<PlayerHealthScript>().player.TakeDamage(zombie.damage);
                    return;
                }
            } else
            {
                zombieController.SetBool("Attacking", false);
            }
        } catch { }
    }

    private void OnCollisionStay(Collision collision)
    {
        TakePlayerDamage(collision);
    }
}
