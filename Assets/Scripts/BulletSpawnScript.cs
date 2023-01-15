using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnScript : MonoBehaviour
{
    [SerializeField]
    private float bulletCount;

    // for bullet casing effect
    public GameObject bulletCasingPrefab;
    public Transform bulletCasingSpawn;

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
            new WeaponClass(weaponType: WeaponType.Sidearm, 10, 8, "glock", .2f, 3f));
        currentWeapon = globalWeaponList[0];
        bulletCount = currentWeapon.magazineSize;
    }

    void Update()
    {
        // check ammo count
        if (bulletCount == 0)
        {
            if (Time.time - lastClickTime >= currentWeapon.reloadTime)
            {
                bulletCount = currentWeapon.magazineSize;
            }
        } else if (Input.GetMouseButtonDown(0)) { 
            bulletCount -= 1;
            if(Time.time - lastClickTime > currentWeapon.fireRate)
            {
                lastClickTime = Time.time;
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.VelocityChange);
                GetComponent<AudioSource>().Play();

                // trigger bullet casing effect
                GameObject bulletCasing = Instantiate(bulletCasingPrefab, bulletCasingSpawn.position, bulletCasingSpawn.rotation);
                rb.AddForce(bulletCasingPrefab.transform.forward);
                Destroy(bullet, bulletLifetime);
                Destroy(bulletCasing, bulletLifetime);
            }
        }
    }
}
