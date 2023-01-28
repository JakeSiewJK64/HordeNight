using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    public Zombie zombie;
    public GameObject player;
    private Animator zombieController;
    private ZombiesKillCounterScript counter;
    private PlayerPointScript playerPoints;
    private float followRadius = 1000f;

    private void Start()
    {
        zombieController = GetComponentInChildren<Animator>();
        InitializeZombie();
        player = GetPlayer();
    }

    private void InitializeZombie()
    {
        zombie = new Zombie(
            (float)Random.Range(0.5f, 6), 
            100 + 100 * (player.GetComponent<ZombiesKillCounterScript>().GetRound() / player.GetComponent<ZombiesKillCounterScript>().GetBloodMoon()),
            .5f + .5f * (player.GetComponent<ZombiesKillCounterScript>().GetRound() / player.GetComponent<ZombiesKillCounterScript>().GetBloodMoon())
        );
    }

    private GameObject GetPlayer()
    {
       return player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
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
        CheckHealth();
    }

    private void TakePlayerDamage(Collision collision)
    {
        try
        {
            if (collision.gameObject.tag == "Player" && collision != null && gameObject != null)
            {
                zombieController.SetBool("Attacking", true);
                collision.gameObject.GetComponent<PlayerHealthScript>().TakeDamage(zombie.damage);
                return;
            } else
            {
                zombieController.SetBool("Attacking", false);
            }
        } catch { }
    }

    void CheckHealth()
    {
        if (zombie.health <= 0)
        {
            counter.IncrementCounter();
            playerPoints.IncrementPoints(100f);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            zombie.health -= collision.gameObject.GetComponent<BulletScript>().damage;
            counter = collision.gameObject.GetComponent<BulletScript>().GetPlayer().GetComponent<ZombiesKillCounterScript>();
            playerPoints = collision.gameObject.GetComponent<BulletScript>().GetPlayer().GetComponent<PlayerPointScript>();
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        TakePlayerDamage(collision);
    }
}
