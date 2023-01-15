using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float damage = 0;

    private Dictionary<string, System.Action<Collision>> actions 
        = new Dictionary<string, System.Action<Collision>>();
    
    private void Start()
    {
        actions.Add("Zombie", HandleZombies);
        actions.Add("Environment", HandleEnvironment);
    }

    private void HandleEnvironment(Collision obj)
    {
        Debug.Log("hit environment!");
        Destroy(gameObject);
    }

    private void HandleZombies(Collision obj)
    {
        // destroy the bullet on impact
        Destroy(gameObject);
        ZombieScript zombie =  obj.gameObject.GetComponent<ZombieScript>();
        zombie.health -= damage;
        Debug.Log(zombie);
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
