using System.IO;
using UnityEditor;
using UnityEngine;

public class BulletSpawnScript : MonoBehaviour
{
    public float bulletCount;

    public string soundFolder = "Assets/Raw/";

    // for bullet casing effect
    public GameObject bulletCasingPrefab;
    public Transform bulletCasingSpawn;

    // actual weapon effect
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 10f;
    public float bulletLifetime = 2f;
    public float lastClickTime;

    public WeaponClass currentWeapon;
    private AudioSource audioSource;

    private AudioClip reloadSound;
    private AudioClip shootingSound;

    public bool reloading = false;

    public Inventory inventory;

    public void ChangeWeapon(WeaponClass newWeapon)
    {
        currentWeapon = newWeapon;
        bulletCount = newWeapon.magazineSize;
        reloading = false;
    }  

    private void Start()
    {
        currentWeapon = gameObject.GetComponent<PlayerInventoryScript>().GetCurrentWeapon();
        bulletCount = currentWeapon.magazineSize;

        // initialize audio
        audioSource = GetComponent<AudioSource>();
        reloadSound = AssetDatabase.LoadAssetAtPath<AudioClip>(Path.Combine("Assets/Raw/", currentWeapon.reloadingSoundPath));
        shootingSound = AssetDatabase.LoadAssetAtPath<AudioClip>(Path.Combine("Assets/Raw/", currentWeapon.shootingSoundPath));
    }

    private void playWeaponSound(AudioClip clip)
    {
       audioSource.PlayOneShot(clip);
    }

    private void checkReloading()
    {
        if (Input.GetKeyDown(KeyCode.R) && !reloading && bulletCount != currentWeapon.magazineSize)
        {
            Reload();
        }

        if (Time.time - lastClickTime > currentWeapon.reloadTime)
        {
            reloading = false;
        }
    }

    private void Reload()
    {
        reloading = true;
        bulletCount = currentWeapon.magazineSize;
        playWeaponSound(reloadSound);        
    }

    private void Shoot()
    {
        if (Input.GetMouseButton(0) && !reloading)
        {
            if (Time.time - lastClickTime > currentWeapon.fireRate)
            {
                bulletCount--;
                lastClickTime = Time.time;
                bulletPrefab.GetComponent<BulletScript>().damage = currentWeapon.damage * (float) currentWeapon.weaponType;
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.VelocityChange);

                // play shoot sound
                playWeaponSound(shootingSound);

                // trigger bullet casing effect
                GameObject bulletCasing = Instantiate(bulletCasingPrefab, bulletCasingSpawn.position, bulletCasingSpawn.rotation);
                rb.AddForce(bulletCasingPrefab.transform.forward);
                Destroy(bullet, bulletLifetime);
                Destroy(bulletCasing, bulletLifetime);
            }
        }
    }

    private void Update()
    {
        checkReloading();        
        if (bulletCount == 0)
        {
            Reload();
        } else
        {
            Shoot();
        }
    }
}
