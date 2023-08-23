public class Store
{
    private Inventory Goods;

    public int Count
    {
        get { return Goods.Count; }
    }
    public Store()
    {
        Goods = new Inventory();
    }

    public void AddItemString(string name)
    {
        Goods.AddItemName(name);
    }

    public string GetItemString(int i)
    {
        return Goods.GetItemID(i);
    }
    
    
}