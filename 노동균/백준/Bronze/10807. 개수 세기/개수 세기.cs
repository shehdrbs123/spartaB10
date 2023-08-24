public class Program
{
    public static void Main(string[] args)
    {
        int numCount=0;
        string[]? nums=null;
        int numToFind=0;
        int findCount=0;
        
        numCount = int.Parse(Console.ReadLine());
        nums = Console.ReadLine().Split(' ');
        numToFind = int.Parse(Console.ReadLine());
        for(int i=0;i<numCount;++i)
        {
            int num = int.Parse(nums[i]);
            if(numToFind == num)
                ++findCount;
        }
        
        Console.WriteLine(findCount);
    }
}