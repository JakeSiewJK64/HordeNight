using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryScript : MonoBehaviour
{
    public List<WeaponClass> globalWeaponList = new List<WeaponClass>();
    public Dictionary<string, Item> inventory = new Dictionary<string, Item>();

    void InitializeWeapon()
    {
        globalWeaponList.Add(
            new WeaponClass("glock", "description", ItemType.Weapon, WeaponType.Sidearm, 3, 8, .2f, 2f, "glock.mp3", "glock_reload.mp3"));

        globalWeaponList.Add(
            new WeaponClass("m4", "description", ItemType.Weapon, WeaponType.AssaultRifle, 5, 30, .1f, 2f, "rifle_shoot.mp3", "glock_reload.mp3"));
    }

    void Start()
    {
        InitializeWeapon();
        inventory.Add("Weapon1", globalWeaponList[0]);
        inventory.Add("Weapon2", globalWeaponList[1]);
    }
}
