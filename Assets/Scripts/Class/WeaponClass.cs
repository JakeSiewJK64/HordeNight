public class WeaponClass
{
    public WeaponType weaponType;
    public int damage;
    public int magazineSize;
    public string name;
    public float fireRate;
    public WeaponClass(WeaponType weaponType, int damage, int magazineSize, string name, float fireRate)
    {
        this.weaponType = weaponType;
        this.damage = damage;
        this.magazineSize = magazineSize;
        this.name = name;
        this.fireRate = fireRate;
    }
}