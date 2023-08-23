public class Player
{
    public string Name { get; set; }
    public Inventory Inventory{ get; set; }
    public Status Status { get; set; }
    public Item[] Equiped { get; set; }
    public Player(string name)
    {
        Name = name;
        Inventory = new Inventory();
        Status = new Status();
        Equiped = new Item[Enum.GetNames<EEquipType>().Length];
    }

    public void Equip(Item newItem)
    {
        Item currentEquiped = Equiped[(int)newItem.EquipType];
        if (currentEquiped == newItem)
        {
            Equiped[(int)newItem.EquipType] = null;
            Status.AddExtra(currentEquiped.AbilityType,-currentEquiped.AbilityValue);
        }else
        {
            if(currentEquiped != null)
                Status.AddExtra(currentEquiped.AbilityType,-currentEquiped.AbilityValue);
                
            Equiped[(int)newItem.EquipType] = newItem;
            Status.AddExtra(newItem.AbilityType,newItem.AbilityValue);
        }
    }
}