using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryScript : MonoBehaviour
{
    private Inventory inventory;
    private WeaponClass currentWeapon;

    private string imagePath = "Assets/Raw/Img/";

    [SerializeField]
    private Image primaryWeaponHotBar;
    
    [SerializeField]
    private Image secondaryWeaponHotBar;

    private void Awake()
    {
        InitializeInventory();
        currentWeapon = (WeaponClass)inventory.GetSecondaryWeapon();
        UpdateWeaponHotbarSprites();
        ToggleSecondaryHotbar();
    }

    public void UpdateWeaponHotbarSprites()
    {
        if(inventory.GetPrimaryWeapon() != null)
        {
            primaryWeaponHotBar.GetComponent<Image>().sprite = AssetDatabase.LoadAssetAtPath<Sprite>(Path.Combine(imagePath, ((WeaponClass) inventory.GetPrimaryWeapon()).weaponIconPath));
        } else if (inventory.GetSecondaryWeapon() != null)
        {
            secondaryWeaponHotBar.GetComponent<Image>().sprite = AssetDatabase.LoadAssetAtPath<Sprite>(Path.Combine(imagePath, ((WeaponClass)inventory.GetSecondaryWeapon()).weaponIconPath));
        }
    }

    private void InitializeInventory()
    {
        inventory = new Inventory(
           new Dictionary<string, Item> {
                { 
                   "Secondary", 
                   new WeaponClass("glock", "description", ItemType.Weapon, WeaponType.Sidearm, WeaponHolding.SECONDARY, 3, 8, 8, .2f, 2f, "glock.mp3", "glock_reload.mp3", "glock.png")
                }
           }
       );
    }

    private void ToggleSecondaryHotbar()
    {
            gameObject.GetComponent<BulletSpawnScript>().ChangeWeapon((WeaponClass)inventory.GetSecondaryWeapon());
            primaryWeaponHotBar.GetComponent<Outline>().effectColor = new Color(0, 0, 0, .2f);
            secondaryWeaponHotBar.GetComponent<Outline>().effectColor = new Color(46, 204, 113, 1.0f);
    }

    private void TogglePrimaryHotbar()
    {
            gameObject.GetComponent<BulletSpawnScript>().ChangeWeapon((WeaponClass)inventory.GetPrimaryWeapon());
            primaryWeaponHotBar.GetComponent<Outline>().effectColor = new Color(46, 204, 113, 1.0f);
            secondaryWeaponHotBar.GetComponent<Outline>().effectColor = new Color(0, 0, 0, .2f);
    }

    private void CheckInput()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && inventory.GetPrimaryWeapon() != null) 
        {
            TogglePrimaryHotbar();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && inventory.GetSecondaryWeapon() != null)
        {
            ToggleSecondaryHotbar();
        }
    }

    private void Update()
    {
        CheckInput();
    }

    public WeaponClass GetCurrentWeapon()
    {
        return currentWeapon;
    }

    public Inventory GetPlayerInventory()
    {
        return inventory;
    }
}
