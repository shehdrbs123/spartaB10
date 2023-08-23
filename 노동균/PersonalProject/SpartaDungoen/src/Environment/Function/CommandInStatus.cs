public class CommandInStatus : Command
{
    public override void Execute()
    {
        // Top Print
        string functionName = nameof(CommandInStatus);
        string title = _stringContainer.GetString(functionName+"Top");
        _consoleTypingPrinter.TopList.Add(title);
        _consoleTypingPrinter.TopList.Add("");

        //Info Print
        string name = _currentPlayer.Name;
        string level = _stringContainer.GetString("Level");
        string Job = _stringContainer.GetString("Job");
        string Attack = _stringContainer.GetString("Attack");
        string Defense = _stringContainer.GetString("Defense");
        string Health = _stringContainer.GetString("Health");
        string Gold = _stringContainer.GetString("Gold");

        string ExtraAttack = "(" + _currentPlayer.Status.ExtraAttack + ")";
        string ExtraDefense = "(" + _currentPlayer.Status.ExtraDefense + ")";
        string ExtraHealth = "(" + _currentPlayer.Status.ExtraHealth + ")"; 
        
        _consoleTypingPrinter.InfoList.Add($"이름 : {name} \n");
        _consoleTypingPrinter.InfoList.Add(string.Format(level, _currentPlayer.Status.Level));
        _consoleTypingPrinter.InfoList.Add(string.Format(Job, _currentPlayer.Status.Job));
        _consoleTypingPrinter.InfoList.Add(string.Format(Attack, _currentPlayer.Status.Attack+_currentPlayer.Status.ExtraAttack,ExtraAttack));
        _consoleTypingPrinter.InfoList.Add(string.Format(Defense, _currentPlayer.Status.Defense+_currentPlayer.Status.ExtraDefense,ExtraDefense));
        _consoleTypingPrinter.InfoList.Add(string.Format(Health, _currentPlayer.Status.Health+_currentPlayer.Status.ExtraDefense,ExtraHealth));
        _consoleTypingPrinter.InfoList.Add(string.Format(Gold, _currentPlayer.Status.Gold));
        _consoleTypingPrinter.InfoList.Add(" ");

        
        // Select Print
        _currentFunctionListIdsIDs.FunctionListIds = _functionListContainer.GetFunctionList(functionName);
        
        int functionCount = _currentFunctionListIdsIDs.Count;
        for (int i = 1; i < functionCount; i++)
        {
            string FunctionName = _stringContainer.GetString(_currentFunctionListIdsIDs.FunctionListIds[i]);
            _consoleTypingPrinter.SelectList.Add(i + ". " + FunctionName);
        }

    }
}