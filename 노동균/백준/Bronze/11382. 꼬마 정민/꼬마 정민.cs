public class Program
{
    public static void Main(string[] args)
    {
        string[] nums = new string[1];
        ulong result = 0;
        
        nums = Console.ReadLine().Split(' ');
        
        for(int i=0;i<nums.Length;++i)
        {
            ulong num = ulong.Parse(nums[i]);
            result += num;
        }
        
        Console.WriteLine(result);
    }
}