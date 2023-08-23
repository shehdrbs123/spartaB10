public class CommandSell : CommandInStoreSell
{
    public override void Execute()
    {
        if(!firstFunctionListSize.HasValue)
            firstFunctionListSize = _functionListContainer.GetFunctionList(nameof(CommandBuy)).Count;
        int input = _inputMemory.preInput - firstFunctionListSize.Value;

        string sellId = _currentPlayer.Inventory.GetItemID(input);
        Item newItem = _itemDataContainer.GetItem(sellId);
        
        DoBusiness(sellId,newItem);
        base.Execute();
    }

    protected void DoBusiness(string newItemID, Item newItem)
    {
        if(_currentPlayer.Equiped[(int)newItem.EquipType] == newItem)
            _currentPlayer.Equip(newItem);
        
        _currentPlayer.Inventory.Remove(newItemID);
        _currentPlayer.Status.Gold += (int)(newItem.Gold * 0.85f);
        _consoleTypingPrinter.SelectList.Add(_stringContainer.GetString("Selled"));
    }
}