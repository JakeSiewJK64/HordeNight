using UnityEngine;

public class BulletSpawnScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 10f;
    public float bulletLifetime = 2f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.VelocityChange);
            GetComponent<AudioSource>().Play();
            Destroy(bullet, bulletLifetime);
        }
    }
}
