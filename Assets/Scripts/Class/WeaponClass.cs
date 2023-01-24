public class WeaponClass : Item
{
    public WeaponType weaponType;
    public WeaponHolding weaponHolding;
    public int damage;
    public int magazineSize;
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
        int damage,
        int magazineSize,
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
        this.weaponHolding = weaponHolding;
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

    public void Reload()
    {
        currentBullets = magazineSize;
    }
}