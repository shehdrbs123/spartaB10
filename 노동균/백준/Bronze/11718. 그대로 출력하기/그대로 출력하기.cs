public class Program
{
    public static void Main(string[] args)
    {
        while(true)
        {
            string data = Console.ReadLine();
            if(data == null) 
                break;
            else
                Console.WriteLine(data);
        }
    }
}