// See https://aka.ms/new-console-template for more information

public class Program
{
    public static void Main(string[] args)
    {
        for (int i = 1; i < 10; i++)
        {
            for (int j = 2; j < 10; j++)
            {
                Console.Write($"{j,2} * {i,2} = {i * j,2}  ");
            }
            Console.WriteLine();
        }
    }    
}