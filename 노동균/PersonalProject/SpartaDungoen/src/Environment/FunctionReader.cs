public class FunctionReader
{
    private Dictionary<string, Command> _functionDic;

    public FunctionReader()
    {
        _functionDic = new Dictionary<string, Command>();

        SettingFunction();
    }

    private void SettingFunction()
    {
        _functionDic[nameof(CommandInVilliage)] = new CommandInVilliage();
        _functionDic[nameof(CommandInStatus)] = new CommandInStatus();
        _functionDic[nameof(CommandInInventory)] = new CommandInInventory();
        _functionDic[nameof(CommandInInventoryEquip)] = new CommandInInventoryEquip();
        _functionDic[nameof(CommandEquip)] = new CommandEquip();
        _functionDic[nameof(CommandInStore)] = new CommandInStore();
        _functionDic[nameof(CommandInStoreBuy)] = new CommandInStoreBuy();
        _functionDic[nameof(CommandBuy)] = new CommandBuy();
        _functionDic[nameof(CommandInStoreSell)] = new CommandInStoreSell();
        _functionDic[nameof(CommandSell)] = new CommandSell();
        _functionDic[nameof(CommandInDungoen)] = new CommandInDungoen();
        _functionDic[nameof(CommandDungoenFight)] = new CommandDungoenFight();
    }

    public Command GetFunction(string functionId)
    {
        
        return _functionDic[functionId];
    }
}
