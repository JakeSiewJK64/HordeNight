public class WeaponClass
{
    public WeaponType weaponType;
    public int damage;
    public int magazineSize;
    public string name;
    public float fireRate;
    public float reloadTime;
    public WeaponClass(
        WeaponType weaponType, 
        int damage, 
        int magazineSize, 
        string name, 
        float fireRate, 
        float reloadTime)
    {
        this.weaponType = weaponType;
        this.damage = damage;
        this.magazineSize = magazineSize;
        this.name = name;
        this.fireRate = fireRate;
        this.reloadTime = reloadTime;
    }
}