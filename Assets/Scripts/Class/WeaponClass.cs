using System.Diagnostics;

public class WeaponClass : Item
{
    public WeaponType weaponType;
    public WeaponHolding weaponHolding;
    public float reserveAmmo;
    public float startingAmmo;
    public int damage;
    public float magazineSize;
    public float currentBullets;
    public float fireRate;
    public float reloadTime;
    public string shootingSoundPath;
    public string reloadingSoundPath;
    public string weaponIconPath;
    public string weaponPrefabPath;

    public WeaponClass(
        string name, 
        string description,
        ItemType itemType,
        WeaponType weaponType,
        WeaponHolding weaponHolding,
        float reserveAmmo,
        float startingAmmo,
        int damage,
        float magazineSize,
        float currentBullets,
        float fireRate,
        float reloadTime,
        string shootingSoundPath,
        string reloadingSoundPath,
        string weaponIconPath,
        string weaponPrefabPath, 
        float price) : base(name, description, itemType, price)
    {
        this.weaponType = weaponType;
        this.startingAmmo = startingAmmo;
        this.weaponHolding = weaponHolding;
        this.reserveAmmo = reserveAmmo;
        this.damage = damage;
        this.magazineSize = magazineSize;
        this.currentBullets = currentBullets;
        this.fireRate = fireRate;
        this.reloadTime = reloadTime;
        this.shootingSoundPath = shootingSoundPath;
        this.reloadingSoundPath = reloadingSoundPath;
        this.weaponIconPath = weaponIconPath;
        this.weaponPrefabPath = weaponPrefabPath;
        this.price = price;
    }

    public void ResetReserveAmmo()
    {
        this.reserveAmmo = this.startingAmmo;
    }

    public void Reload()
    {
        if(reserveAmmo > 0)
        {
            if(reserveAmmo > magazineSize)
            {
                if (currentBullets > 0)
                {
                    reserveAmmo -= magazineSize - currentBullets;
                    currentBullets = magazineSize;
                    return;
                }
                currentBullets = magazineSize;
                reserveAmmo -= magazineSize;
            } else
            {
                currentBullets = reserveAmmo;
                reserveAmmo -= currentBullets;
            }
        }
    }
}