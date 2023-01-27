using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    public Zombie zombie;
    public GameObject player;
    private Animator zombieController;

    [SerializeField]
    private float followRadius = 500f;

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
