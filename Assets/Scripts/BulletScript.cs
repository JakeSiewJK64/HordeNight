using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Dictionary<string, System.Action<Collision>> actions 
        = new Dictionary<string, System.Action<Collision>>();
    
    private void Start()
    {
        actions.Add("Zombie", HandleZombies);
        actions.Add("Environment", HandleEnvironment);
    }

    private void HandleEnvironment(Collision obj)
    {
    }

    private void HandleZombies(Collision obj)
    {
        Destroy(obj.gameObject);
        Debug.Log("Zombie Hit");
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
