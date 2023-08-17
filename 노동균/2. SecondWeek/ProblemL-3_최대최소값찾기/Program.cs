// See https://aka.ms/new-console-template for more information

public class Program
{
    public static void Main(string[] args)
    {
        int numCount;
        int[] nums;
        int max=0;
        int min=int.MaxValue;
        Program starWriter = new Program();
        Console.Write("입력을 몇개 받으실 겁니까? : ");
        numCount = int.Parse(Console.ReadLine());
        nums = new int[numCount];

        for (int i = 0; i < numCount; i++)
        {
            Console.Write("숫자를 입력하세요 : ");
            nums[i] = int.Parse(Console.ReadLine());
        }

        max = nums.Max();
        min = nums.Min();
        Console.WriteLine($"최대값 : {max}");
        Console.WriteLine($"최소값 : {min}");
        
    }
}