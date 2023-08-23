public class CommandInStore : Command
{
    protected bool isInteraction = false;
    protected int? firstFunctionListSize;
    public override void Execute()
    {
        // print Top
        string functionName = this.GetType().Name;
        _currentFunctionListIdsIDs.FunctionListIds = _functionListContainer.GetFunctionList(functionName);
        if(!firstFunctionListSize.HasValue)
            firstFunctionListSize = _currentFunctionListIdsIDs.FunctionListIds.Count;
        _consoleTypingPrinter.TopList.Add(_stringContainer.GetString(functionName+"Top"));
        _consoleTypingPrinter.TopList.Add("");
        _consoleTypingPrinter.TopList.Add(_stringContainer.GetString("GoldTop"));

        string goldString = _stringContainer.GetString("Gold");
        goldString = string.Format(goldString, _currentPlayer.Status.Gold);
        _consoleTypingPrinter.TopList.Add(goldString);
        _consoleTypingPrinter.TopList.Add("");

        // print Info
        InfoProcess();
       
        
        //print fuctions
        _currentFunctionListIdsIDs.FunctionListIds = _functionListContainer.GetFunctionList(functionName);
        
        for (int i = 1; i < firstFunctionListSize; i++)
        {
            string FunctionName = _stringContainer.GetString(_currentFunctionListIdsIDs.FunctionListIds[i]);
            _consoleTypingPrinter.SelectList.Add(i + ". " + FunctionName);
        }
        FunctionAdd();
    }

    protected virtual void InfoProcess()
    {
        Store currentStore = _storeDataContainer.GetStore(_CurrentVilliage.NameId);
        _consoleTypingPrinter.InfoList.Add(_stringContainer.GetString("ItemList"));
        for (int i = 0; i < currentStore.Count; i++)
        {
            string ItemNameID = currentStore.GetItemString(i);
            Item item = _itemDataContainer.GetItem(ItemNameID);

            string isBuyID;
            string name = _stringContainer.GetString(ItemNameID);
            string abilityValue = item.AbilityValue >= 0 ? "+" + item.AbilityValue : "-" + item.AbilityValue;
            string description = _stringContainer.GetString(item.DescriptionID);
            string AbilityType = item.AbilityType.ToString();
            string isBuy = "";
            
            if (_currentPlayer.Inventory.Contains(ItemNameID))
            {
                isBuyID = "Bought";
                isBuy = _stringContainer.GetString(isBuyID);
            }
            else
            {
                isBuyID = "Gold";
                isBuy = _stringContainer.GetString(isBuyID);
                isBuy = string.Format(isBuy, item.Gold);
            }

            string iString = "";
            if (isInteraction)
            {
                iString = (i + firstFunctionListSize).ToString() + ". ";
            }
            
            string result = string.Format($"- {iString}{name,-20}|{AbilityType,-10} {abilityValue,-6}|{description,40}| {isBuy}");
            _consoleTypingPrinter.InfoList.Add(result);
        }
        _consoleTypingPrinter.InfoList.Add("");
    }

    protected virtual void FunctionAdd() { }
}