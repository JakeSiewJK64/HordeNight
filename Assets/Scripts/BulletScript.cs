using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float damage;
    public ZombieScript zombieScript;

    private GameObject player;

    private Dictionary<string, System.Action<Collision>> actions
        = new Dictionary<string, System.Action<Collision>>();

    private void Start()
    {
        actions.Add("Zombie", HandleZombies);
        actions.Add("Environment", HandleEnvironment);
    }

    public void SetOriginPlayer(GameObject player)
    {
        this.player = player;
    }

    private void HandleEnvironment(Collision obj)
    {
        Destroy(gameObject);
    }

    private void HandleZombies(Collision obj)
    {
        zombieScript =  obj.gameObject.GetComponent<ZombieScript>();
        zombieScript.zombie.health -= damage;
        Destroy(gameObject);

        if(player)
        {
            player.GetComponent<PlayerPointScript>().IncrementPoints(10);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        if(actions.ContainsKey(tag))
        {
            actions[tag](collision);
        }
    }
}
