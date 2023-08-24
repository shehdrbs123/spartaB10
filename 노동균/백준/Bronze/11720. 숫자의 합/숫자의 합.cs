public class Program
{
    public static void Main(string[] args)
    {
        int stringLength;
        string inputString;
        int result = 0;
        stringLength = int.Parse(Console.ReadLine());
        inputString = Console.ReadLine();
        
        for(int i=0;i<stringLength;++i)
        {
            int num = inputString[i]-'0';
            result+=num;
        }
        
        Console.WriteLine(result);
    }
}