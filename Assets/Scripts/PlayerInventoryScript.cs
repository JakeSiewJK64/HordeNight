using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryScript : MonoBehaviour
{
    private List<WeaponClass> globalWeaponList;
    private Inventory inventory;

    private void Awake()
    {
        globalWeaponList = new List<WeaponClass>
        {
            new WeaponClass("glock", "description", ItemType.Weapon, WeaponType.Sidearm, 3, 8, .2f, 2f, "glock.mp3", "glock_reload.mp3"),

            new WeaponClass("m4", "description", ItemType.Weapon, WeaponType.AssaultRifle, 5, 30, .1f, 2f, "rifle_shoot.mp3", "glock_reload.mp3")
        };
        
        inventory = new Inventory(
            new Dictionary<string, Item> {
                { "Primary", globalWeaponList[1] },
                { "Secondary", globalWeaponList[0] }
            }
        );
    }

    public Inventory GetInventory() { 
        return inventory;
    }
}
