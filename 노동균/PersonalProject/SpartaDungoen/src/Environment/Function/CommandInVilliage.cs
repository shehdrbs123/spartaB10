
public class CommandInVilliage : Command
{
    public override void Execute()
    {
        // Top Print
        
        string title = _stringContainer.GetString("CommandInVilliageTop");
        string name = _stringContainer.GetString(_CurrentVilliage.NameId);
        
        _consoleTypingPrinter.TopList.Add(string.Format(title,name));
        _consoleTypingPrinter.TopList.Add(" ");
        ConsolePaint titlePainting = new ConsolePaint(0,0,name.Length,ConsoleColor.Yellow,ConsoleColor.Black);
        _consoleTypingPrinter.Paints.Add(titlePainting);

        
        // Select Print
        _currentFunctionListIdsIDs.FunctionListIds = _functionListContainer.GetFunctionList(nameof(CommandInVilliage));
        
        int functionCount = _currentFunctionListIdsIDs.Count;
        
        for (int i = 1; i < functionCount; i++)
        {
            string FunctionName = _stringContainer.GetString(_currentFunctionListIdsIDs.FunctionListIds[i]);
            _consoleTypingPrinter.SelectList.Add(i + ". " + FunctionName);
        }
    }
}