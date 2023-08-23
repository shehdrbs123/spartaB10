
using System.Text;

public class ConsoleTypingPrinter
{
    private enum ETypePos
    {
        Top,Info,Select,End
    }
    private int _typeSpeed;
    private List<List<string>> _showList;

    public List<ConsolePaint> Paints
    {
        get;
        private set;
    }
    
    public List<string> TopList
    {
        get
        {
            return _showList[(int)ETypePos.Top];
        }
        private set
        {
            _showList[(int)ETypePos.Top] = value;
        }
    }
    
    public List<string> InfoList
    {
        get
        {
            return _showList[(int)ETypePos.Info];
        }
        private set
        {
            _showList[(int)ETypePos.Info] = value;
        }
    }
    
    public List<string> SelectList
    {
        get
        {
            return _showList[(int)ETypePos.Select];
        }
        private set
        {
            _showList[(int)ETypePos.Select] = value;
        }
    }
    
    public List<string> EndList
    {
        get
        {
            return _showList[(int)ETypePos.End];
        }
        private set
        {
            _showList[(int)ETypePos.End] = value;
        }
    }
    
    public ConsoleTypingPrinter(int typeSpeed)
    {
        _typeSpeed = typeSpeed;
        _showList = new List<List<string>>();
        _showList.Capacity = 4;
        Paints = new List<ConsolePaint>();

        for (int i = 0; i < _showList.Capacity; i++)
        {
            _showList.Add(new List<string>());
        }
        
        TopList.Capacity = 10;
        InfoList.Capacity = 10;
        SelectList.Capacity = 10;
        EndList.Capacity = 2;
    }

    public void Print()
    {
        Console.Clear();
        bool isWriteAll = false;
        int paintsIdx = 0;
        foreach (var writeList in _showList)
        {
            for (int line = 0; line < writeList.Count(); ++line)
            {
                string writeString = writeList[line];
                for (int charIdx = 0; charIdx < writeString.Length; charIdx++)
                {
                    if (Console.KeyAvailable)
                    {
                        Console.ReadKey(true);
                        isWriteAll = true;
                    }
                        
                    if(isWriteAll == false)
                        Thread.Sleep(_typeSpeed);
                    ConsoleColor foreColor = ConsoleColor.White;
                    ConsoleColor backColor = ConsoleColor.Black;
                    if (Paints.Count > 0 && paintsIdx < Paints.Count )
                    {
                        ConsolePaint paints = Paints[paintsIdx];
                        if (paints.Line == line && paints.Start <= charIdx && charIdx <= paints.End)
                        {
                            foreColor = paints.ForeColor;
                            backColor = paints.BackColor;
                        }

                        if (paints.End == charIdx)
                        {
                            paintsIdx++;
                            Console.ResetColor();
                        }
                            
                    }
                    WriteType(writeString[charIdx],foreColor,backColor);
                }
                Console.WriteLine();
            }
        }
        Clear();
    }

    private void Clear()
    {
        TopList.Clear();
        InfoList.Clear();
        SelectList.Clear();
        Paints.Clear();
    }
    
    private void WriteType(char data, ConsoleColor foreColor = ConsoleColor.White, ConsoleColor backColor =ConsoleColor.Black)
    {
        Console.ForegroundColor = foreColor;
        Console.BackgroundColor = backColor;
        Console.Write(data);
    }
}

public struct ConsolePaint
{
    public ConsolePaint(int line, int start, int end, ConsoleColor foreColor, ConsoleColor backColor,bool isBold = false)
    {
        Line = line;
        Start = start;
        End = end;
        ForeColor = foreColor;
        BackColor = backColor;
        IsBold = isBold;
    }
    public int Line;
    public int Start;
    public int End;
    public ConsoleColor ForeColor;
    public ConsoleColor BackColor;
    public bool IsBold;
}