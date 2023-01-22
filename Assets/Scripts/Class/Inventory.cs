using System.Collections.Generic;

public class Inventory
{
    public Dictionary<string, Item> inventory { get; set; }

    public Inventory(Dictionary<string, Item> inventory)
    {
        this.inventory = inventory;
    }

    public Item GetPrimaryWeapon()
    {
        try
        {
            return inventory["Primary"];
        } catch { return null; }
    }

    public Item GetSecondaryWeapon()
    {
        try
        {
            return inventory["Secondary"];
        }
        catch { return null; }
    }

    public void SetPrimaryWeapon(WeaponClass item)
    {
        if (inventory.ContainsKey("Primary"))
        {
            inventory.Remove("Primary");
        }
        inventory.Add("Primary", item);
    }

    public void SetSecondaryWeapon(WeaponClass item)
    {
        if (inventory.ContainsKey("Secondary"))
        {
            inventory.Remove("Secondary");
        }
        inventory.Add("Secondary", item);
    }
}