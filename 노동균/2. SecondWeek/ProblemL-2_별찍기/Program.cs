// See https://aka.ms/new-console-template for more information

public class Program
{
    public static void Main(string[] args)
    {
        int starCount;
        Program starWriter = new Program();
        Console.Write("별을 몇개 찍을 까요? : ");

        starCount = int.Parse(Console.ReadLine());
        
        starWriter.WriteStar(starCount);
        
    }

    public void WriteStar(int n)
    {
        WriteDirection(n);
        WriteInverseDirection(n);
        WritePyramid(n);
    }

    private void WriteDirection(int n)
    {
        for (int i = 0; i <= n; i++)
        {
            for (int j = 0; j < i; j++)
            {
                Console.Write("*");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    private void WriteInverseDirection(int n)
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = n-i; j > 0 ; j--)
            {
                Console.Write("*");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    private void WritePyramid(int n)
    {
        int midPos = n-1;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n*2 ; j++)
            {
                if (midPos - i <= j && j <= midPos + i)
                {
                    Console.Write("*");
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}