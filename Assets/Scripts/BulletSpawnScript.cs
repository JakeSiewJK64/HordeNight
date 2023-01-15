using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnScript : MonoBehaviour
{
    // for bullet casing effect
    public GameObject bulletCasingPrefab;
    public Transform bulletCasingSpawn;
    public float bulletCasingLifetime = 2f;

    // actual weapon effect
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 10f;
    public float bulletLifetime = 2f;
    public float lastClickTime;

    public List<WeaponClass> globalWeaponList = new List<WeaponClass>();
    public WeaponClass currentWeapon;

    void Start()
    {
        globalWeaponList.Add(
            new WeaponClass(weaponType: WeaponType.Sidearm, 10, 8, "glock", 1f));
        currentWeapon = globalWeaponList[0];
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(Time.time - lastClickTime > currentWeapon.fireRate)
            {
                lastClickTime = Time.time;
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.VelocityChange);
                GetComponent<AudioSource>().Play();
                Destroy(bullet, bulletLifetime);

                // trigger bullet casing effect
                GameObject bulletCasing = Instantiate(bulletCasingPrefab, bulletCasingSpawn.position, bulletCasingSpawn.rotation);
                Rigidbody bulletCasingrb = bullet.GetComponent<Rigidbody>();
                rb.AddForce(bulletCasingPrefab.transform.forward);
                Destroy(bullet, bulletCasingLifetime);
            }
        }
    }
}
