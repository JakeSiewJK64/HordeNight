using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyItemViewholderScript : MonoBehaviour
{
    public Item itemObject;
    
    [SerializeField]
    private Image image;

    [SerializeField]
    private TextMeshProUGUI itemName;
    
    [SerializeField]
    private TextMeshProUGUI itemType;

    private string imagePath = "Raw\\Img\\";

    public void SetItem(Item item)
    {
        itemObject = item;
        ChangeItemName(item.name);
        ChangeImage(Resources.Load<Sprite>(Path.Combine(imagePath, ((WeaponClass) item).weaponIconPath)));
        if(item.itemType == ItemType.Weapon)
        {
            ChangeItemType(((WeaponClass) item).weaponType.ToString());
        }
    }

    public Image GetImage()
    {
        return image;
    }

    public string GetItemName()
    {
        return itemName.text;
    }

    public void ChangeItemName(string name)
    {
        itemName.text = name;
    }
    
    public void ChangeItemType(string type)
    {
        itemType.text = type;
    }

    public void ChangeImage(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void OnMouseClick()
    {
        GetComponentInParent<BuyStationScript>().UpdateDescriptionPanel(itemObject);
    }
}
