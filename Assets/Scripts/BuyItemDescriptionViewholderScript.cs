using TMPro;
using UnityEngine;

public class BuyItemDescriptionViewholderScript : MonoBehaviour
{
    private Item item;

    [SerializeField]
    private TextMeshProUGUI damage;

    [SerializeField]
    private TextMeshProUGUI reloadSpeed;

    [SerializeField]
    private TextMeshProUGUI magSize;

    private void Start()
    {
    }

    public void UpdateDescription(Item item)
    {
        this.item = item;
        if (item.itemType == ItemType.Weapon)
        {            
            damage.text = "Damage: " + ((WeaponClass)item).damage;
            reloadSpeed.text = "Reload Time: " + ((WeaponClass)item).reloadTime.ToString() + "s";
            magSize.text = "Magazine Size: " + ((WeaponClass)item).magazineSize.ToString();
        }
    }

    private void Update()
    {
        if(item != null)
        {
            UpdateDescription(item);
        }
    }
}
