using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyItemViewholderScript : MonoBehaviour
{
    [SerializeField]
    private Image image;

    [SerializeField]
    private TextMeshProUGUI itemName;
    
    [SerializeField]
    private TextMeshProUGUI itemType;

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

}
