public class WeaponClass
{
    public WeaponType weaponType;
    public int damage;
    public int magazineSize;
    public string name;
    public float fireRate;
    public float reloadTime;
    public string shootingSoundPath;
    public string reloadingSoundPath;
    public WeaponClass(
        WeaponType weaponType, 
        int damage, 
        int magazineSize, 
        string name, 
        float fireRate, 
        float reloadTime,
        string shootingSoundPath,
        string reloadingSountPath)
    {
        this.weaponType = weaponType;
        this.damage = damage;
        this.magazineSize = magazineSize;
        this.name = name;
        this.fireRate = fireRate;
        this.reloadTime = reloadTime;
        this.shootingSoundPath = shootingSoundPath;
        this.reloadingSoundPath = reloadingSountPath;
    }
}