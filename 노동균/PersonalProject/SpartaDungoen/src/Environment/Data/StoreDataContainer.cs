public class StoreDataContainer : DataReader
{
    private Dictionary<string, Store> _stores;

    public StoreDataContainer()
    {
        _stores = new Dictionary<string, Store>();
    }
    public override void Process(string[] data)
    {
        Store newStore = new Store();

        int Length = int.Parse(data[1]);
        for (int i = 0; i < Length; ++i)
        {
            newStore.AddItemString(data[i+2]);
        }
        _stores.Add(data[0],newStore);
    }

    public Store GetStore(string name)
    {
        return _stores[name];
    }
}