public class CommandInStoreBuy  : CommandInStore
{
    public CommandInStoreBuy()
    {
        isInteraction = true;
    }

    protected override void FunctionAdd()
    {
        Store store = _storeDataContainer.GetStore(_CurrentVilliage.NameId);
        int? preInventoryCount = _currentFunctionListIdsIDs.Count - firstFunctionListSize;
        if (preInventoryCount > store.Count)
        {
            while (preInventoryCount != store.Count)
            {
                _currentFunctionListIdsIDs.FunctionListIds.RemoveAt(_currentFunctionListIdsIDs.FunctionListIds.Count-1);
                --preInventoryCount;
            }
            
        }
        else if(preInventoryCount < store.Count)
        {
            while (preInventoryCount != store.Count)
            {
                _currentFunctionListIdsIDs.FunctionListIds.Add( "CommandBuy");
                ++preInventoryCount;
            }
        }
    }
}