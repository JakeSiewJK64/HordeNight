using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryScript : MonoBehaviour
{
    private List<WeaponClass> globalWeaponList;
    private Inventory inventory;
    private WeaponClass currentWeapon;

    private void Awake()
    {
        InitializeWeaponList();
        InitializeInventory();
        currentWeapon = (WeaponClass)inventory.GetPrimaryWeapon();
    }

    private void InitializeInventory()
    {
        inventory = new Inventory(
           new Dictionary<string, Item> {
                { "Primary", globalWeaponList[1] },
                { "Secondary", globalWeaponList[0] }
           }
       );
    }

    private void InitializeWeaponList()
    {
        globalWeaponList = new List<WeaponClass>
        {
            new WeaponClass("glock", "description", ItemType.Weapon, WeaponType.Sidearm, 3, 8, .2f, 2f, "glock.mp3", "glock_reload.mp3"),

            new WeaponClass("m4", "description", ItemType.Weapon, WeaponType.AssaultRifle, 5, 30, .1f, 2f, "rifle_shoot.mp3", "glock_reload.mp3")
        };
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            gameObject.GetComponent<BulletSpawnScript>().ChangeWeapon((WeaponClass)inventory.GetPrimaryWeapon());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gameObject.GetComponent<BulletSpawnScript>().ChangeWeapon((WeaponClass)inventory.GetSecondaryWeapon());
        }
    }

    public WeaponClass GetCurrentWeapon()
    {
        return currentWeapon;
    }

    public Inventory GetInventory() { 
        return inventory;
    }
}
