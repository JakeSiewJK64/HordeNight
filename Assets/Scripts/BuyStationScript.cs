using System.Collections.Generic;
using System.IO;
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

    private void Start()
    {
        buyStation.enabled = false;
        InitializeWeaponList();
        populateBuystation();
    }

    private void populateBuystation()
    {
        foreach (WeaponClass item in globalWeaponList)
        {
            GameObject newItem = Instantiate(viewholder, scrollArea.content);
            if(newItem.TryGetComponent(out BuyItemViewholderScript viewholderItem))
            {
                viewholderItem.ChangeImage(Resources.Load<Sprite>(Path.Combine(imagePath, item.weaponIconPath)));
                viewholderItem.ChangeItemName(item.name);
                viewholderItem.ChangeItemType(item.weaponType.ToString());
            }
        }
    }

    private void CheckBuyStation(Collision col)
    {
        if (col.gameObject.tag == "Buystation")
        {
            // todo: enter buy station
            buyStation.enabled = true;
            mainScreen.enabled = false;
            ToggleCursor();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // todo: exit buy station
                buyStation.enabled = false;
                mainScreen.enabled = true;
                ToggleCursor();
            }
        } 
    }

    private void InitializeWeaponList()
    {
        globalWeaponList = new List<WeaponClass>
        {
            new WeaponClass("glock", "description", ItemType.Weapon, WeaponType.Sidearm, 3, 8, 8, .2f, 2f, "glock.mp3", "glock_reload.mp3", "m4.png"),
            new WeaponClass("m4", "description", ItemType.Weapon, WeaponType.AssaultRifle, 5, 30, 30, .1f, 2f, "assault_rifle/AutoGun_1p_02.wav", "glock_reload.mp3", "m4.png")
        };
    }

    private void ToggleCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void OnCollisionStay(Collision collision)
    {
        CheckBuyStation(collision);
    }

    private void OnCollisionEnter(Collision collision)
    {
        CheckBuyStation(collision);
    }
}
