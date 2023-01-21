using System.IO;
using TMPro;
using UnityEditor;
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

    [SerializeField]
    private GameObject buyStation;

    private string imagePath = "Assets/Raw/Img/";

    public void SetItem(Item item)
    {
        itemObject = item;
        ChangeItemName(item.name);
        ChangeImage(AssetDatabase.LoadAssetAtPath<Sprite>(Path.Combine(imagePath, ((WeaponClass) item).weaponIconPath)));
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

    private void OnMouseClick()
    {
        GetComponentInParent<BuyStationScript>().UpdateDescriptionPanel(itemObject);
    }
}
