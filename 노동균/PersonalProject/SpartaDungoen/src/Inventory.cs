public class Inventory
{
    private List<string> ItemNames;

    public Inventory()
    {
        ItemNames = new List<string>();
    }

    public string GetItemID(int i)
    {
        return ItemNames[i];
    }

    public void AddItemName(string itemName)
    {
        ItemNames.Add(itemName);
        ItemNames = ItemNames.OrderBy(i => i.Length).Reverse().ToList();
    }

    public bool Contains(string itemName)
    {
        return ItemNames.Contains(itemName);
    }

    public void Remove(string itemName)
    {
        ItemNames.Remove(itemName);
    }

    public int Count
    {
        get { return ItemNames.Count; }
    }
}