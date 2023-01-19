using UnityEngine;

public class KillZoneScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);       
    }

    private void OnCollisionStay(Collision collision)
    {
        Destroy(collision.gameObject);
    }
}
