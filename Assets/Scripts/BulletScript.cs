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
        actions.Add("Environment", HandleEnvironment);
        actions.Add("Zombie", HandleZombie);
    }

    private void HandleZombie(Collision obj)
    {
        if(obj.gameObject)
        {
            player.GetComponent<PlayerZombiehealthIndicatorScript>().SetZombie(
                obj.gameObject.GetComponent<ZombieScript>().zombie.zombieType.ToString(), 
                obj.gameObject.GetComponent<ZombieScript>().zombie.health);
            player.GetComponent<PlayerPointScript>().IncrementPoints(10);
        }
    }

    public void SetOriginPlayer(GameObject player)
    {
        this.player = player;
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    private void HandleEnvironment(Collision obj)
    {
        Destroy(gameObject);
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
