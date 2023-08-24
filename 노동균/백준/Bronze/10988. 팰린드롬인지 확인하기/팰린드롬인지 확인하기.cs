public class Program
{
    public static void Main(string[] args)
    {
        string inputString;
        int start=0;
        int end=0;
        int result = 1;
        
        inputString = Console.ReadLine();
        end = inputString.Length-1;
        
        while(start < end)
        {
            if(inputString[start] != inputString[end])
            {
                result = 0;
                break;
            }
            ++start;
            --end;
        }
        
        Console.WriteLine(result);
    }
}