public class CommandBuy : CommandInStoreBuy
{
    protected float Rate;
    protected int IsBuy;

    public CommandBuy()
    {
        Rate = 1f;
        IsBuy = -1;
    }
    public override void Execute()
    {
        if(!firstFunctionListSize.HasValue)
            firstFunctionListSize = _functionListContainer.GetFunctionList(nameof(CommandBuy)).Count;
        int input = _inputMemory.preInput - firstFunctionListSize.Value;

        Store store = _storeDataContainer.GetStore(_CurrentVilliage.NameId);
        string newItemID = store.GetItemString(input);
        Item newItem = _itemDataContainer.GetItem(newItemID);
        
        DoBusiness(newItemID,newItem);

        base.Execute();
        
    }

    protected virtual void DoBusiness(string newItemID, Item newItem)
    {
        if (_currentPlayer.Inventory.Contains(newItemID))
        {
            _consoleTypingPrinter.SelectList.Add(_stringContainer.GetString("AlreadyBuy"));
        }
        else
        {
            if (_currentPlayer.Status.Gold >= newItem.Gold)
            {
                _consoleTypingPrinter.SelectList.Add(_stringContainer.GetString("Bought"));
                _currentPlayer.Inventory.AddItemName(newItemID);
                _currentPlayer.Status.Gold += (int)(newItem.Gold * IsBuy * Rate) ;
            }
            else
            {
                _consoleTypingPrinter.SelectList.Add(_stringContainer.GetString("NotEnoughMoney"));
            }
        }
    }
}