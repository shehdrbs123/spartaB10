public class CommandInDungoen : Command
{
    public int? firstFunctionListSize;
    public override void Execute()
    {
        string functionName = GetType().Name;
        TopListEdit(functionName);
        
        List<string> functionList = _functionListContainer.GetFunctionList(functionName);
        _currentFunctionListIdsIDs.FunctionListIds = functionList;
        if (!firstFunctionListSize.HasValue)
            firstFunctionListSize = functionList.Count;
        
        List<string> dungoenList = _dungoenListDataContainer.GetFunctionList(_CurrentVilliage.NameId);
        
        if (firstFunctionListSize + dungoenList.Count > functionList.Count)
        {
            for (int i = 0; i < dungoenList.Count; i++)
            {
                functionList.Insert(1,"CommandDungoenFight");
            }
        }


        int functionCount = _currentFunctionListIdsIDs.Count;
        for (int i = 0; i < dungoenList.Count; i++)
        {
            string dungoenName = _stringContainer.GetString(dungoenList[i]);
            Dungoen dungoen = _dungoenDataContainer.GetDungoen(dungoenList[i]);
            string baseDefense = _stringContainer.GetString("BaseDefense");
            _consoleTypingPrinter.SelectList.Add($"{i+1}. {dungoenName}  ( {baseDefense} : {dungoen.BaseDefense} )");
        }
        string toVilliage = _stringContainer.GetString(functionList[functionList.Count - 1]);
        _consoleTypingPrinter.SelectList.Add((functionList.Count-1)+ ". " + toVilliage);
    }
}