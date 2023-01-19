using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryScript : MonoBehaviour
{
    private List<WeaponClass> globalWeaponList;
    private Inventory inventory;
    private WeaponClass currentWeapon;

    private string imagePath = "Assets/Raw/Img/";

    [SerializeField]
    private Image primaryWeaponHotBar;
    
    [SerializeField]
    private Image secondaryWeaponHotBar;

    private void Awake()
    {
        InitializeWeaponList();
        InitializeInventory();
        currentWeapon = (WeaponClass)inventory.GetPrimaryWeapon();
        primaryWeaponHotBar.GetComponent<Outline>().effectColor = Color.green;
        UpdateWeaponHotbarSprites();  
    }

    private void UpdateWeaponHotbarSprites()
    {
       primaryWeaponHotBar.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>(Path.Combine(imagePath, ((WeaponClass) inventory.GetPrimaryWeapon()).weaponIconPath));
       secondaryWeaponHotBar.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>(Path.Combine(imagePath, ((WeaponClass)inventory.GetSecondaryWeapon()).weaponIconPath));
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
            new WeaponClass("glock", "description", ItemType.Weapon, WeaponType.Sidearm, 3, 8, 8, .2f, 2f, "glock.mp3", "glock_reload.mp3", "m4.png"),
            new WeaponClass("m4", "description", ItemType.Weapon, WeaponType.AssaultRifle, 5, 30, 30, .1f, 2f, "assault_rifle/AutoGun_1p_02.wav", "glock_reload.mp3", "m4.png")
        };
    }

    private void CheckInput()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            gameObject.GetComponent<BulletSpawnScript>().ChangeWeapon((WeaponClass)inventory.GetPrimaryWeapon());
            primaryWeaponHotBar.GetComponent<Outline>().effectColor = Color.green;
            secondaryWeaponHotBar.GetComponent<Outline>().effectColor= Color.white;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gameObject.GetComponent<BulletSpawnScript>().ChangeWeapon((WeaponClass)inventory.GetSecondaryWeapon());
            primaryWeaponHotBar.GetComponent<Outline>().effectColor = Color.white;
            secondaryWeaponHotBar.GetComponent<Outline>().effectColor = Color.green;
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
}
