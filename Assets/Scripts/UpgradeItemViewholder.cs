using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItemViewholder : MonoBehaviour
{
    [SerializeField]
    private Image itemProfile;

    [SerializeField]
    private TextMeshProUGUI damage, reloadSpeed, fireRate, capacity;

    private WeaponClass itemObject;
    private string imagePath = "Raw\\Img\\";

    public void SetItemInfo(WeaponClass item)
    {
        itemObject = item;
        damage.text = "(Level " + item.upgradeStats.damage.GetLevel().ToString() + ") : " + item.upgradeStats.damage.GetValue().ToString();
        reloadSpeed.text = "(Level " + item.upgradeStats.reloadSpeed.GetLevel().ToString() + ") : " + item.upgradeStats.reloadSpeed.GetValue().ToString();
        fireRate.text = "(Level " + item.upgradeStats.fireRate.GetLevel().ToString() + ") : " + item.upgradeStats.fireRate.GetValue().ToString();
        capacity.text = "(Level " + item.upgradeStats.capacity.GetLevel().ToString() + ") : " + item.upgradeStats.capacity.GetValue().ToString();
        ChangeImage(item.weaponIconPath);
    }

    public void ChangeImage(string name)
    {
        itemProfile.sprite = Resources.Load<Sprite>(Path.Combine(imagePath, name));
    }

    public void OnMouseClick()
    {
        GetComponentInParent<UpgradeStationScript>().UpdateDescriptionPanel(itemObject);
    }
}
