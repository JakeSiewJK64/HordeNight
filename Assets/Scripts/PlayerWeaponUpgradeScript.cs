using UnityEngine;
using UnityEngine.UI;

public class PlayerWeaponUpgradeScript : MonoBehaviour
{
    [SerializeField]
    private ScrollRect scrollArea;

    [SerializeField]
    private GameObject viewholder;

    private void Start()
    {
        UpdateUpgradeItems();
    }

    public void UpdateUpgradeItems()
    {
        scrollArea.content.DetachChildren();
        foreach (WeaponClass item in GetComponent<PlayerInventoryScript>().GetPlayerInventory().GetAllWeaponsFromInventory())
        {
            GameObject newItem = Instantiate(viewholder, scrollArea.content);
            if (newItem.TryGetComponent(out BuyItemViewholderScript viewholderItem))
            {
                viewholderItem.SetItem(item);
            }
        }
    }
}
