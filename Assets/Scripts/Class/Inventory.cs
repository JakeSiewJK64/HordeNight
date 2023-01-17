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
        return this.inventory["Primary"];
    }

    public Item GetSecondaryWeapon()
    {
        return inventory["Secondary"];
    }
}