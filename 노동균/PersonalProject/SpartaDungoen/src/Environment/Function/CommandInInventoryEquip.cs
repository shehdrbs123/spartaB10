public class CommandInInventoryEquip : Command
{
    public static int? firstFunctionListSize;

    public override void Execute()
    {
        //  Top Print
        string functionName = nameof(CommandInInventoryEquip);
        _currentFunctionListIdsIDs.FunctionListIds = _functionListContainer.GetFunctionList(functionName);
        if (!firstFunctionListSize.HasValue)
            firstFunctionListSize = _currentFunctionListIdsIDs.FunctionListIds.Count;
        string title = _stringContainer.GetString(functionName+"Top");
        _consoleTypingPrinter.TopList.Add(title);
        _consoleTypingPrinter.TopList.Add("");

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

            string result = string.Format($"- {i+firstFunctionListSize}. {Equip,4} {name,-20}|{AbilityType,-10} {abilityValue,-6}|{description}");
            _consoleTypingPrinter.InfoList.Add(result);
        }
        _consoleTypingPrinter.InfoList.Add("");
        
        //  Select Print
        for (int i = 1; i < firstFunctionListSize; i++)
        {
            string FunctionName = _stringContainer.GetString(_currentFunctionListIdsIDs.FunctionListIds[i]);
            _consoleTypingPrinter.SelectList.Add(i + ". " + FunctionName);
        }
        
        // 인벤토리 내 값 선택을 위해 내용 추가.
        int? preInventoryCount = _currentFunctionListIdsIDs.Count - firstFunctionListSize;
        if (preInventoryCount > inventory.Count)
        {
            while (preInventoryCount != inventory.Count)
            {
                _currentFunctionListIdsIDs.FunctionListIds.RemoveAt(_currentFunctionListIdsIDs.FunctionListIds.Count-1);
                --preInventoryCount;
            }
            
        }
        else if(preInventoryCount < inventory.Count)
        {
            while (preInventoryCount != inventory.Count)
            {
                _currentFunctionListIdsIDs.FunctionListIds.Add("CommandEquip");
                ++preInventoryCount;
            }
            
        }
                    
    }
}