public class WeaponClass : Item
{
    public WeaponType weaponType;
    public int damage;
    public int magazineSize;
    public float currentBullets;
    public float fireRate;
    public float reloadTime;
    public string shootingSoundPath;
    public string reloadingSoundPath;

    public WeaponClass(
        string name, 
        string description, 
        ItemType itemType, 
        WeaponType weaponType, 
        int damage, 
        int magazineSize, 
        float currentBullets,
        float fireRate, 
        float reloadTime, 
        string shootingSoundPath, 
        string reloadingSoundPath) : base(name, description, itemType)
    {
        this.weaponType = weaponType;
        this.damage = damage;
        this.magazineSize = magazineSize;
        this.currentBullets = currentBullets;
        this.fireRate = fireRate;
        this.reloadTime = reloadTime;
        this.shootingSoundPath = shootingSoundPath;
        this.reloadingSoundPath = reloadingSoundPath;
    }

    public void Reload()
    {
        currentBullets = magazineSize;
    }
}