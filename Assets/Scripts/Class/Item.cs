public class Item
{
    string Name { get; set; }
    string Description { get; set; } 
    ItemType ItemType { get; set; }

    public Item(string name, string description, ItemType itemType)
    {
        Name = name;
        Description = description;
        ItemType = itemType;
    }
}
