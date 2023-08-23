public class CommandEquip : CommandInInventoryEquip
{
    public override void Execute()
    {
        int input = _inputMemory.preInput - firstFunctionListSize.Value;

        string newItemString = _currentPlayer.Inventory.GetItemID(input);
        Item newItem = _itemDataContainer.GetItem(newItemString);

        _currentPlayer.Equip(newItem);

        base.Execute();
    }
}