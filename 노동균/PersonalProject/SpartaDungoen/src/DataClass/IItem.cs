public enum EEquipType
{
    Hand=0,Shirt,Pants,Shoes,Head
}
public class Item
{
    public string NameID { get; set; }
    public string DescriptionID { get; set; }
    public EEquipType EquipType { get; set; }
    public EStatus AbilityType { get; set; }
    public int AbilityValue { get; set; }
    public int Gold { get; set; }
}