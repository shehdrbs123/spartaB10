public class Program
{
    public static void Main(string[] args)
    {
        int bytes=0;
        int forCount = 0;
        
        bytes = int.Parse(Console.ReadLine());
        forCount = bytes/4;
        
        for(int i=0;i<forCount; ++i)
        {
            Console.Write("long ");
        }
        Console.Write("int");
    }
}