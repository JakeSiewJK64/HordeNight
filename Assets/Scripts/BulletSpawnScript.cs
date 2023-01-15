using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class BulletSpawnScript : MonoBehaviour
{
    [SerializeField]
    private float bulletCount;

    public string soundFolder = "Assets/Raw";

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
    private AudioSource audioSource;

    private AudioClip reloadSound;
    private AudioClip shootingSound;

    public bool reloading = false;

    void Start()
    {
        globalWeaponList.Add(
            new WeaponClass(WeaponType.Sidearm, 10, 8, "glock", .2f, 3f, "glock.mp3", "glock_reload.mp3"));
        currentWeapon = globalWeaponList[0];
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
        if (Time.time - lastClickTime > currentWeapon.reloadTime)
        {
            reloading = false;
        }
    }

    void Reload()
    {
        bulletCount = currentWeapon.magazineSize;
        this.playWeaponSound(reloadSound);        
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && !reloading)
        {
            if (Time.time - lastClickTime > currentWeapon.fireRate)
            {
                bulletCount -= 1;
                lastClickTime = Time.time;
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.VelocityChange);

                // play shoot sound
                this.playWeaponSound(shootingSound);

                // trigger bullet casing effect
                GameObject bulletCasing = Instantiate(bulletCasingPrefab, bulletCasingSpawn.position, bulletCasingSpawn.rotation);
                rb.AddForce(bulletCasingPrefab.transform.forward);
                Destroy(bullet, bulletLifetime);
                Destroy(bulletCasing, bulletLifetime);
            }
        }
    }

    void Update()
    {
        this.checkReloading();
        if (bulletCount == 0)
        {
            this.reloading = true;
            this.Reload();
        } else
        {
            this.Shoot();
        }
    }
}
