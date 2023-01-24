public class Item
{
    public string name { get; set; }
    public string description { get; set; }
    public ItemType itemType { get; set; }
    public float price { get; set; }

    public Item(string name, string description, ItemType itemType, float price)
    {
        this.name = name;
        this.description = description;
        this.itemType = itemType;
        this.price = price;
    }
}
