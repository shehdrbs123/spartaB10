
public abstract class Command
{
    protected static ResourceManager _resourceManager;
    protected static StringContainer _stringContainer;
    protected static VilliageDataContainer _villiageDataContainer;
    protected static Player _currentPlayer;
    protected static Villiage _CurrentVilliage;
    protected static ConsoleTypingPrinter _consoleTypingPrinter;
    protected static FunctionListContainer _functionListContainer;
    protected static ItemDataContainer _itemDataContainer;
    protected static StoreDataContainer _storeDataContainer;
    protected static CurrentFunctionListIds _currentFunctionListIdsIDs;
    protected static DungoenDataContainer _dungoenDataContainer;
    protected static DungoenListDataContainer _dungoenListDataContainer;
    protected static InputMemory _inputMemory;

    public static void Init(ResourceManager resourceManager, Player currentPlayer, Villiage currentVilliage, ConsoleTypingPrinter consoleTypingPrinter, CurrentFunctionListIds currentFunctionListIdsIds, InputMemory inputMemory)
    {
        _resourceManager = resourceManager;
        _currentPlayer = currentPlayer;
        _CurrentVilliage = currentVilliage;
        _consoleTypingPrinter = consoleTypingPrinter;
        _stringContainer = _resourceManager.StringContainer;
        _villiageDataContainer = _resourceManager.VilliageDataContainer;
        _functionListContainer = _resourceManager.FunctionListContainer;
        _itemDataContainer = _resourceManager.ItemDataContainer;
        _storeDataContainer = _resourceManager.StoreDataContainer;
        _dungoenDataContainer = _resourceManager.DungoenDataContainer;
        _dungoenListDataContainer = _resourceManager.DungoenListDataContainer;
        _currentFunctionListIdsIDs = currentFunctionListIdsIds;
        _inputMemory = inputMemory;
    }
    public abstract void Execute();

    protected virtual void TopListEdit( string functionName )
    {
        string title = _stringContainer.GetString(functionName+"Top");
        _consoleTypingPrinter.TopList.Add(title);
        _consoleTypingPrinter.TopList.Add("");
    }

    protected virtual void SelectListEdit(string functionName)
    {
        _currentFunctionListIdsIDs.FunctionListIds = _functionListContainer.GetFunctionList(functionName);
        
        int functionCount = _currentFunctionListIdsIDs.Count;
        for (int i = 1; i < functionCount; i++)
        {
            string FunctionName = _stringContainer.GetString(_currentFunctionListIdsIDs.FunctionListIds[i]);
            _consoleTypingPrinter.SelectList.Add(i + ". " + FunctionName);
        }
    }
}

