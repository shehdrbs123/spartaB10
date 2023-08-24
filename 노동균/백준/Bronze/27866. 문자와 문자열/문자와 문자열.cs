public class Program
{
    public static void Main(string[] args)
    {
        string str=null;
        int num=0;
        
        str = Console.ReadLine();
        num = int.Parse(Console.ReadLine());
        
        Console.WriteLine(str[num-1]);
    }
}