public class ItemDataContainer : DataReader
{
    private const int _name=0;
    private const int _desciption=1;
    private const int _type=2;
    private const int _abilityName=3;
    private const int _abilityValue=4;
    private const int _gold=5;
    private Dictionary<string,Item> _items;

    public ItemDataContainer()
    {
        _items = new Dictionary<string,Item>();
    }
    public override void Process(string[] data)
    {
        Item newItem = new Item();
        newItem.NameID = data[_name];
        newItem.DescriptionID = data[_desciption];
        newItem.EquipType = Enum.Parse<EEquipType>(data[_type]);
        newItem.AbilityType = Enum.Parse<EStatus>(data[_abilityName]);
        newItem.AbilityValue = int.Parse(data[_abilityValue]);
        newItem.Gold = int.Parse(data[_gold]);
        _items.Add(newItem.NameID,newItem);
    }

    public Item GetItem(string itemName)
    {
        return _items[itemName];
    }
}