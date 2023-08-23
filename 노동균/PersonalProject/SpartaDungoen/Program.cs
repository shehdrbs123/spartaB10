// See https://aka.ms/new-console-template for more information

public class Program
{
    public static void Main(string[] args)
    {
        ThisisSpartaCore core = new ThisisSpartaCore();
        core.Play();
    }
}

public class CurrentFunctionListIds
{
    public List<string> FunctionListIds;

    public int Count
    {
        get
        {
            return FunctionListIds.Count;
        }
    }
}

public class ThisisSpartaCore
{
    private ConsoleTypingPrinter _printer;

    private ResourceManager _resources;
    private Villiage _currentVilliage;
    private Player _currentPlayer;
    private FunctionReader _functionReader;
    private CurrentFunctionListIds _currentFunctionListIds;
    private InputMemory _inputMemory;
    
    public ThisisSpartaCore() { }
    public void Play()
    {
        InstanceInit();
        LoadResourses();
        LoadData();
        GameInit();
        GameStart();
    }

    private void InstanceInit()
    {
        _printer = new ConsoleTypingPrinter(100);
        _currentFunctionListIds = new CurrentFunctionListIds();
        _functionReader = new FunctionReader();
        _inputMemory = new InputMemory();
    }

    private void LoadResourses()
    {
        
    }
    
    private void LoadData()
    {
        _resources = new ResourceManager();
        
    }

    private void GameInit()
    {
        _currentPlayer = new Player("스파르타 아기");
        _currentPlayer.Inventory.AddItemName("OldSword");
        _currentPlayer.Inventory.AddItemName("CastIronArmor");
        _currentPlayer.Inventory.AddItemName("SteelSword");
        
        _currentVilliage = _resources.VilliageDataContainer.GetVilliage("Sparta");
        _printer.EndList.Add(" ");
        _printer.EndList.Add(_resources.StringContainer.GetString("RequestCommand"));
        
        Command.Init(_resources,_currentPlayer,_currentVilliage,_printer,_currentFunctionListIds,_inputMemory);
        _currentFunctionListIds.FunctionListIds =
            _resources.FunctionListContainer.GetFunctionList(_currentVilliage.FunctionIDs[0]);
        _functionReader.GetFunction(_currentFunctionListIds.FunctionListIds[0]).Execute();
    }

    private bool isGameOver = false;
    private void GameStart() 
    {
        int keyInput=0;
        _printer.Print();
        // 시작 화면 보여주기
        while (!isGameOver)
        {
            // 입력 받기
            Console.Write(">>");
            while (!TryGetKey(_currentFunctionListIds.Count, out keyInput))
            {
                Console.WriteLine("잘못 입력하셨습니다.");
                Console.Write(">>");
            }
            
            // 상태 변경, 변경된 상태 적용, 화면 보여주기
            //상태의 변경
            string functionName = _currentFunctionListIds.FunctionListIds[keyInput];
            _functionReader.GetFunction(functionName).Execute();
            
            // 화면 보여주기
            _printer.Print();
        }
    }

    private bool TryGetKey(int range, out int key)
    {
        key = 0;
        bool isOk = false;
        if (int.TryParse(Console.ReadLine(), out key))
        {
            if (1 <= key && key < range)
            {
                _inputMemory.preInput = key;
                isOk = true;
            }
        }
        
        return isOk;
    }
    
}


