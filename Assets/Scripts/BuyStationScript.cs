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
    private GameObject viewholder;

    [SerializeField]
    private ScrollRect scrollArea;

    private List<WeaponClass> globalWeaponList;

    private string imagePath = "Assets/Raw/Img/";

    private bool interacting = false;

    private void Start()
    {
        buyStation.gameObject.SetActive(false);
        InitializeWeaponList();
        populateBuystation();
    }

    private void Update()
    {
        if(interacting && Input.GetKeyDown(KeyCode.F))
        {
            // todo: exit buy station
            buyStation.gameObject.SetActive(false);
            mainScreen.gameObject.SetActive(true);
            ToggleCursor();
            interacting = false;
            GetComponent<BulletSpawnScript>().interactingBuyStation = false;
        }
    }

    private void populateBuystation()
    {
        foreach (WeaponClass item in globalWeaponList)
        {
            GameObject newItem = Instantiate(viewholder, scrollArea.content);
            if(newItem.TryGetComponent(out BuyItemViewholderScript viewholderItem))
            {
                viewholderItem.ChangeImage(AssetDatabase.LoadAssetAtPath<Sprite>(Path.Combine(imagePath, item.weaponIconPath)));
                viewholderItem.ChangeItemName(item.name);
                viewholderItem.ChangeItemType(item.weaponType.ToString());
            }
        }
    }

    private void CheckBuyStation()
    {
        // todo: enter buy station
        buyStation.gameObject.SetActive(true);
        mainScreen.gameObject.SetActive(false);
        ToggleCursor();            
        interacting = true;
        GetComponent<BulletSpawnScript>().interactingBuyStation = true;
    }

    private void InitializeWeaponList()
    {
        globalWeaponList = new List<WeaponClass>
        {
            new WeaponClass("glock", "description", ItemType.Weapon, WeaponType.Sidearm, 3, 8, 8, .2f, 2f, "glock.mp3", "glock_reload.mp3", "glock.png"),
            new WeaponClass("glock", "description", ItemType.Weapon, WeaponType.Sidearm, 3, 8, 8, .2f, 2f, "glock.mp3", "glock_reload.mp3", "glock.png"),
            new WeaponClass("glock", "description", ItemType.Weapon, WeaponType.Sidearm, 3, 8, 8, .2f, 2f, "glock.mp3", "glock_reload.mp3", "glock.png"),
            new WeaponClass("glock", "description", ItemType.Weapon, WeaponType.Sidearm, 3, 8, 8, .2f, 2f, "glock.mp3", "glock_reload.mp3", "glock.png"),
            new WeaponClass("glock", "description", ItemType.Weapon, WeaponType.Sidearm, 3, 8, 8, .2f, 2f, "glock.mp3", "glock_reload.mp3", "glock.png"),
            new WeaponClass("glock", "description", ItemType.Weapon, WeaponType.Sidearm, 3, 8, 8, .2f, 2f, "glock.mp3", "glock_reload.mp3", "glock.png"),
            new WeaponClass("glock", "description", ItemType.Weapon, WeaponType.Sidearm, 3, 8, 8, .2f, 2f, "glock.mp3", "glock_reload.mp3", "glock.png"),
            new WeaponClass("m4", "description", ItemType.Weapon, WeaponType.AssaultRifle, 5, 30, 30, .1f, 2f, "assault_rifle/AutoGun_1p_02.wav", "glock_reload.mp3", "m4.png")
        };
    }

    private void ToggleCursor()
    {
        Cursor.visible = !Cursor.visible;
        Cursor.lockState = Cursor.lockState == CursorLockMode.Confined ? CursorLockMode.Locked : CursorLockMode.Confined;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Buystation")
        {
            CheckBuyStation();
        }
    }
}
