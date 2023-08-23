public class CommandInInventory : Command
{
    public override void Execute()
    {
        //  Top Print
        string functionName = GetType().Name;
        TopListEdit(functionName);

        // Info Print
        Inventory inventory = _currentPlayer.Inventory;
        
        _consoleTypingPrinter.InfoList.Add(_stringContainer.GetString("ItemList"));
        for (int i = 0; i < inventory.Count; i++)
        {
            Item item = _itemDataContainer.GetItem(inventory.GetItemID(i));
            string abilityValue = item.AbilityValue >= 0 ? "+" + item.AbilityValue : "-" + item.AbilityValue;
            string name = _stringContainer.GetString(item.NameID);
            string description = _stringContainer.GetString(item.DescriptionID);
            string AbilityType = item.AbilityType.ToString();
            string Equip = "";

            Item currentEquip = _currentPlayer.Equiped[(int)item.EquipType];
            if (currentEquip != null && currentEquip == item)
            {
                Equip = "[E]";
            }
            
            string result = string.Format($"- {Equip,4} {name,-20}|{AbilityType,-10} {abilityValue,-6}|{description}");
            _consoleTypingPrinter.InfoList.Add(result);
        }
        _consoleTypingPrinter.InfoList.Add("");
        
        //  Select Print
        _currentFunctionListIdsIDs.FunctionListIds = _functionListContainer.GetFunctionList(functionName);
        
        int functionCount = _currentFunctionListIdsIDs.Count;
        for (int i = 1; i < functionCount; i++)
        {
            string FunctionName = _stringContainer.GetString(_currentFunctionListIdsIDs.FunctionListIds[i]);
            _consoleTypingPrinter.SelectList.Add(i + ". " + FunctionName);
        }
    }
}