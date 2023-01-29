using System.Collections.Generic;
using UnityEngine;

public class UpgradeDescScript : MonoBehaviour
{
    [SerializeField]
    private GameObject firepower, reloadspeed, capacity, firerate;

    class UpgradeItem
    {
        public string name;
        public UpgradeModule module;
        public GameObject viewholder;
        public UpgradeItem(GameObject viewholder, string name, UpgradeModule module)
        {
            this.viewholder = viewholder;
            this.name = name;
            this.module = module;
        }
    }

    public void UpdateViewholder(WeaponClass item)
    {
        List<UpgradeItem> upgradeList = new List<UpgradeItem> { 
            new UpgradeItem(firepower, "Damage", item.upgradeStats.damage),
            new UpgradeItem(reloadspeed, "Reload Speed", item.upgradeStats.reloadSpeed),
            new UpgradeItem(capacity, "Ammo Capacity", item.upgradeStats.capacity),
            new UpgradeItem(firerate, "Rate of Fire", item.upgradeStats.fireRate),
        };

        foreach (UpgradeItem upgradeItem in upgradeList)
        {
            if (upgradeItem.viewholder.TryGetComponent(out UpgradeDescViewholder viewholderItem))
            {
                viewholderItem.UpdateUpgradeName(name: upgradeItem.name);
                viewholderItem.SetUpgradeModule(module: upgradeItem.module);
            }
        }
    }
}
