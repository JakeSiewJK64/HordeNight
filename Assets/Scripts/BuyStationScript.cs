using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BuyStationScript : MonoBehaviour
{
    [SerializeField]
    private Canvas mainScreen, buyStation;

    [SerializeField]
    private GameObject viewholder, descriptionPanel, buyButton, buyAmmo;

    [SerializeField]
    private ScrollRect scrollArea;

    private List<WeaponClass> globalWeaponList;

    private Item selectedItem;
    private float ammoPrice = 1000f;
    public bool interacting = false;

    private string audioPath = "Raw\\Sound\\SoundEffects\\menuSelect";

    private void Start()
    {
        buyStation.gameObject.SetActive(false);
        InitializeWeaponList();
        populateBuystation();
        CheckSelectedItem();
    }

    private void Update()
    {
        CheckSelectedItem();
        if (Input.GetKeyDown(KeyCode.F) && Time.timeScale == 1)
        {            
            CheckBuyStation();
        }
    }

    private void CheckSelectedItem()
    {
        if (selectedItem == null)
        {
            descriptionPanel.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(false);
            buyAmmo.gameObject.SetActive(false);
        } else
        {
            descriptionPanel.gameObject.SetActive(true);

            if(selectedItem.itemType == ItemType.Weapon)
            {
                if(((WeaponClass)selectedItem).weaponHolding == WeaponHolding.PRIMARY &&
                    GetComponent<PlayerInventoryScript>().GetPlayerInventory().GetPrimaryWeapon() != null &&
                    GetComponent<PlayerInventoryScript>().GetPlayerInventory().GetPrimaryWeapon().name == selectedItem.name ||
                    ((WeaponClass)selectedItem).weaponHolding == WeaponHolding.SECONDARY &&
                    GetComponent<PlayerInventoryScript>().GetPlayerInventory().GetSecondaryWeapon() != null &&
                    GetComponent<PlayerInventoryScript>().GetPlayerInventory().GetSecondaryWeapon().name == selectedItem.name)
                {
                    // player already owns the weapon
                    buyButton.gameObject.SetActive(false);
                    buyAmmo.gameObject.SetActive(true);
                } else 
                {
                    buyButton.gameObject.SetActive(true);
                    buyAmmo.gameObject.SetActive(((WeaponClass)selectedItem).reserveAmmo != ((WeaponClass)selectedItem).startingAmmo);
                }
            }
        }
    }

    private void populateBuystation()
    {
        foreach (WeaponClass item in globalWeaponList)
        {
            GameObject newItem = Instantiate(viewholder, scrollArea.content);
            if(newItem.TryGetComponent(out BuyItemViewholderScript viewholderItem))
            {
                viewholderItem.SetItem(item);
            }
        }
    }

    public void CloseBuyStation()
    {
        buyStation.gameObject.SetActive(false);
        mainScreen.gameObject.SetActive(true);
        interacting = false;
    }

    public void CheckBuyStation()
    {
        if (interacting)
        {
            // todo: exit buy station
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            CloseBuyStation();
            return;
        }

        // todo: enter buy station
        buyStation.gameObject.SetActive(true);
        mainScreen.gameObject.SetActive(false);
        
        interacting = true;
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void InitializeWeaponList()
    {
        globalWeaponList = new List<WeaponClass>
        {
            new WeaponClass("m4a1", "description", ItemType.Weapon, WeaponType.AssaultRifle, WeaponHolding.PRIMARY, reserveAmmo: 90, startingAmmo: 90 , damage: 80, 30, 30, fireRate: .1f, reloadTime: 2f, "assault_rifle/AutoGun_1p_02", "glock_reload", "m4", "glock18", 1000),
            new WeaponClass("m249", "description", ItemType.Weapon, WeaponType.LMG, WeaponHolding.PRIMARY, reserveAmmo: 300, startingAmmo: 300, damage: 85, 150, 150, fireRate: .1f, reloadTime: 10f, "assault_rifle/AutoGun_1p_02", "Miniguns_loop/Minigun_Reload_04", "m249", "m249", 3000),
            new WeaponClass("m40a3", "description", ItemType.Weapon, WeaponType.Sniper, WeaponHolding.PRIMARY, reserveAmmo: 64, startingAmmo: 64, damage: 100, 8, 8, fireRate: 5f, reloadTime: 10f, "m40_shoot", "rifle_reload", "m40a3", "m40a3", 1500),
            new WeaponClass("assault shotgun", "description", ItemType.Weapon, WeaponType.Shotgun, WeaponHolding.PRIMARY, reserveAmmo: 64, startingAmmo : 64, damage: 20, 8, 8, fireRate: 1f, reloadTime: 5f, "shotgun_shoot", "shotgun_reload", "assault_shotgun", "shotgun", 500)
        };
    }

    private void PlaySelectSound()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>(Path.Combine(audioPath)));
    }

    public void UpdateDescriptionPanel(Item item)
    {
        PlaySelectSound();
        CheckSelectedItem();
        GetComponent<BuyItemDescriptionViewholderScript>().UpdateDescription(item);
        selectedItem = item;
    }

    private void BuyWeapon()
    {
        if (((WeaponClass)selectedItem).weaponHolding == WeaponHolding.PRIMARY)
        {
            gameObject.GetComponent<PlayerInventoryScript>().GetPlayerInventory().SetPrimaryWeapon((WeaponClass)selectedItem);
            gameObject.GetComponent<PlayerInventoryScript>().TogglePrimaryHotbar();
        }
        else if (((WeaponClass)selectedItem).weaponHolding == WeaponHolding.SECONDARY)
        {
            gameObject.GetComponent<PlayerInventoryScript>().GetPlayerInventory().SetSecondaryWeapon((WeaponClass)selectedItem);
            gameObject.GetComponent<PlayerInventoryScript>().ToggleSecondaryHotbar();
        }
        ((WeaponClass)selectedItem).ResetReserveAmmo();
        gameObject.GetComponent<PlayerInventoryScript>().UpdateWeaponHotbarSprites();
        gameObject.GetComponent<BulletSpawnScript>().ChangeWeapon((WeaponClass)selectedItem);
        gameObject.GetComponent<PlayerPointScript>().DeductPoints(selectedItem.price);
        selectedItem.price *= 2;
    }

    public void OnBuyAmmoButton()
    {
        ((WeaponClass)selectedItem).ResetReserveAmmo();
        gameObject.GetComponent<PlayerPointScript>().DeductPoints(ammoPrice);
    }

    public void OnBuyButtonPressed()
    {
        PlaySelectSound();
        if(selectedItem.itemType == ItemType.Weapon)
        {
            BuyWeapon();
        }
    }
}
