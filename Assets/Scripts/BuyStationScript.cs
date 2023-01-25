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
    private GameObject viewholder, descriptionPanel, buyButton;

    [SerializeField]
    private ScrollRect scrollArea;

    private List<WeaponClass> globalWeaponList;

    private Item selectedItem;

    public bool interacting = false;

    private string audioPath = "Assets/Raw/Sound/SoundEffects/menuSelect.mp3";

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
        if (Input.GetKeyDown(KeyCode.F))
        {            
            CheckBuyStation();
        }
    }

    private void CheckSelectedItem()
    {
        if(selectedItem == null)
        {
            descriptionPanel.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(false);
        } else
        {
            descriptionPanel.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(GetComponent<PlayerPointScript>().GetPoints() >= selectedItem.price);
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

    private void CheckBuyStation()
    {
        if (interacting)
        {
            // todo: exit buy station
            buyStation.gameObject.SetActive(false);
            mainScreen.gameObject.SetActive(true);
            ToggleCursor();
            interacting = false;
            return;
        }

        // todo: enter buy station
        buyStation.gameObject.SetActive(true);
        mainScreen.gameObject.SetActive(false);
        ToggleCursor();            
        interacting = true;
    }

    private void InitializeWeaponList()
    {
        globalWeaponList = new List<WeaponClass>
        {
            new WeaponClass("m4", "description", ItemType.Weapon, WeaponType.AssaultRifle, WeaponHolding.PRIMARY, 90, 15, 30, 30, .1f, 2f, "assault_rifle/AutoGun_1p_02.wav", "glock_reload.mp3", "m4.png", "glock18.prefab", 1000),
            new WeaponClass("m249", "description", ItemType.Weapon, WeaponType.LMG, WeaponHolding.PRIMARY, 300, 10, 150, 150, .1f, 5f, "assault_rifle/AutoGun_1p_02.wav", "Miniguns_loop/Minigun_Reload_04.wav", "m249.png", "m249.prefab", 1000)
        };
    }

    private void ToggleCursor()
    {
        Cursor.visible = !Cursor.visible;
        Cursor.lockState = Cursor.lockState == CursorLockMode.Confined ? CursorLockMode.Locked : CursorLockMode.Confined;
    }

    private void PlaySelectSound()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(AssetDatabase.LoadAssetAtPath<AudioClip>(Path.Combine(audioPath)));
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
        }
        else if (((WeaponClass)selectedItem).weaponHolding == WeaponHolding.SECONDARY)
        {
            gameObject.GetComponent<PlayerInventoryScript>().GetPlayerInventory().SetSecondaryWeapon((WeaponClass)selectedItem);
        }
        gameObject.GetComponent<PlayerInventoryScript>().UpdateWeaponHotbarSprites();
        gameObject.GetComponent<BulletSpawnScript>().ChangeWeapon((WeaponClass)selectedItem);
        gameObject.GetComponent<PlayerPointScript>().DeductPoints(selectedItem.price);
        selectedItem.price *= 2;
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
