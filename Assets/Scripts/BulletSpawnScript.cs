using System.IO;
using TMPro;
using UnityEngine;

public class BulletSpawnScript : MonoBehaviour
{
    private string soundFolder = "Raw\\Sound\\";

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

    [SerializeField]
    float spreadAngle = .5f;

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
        reloadSound = Resources.Load<AudioClip>(Path.Combine(soundFolder, currentWeapon.reloadingSoundPath));
        UpdateWeaponSound();
    }

    private void UpdateWeaponSound()
    {
        shootingSound = Resources.Load<AudioClip>(Path.Combine(soundFolder, currentWeapon.shootingSoundPath));
        reloadSound = Resources.Load<AudioClip>(Path.Combine(soundFolder, currentWeapon.reloadingSoundPath));
    }

    private void playWeaponSound(AudioClip clip)
    {
       audioSource.PlayOneShot(clip);
    }

    private void checkReloading()
    {
        if (Input.GetKeyDown(KeyCode.R) && !reloading && currentWeapon.currentBullets != currentWeapon.GetMagazineSize())
        {
            Reload();
        }

        if (Time.time - lastClickTime > currentWeapon.reloadTime - (currentWeapon.reloadTime * (currentWeapon.upgradeStats.reloadSpeed.GetValue() / 100)))
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
            playWeaponSound(reloadSound);        
        }
    }

    private void UpdateBulletCount()
    {
        bulletCounterIndicator.text = currentWeapon.currentBullets + " / " + currentWeapon.reserveAmmo;
    }

    private void SpawnBulletCasing(Rigidbody rb)
    {
        // trigger bullet casing effect
        GameObject bulletCasing = Instantiate(bulletCasingPrefab, bulletCasingSpawn.position, bulletCasingSpawn.rotation);
        rb.AddForce(bulletCasingPrefab.transform.forward);
        Destroy(bulletCasing, bulletLifetime);
    }

    private void Shoot()
    {
        if (Input.GetMouseButton(0) && !reloading)
        {
            if (Time.time - lastClickTime > currentWeapon.fireRate - (currentWeapon.upgradeStats.fireRate.GetValue() / 100) * currentWeapon.fireRate)
            {
                currentWeapon.currentBullets--;
                lastClickTime = Time.time;

                bulletPrefab.GetComponent<BulletScript>().damage = 
                    (float)(currentWeapon.damage + (currentWeapon.damage * (int) currentWeapon.weaponType * currentWeapon.upgradeStats.damage.GetValue()));

                // play shoot sound
                playWeaponSound(shootingSound);


                GameObject bullet;
                Rigidbody rb;
                if (currentWeapon.weaponType != WeaponType.Shotgun)
                {
                    bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
                    bullet.GetComponent<BulletScript>().SetOriginPlayer(gameObject);
                    rb = bullet.GetComponent<Rigidbody>();
                    rb.AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.VelocityChange);
                    SpawnBulletCasing(rb);
                    Destroy(bullet, bulletLifetime);
                }
                else
                {
                    for (int i = 0; i < 12; i++)
                    {
                        bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
                        bullet.GetComponent<BulletScript>().SetOriginPlayer(gameObject);

                        // setting the pellet spread
                        Vector3 spread = new Vector3(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle));
                        
                        bullet.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + spread);
                        rb = bullet.GetComponent<Rigidbody>();
                        rb.AddForce(transform.forward * bulletSpeed * 50);
                        Destroy(bullet, bulletLifetime);
                    }
                }
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
