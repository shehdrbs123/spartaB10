public class CommandInStoreSell : CommandInStore
{
    public CommandInStoreSell()
    {
        isInteraction = true;
    }
    
    protected override void InfoProcess()
    {
        _consoleTypingPrinter.InfoList.Add(_stringContainer.GetString("ItemList"));
        int inventoryCount = _currentPlayer.Inventory.Count;
        for (int i = 0; i < inventoryCount; i++)
        {
            string itemID = _currentPlayer.Inventory.GetItemID(i);
            Item item = _itemDataContainer.GetItem(itemID);
            
            string name = _stringContainer.GetString(itemID);
            string abilityType = item.AbilityType.ToString();
            string abilityValue = item.AbilityValue >= 0 ? "+" + item.AbilityValue : "-" + item.AbilityValue;
            string description = _stringContainer.GetString(item.DescriptionID);
            string isBuy = _stringContainer.GetString("Gold");
            isBuy = string.Format(isBuy, item.Gold);
            
            string iString = "";
            if (isInteraction)
            {
                iString = (i + firstFunctionListSize).ToString() + ". ";
            }
            string result = string.Format($"- {iString}{name,-20}|{abilityType,-10} {abilityValue,-6}|{description,40}| {isBuy}");
            _consoleTypingPrinter.InfoList.Add(result);
        }
        _consoleTypingPrinter.InfoList.Add("");
    }

    protected override void FunctionAdd()
    {
        Inventory playerInventory = _currentPlayer.Inventory;
        int? preInventoryCount = _currentFunctionListIdsIDs.Count - firstFunctionListSize;
        if (preInventoryCount > playerInventory.Count)
        {
            while (preInventoryCount != playerInventory.Count)
            {
                _currentFunctionListIdsIDs.FunctionListIds.RemoveAt(_currentFunctionListIdsIDs.FunctionListIds.Count-1);
                --preInventoryCount;
            }
            
        }
        else if(preInventoryCount < playerInventory.Count)
        {
            while (preInventoryCount != playerInventory.Count)
            {
                _currentFunctionListIdsIDs.FunctionListIds.Add( "CommandSell");
                ++preInventoryCount;
            }
        }
    }
}