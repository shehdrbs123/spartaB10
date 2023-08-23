public class CommandDungoenFight : Command
{
    private Random rand;

    public CommandDungoenFight()
    {
        rand = new Random();
    }
    public override void Execute()
    {
        string TopTextID;
        string functionName = GetType().Name;
        int beforeHealth = _currentPlayer.Status.Health;
        int beforeGold = _currentPlayer.Status.Gold;
        int input = _inputMemory.preInput;
        string dungoenId = _dungoenListDataContainer.GetFunctionList(_CurrentVilliage.NameId)[input-1];
        Dungoen dungoen = _dungoenDataContainer.GetDungoen(dungoenId);
        string dungoenName = _stringContainer.GetString(dungoenId);
        
        bool bIsWin = isWin(dungoen);
        if (bIsWin)
        {
            TopTextID = "CommandDungoenClear";
        }
        else
        {
            TopTextID = "CommandDungoenDefeat";
        }

        string TopText = _stringContainer.GetString(TopTextID);
        TopText = string.Format(TopText,dungoenName);
        _consoleTypingPrinter.TopList.Add(TopText);
        _consoleTypingPrinter.TopList.Add("");
        
        
        //infoList 수정
        _consoleTypingPrinter.InfoList.Add(_stringContainer.GetString("ExploreResult"));
        
        string resultHealth = _stringContainer.GetString("ResultHealth");
        resultHealth = string.Format(resultHealth, beforeHealth, _currentPlayer.Status.Health);
        _consoleTypingPrinter.InfoList.Add(resultHealth);
        
        string resultGold = _stringContainer.GetString("ResultGold");
        resultGold = string.Format(resultGold, beforeGold, _currentPlayer.Status.Gold);
        _consoleTypingPrinter.InfoList.Add(resultGold);
        _consoleTypingPrinter.InfoList.Add("");

        SelectListEdit(functionName);
        
    }

    public bool isWin(Dungoen dungoen)
    {
        
        int playerDefense = _currentPlayer.Status.Defense;
        bool win = true;
        if (playerDefense < dungoen.BaseDefense)
        {
            int ran = rand.Next(0, 100);
            if (ran >= 60)
                win = false;
        }

        if (!win)
        {
            _currentPlayer.Status.Health /= 2;
        }
        else
        {
            int defenseGap = _currentPlayer.Status.Defense+_currentPlayer.Status.ExtraDefense - dungoen.BaseDefense;
            int damage = rand.Next(20+defenseGap, 36-defenseGap);
            _currentPlayer.Status.Health -= damage;
            int attack = _currentPlayer.Status.Attack + _currentPlayer.Status.ExtraAttack;
            int Gold = dungoen.Gold + (int)(dungoen.Gold * rand.Next(attack,attack*2+1)*0.01f);
            _currentPlayer.Status.Gold += Gold;
        }
            

        return win;
    }
}