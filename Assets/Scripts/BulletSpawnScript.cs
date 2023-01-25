using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;

public class BulletSpawnScript : MonoBehaviour
{
    private string soundFolder = "Assets/Raw/Sound/";

    // for bullet casing effect
    public GameObject bulletCasingPrefab;
    public Transform bulletCasingSpawn;

    public TextMeshProUGUI bulletCounterIndicator;
    public TextMeshProUGUI weaponTypeIndicator;

    // actual weapon effect
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 10f;
    public float bulletLifetime = 2f;
    public float lastClickTime;

    private WeaponClass currentWeapon;
    private AudioSource audioSource;

    private AudioClip reloadSound;
    private AudioClip shootingSound;

    public bool reloading = false;

    public Inventory inventory;

    public void ChangeWeapon(WeaponClass newWeapon)
    {
        currentWeapon = newWeapon;
        reloading = false;
        weaponTypeIndicator.text = currentWeapon.name;
        UpdateWeaponSound();

        if(currentWeapon.weaponHolding == WeaponHolding.SECONDARY)
        {
            GetComponent<PlayerWeaponModelScript>().UpdateSecondary(currentWeapon.weaponPrefabPath);
        } else
        {
            GetComponent<PlayerWeaponModelScript>().UpdatePrimary(currentWeapon.weaponPrefabPath);
        }
    }  

    public WeaponClass GetCurrentWeapon()
    {
        return currentWeapon;
    }

    private void Start()
    {
        currentWeapon = gameObject.GetComponent<PlayerInventoryScript>().GetCurrentWeapon();
        weaponTypeIndicator.text = currentWeapon.name;

        // initialize audio
        audioSource = GetComponent<AudioSource>();
        reloadSound = AssetDatabase.LoadAssetAtPath<AudioClip>(Path.Combine(soundFolder, currentWeapon.reloadingSoundPath));
        UpdateWeaponSound();
    }

    private void UpdateWeaponSound()
    {
        shootingSound = AssetDatabase.LoadAssetAtPath<AudioClip>(Path.Combine(soundFolder, currentWeapon.shootingSoundPath));
        reloadSound = AssetDatabase.LoadAssetAtPath<AudioClip>(Path.Combine(soundFolder, currentWeapon.reloadingSoundPath));
    }

    private void playWeaponSound(AudioClip clip)
    {
       audioSource.PlayOneShot(clip);
    }

    private void checkReloading()
    {
        if (Input.GetKeyDown(KeyCode.R) && !reloading && currentWeapon.currentBullets != currentWeapon.magazineSize)
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
        if(currentWeapon.reserveAmmo > 0)
        {
            reloading = true;
            currentWeapon.Reload();
            Debug.Log(currentWeapon.reserveAmmo);
            Debug.Log(currentWeapon.currentBullets);
            playWeaponSound(reloadSound);        
        }
    }

    private void UpdateBulletCount()
    {
        bulletCounterIndicator.text = currentWeapon.currentBullets + " / " + currentWeapon.reserveAmmo;
    }

    private void Shoot()
    {
        if (Input.GetMouseButton(0) && !reloading)
        {
            if (Time.time - lastClickTime > currentWeapon.fireRate)
            {
                currentWeapon.currentBullets--;
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
        if (!GetComponent<BuyStationScript>().interacting && Time.timeScale == 1.0f)
        {
            UpdateBulletCount();
            checkReloading();      
            if (currentWeapon.currentBullets == 0)
            {
                Reload();
            }
            else
            {
                Shoot();
            }
        }
    }
}
