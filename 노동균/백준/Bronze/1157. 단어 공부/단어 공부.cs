public class Program
{
    public static void Main(string[] args)
    {
        string inputString;
        int highNum=0;
        char highKey='?';
        inputString = Console.ReadLine();
        inputString = inputString.ToUpper();
        char[] arChars = inputString.ToArray();



        var GroupList =
            from n in arChars
            group n by n
            into c
            select new
            {
                Alpha = c.Key,
                Count = c.Count()
            }
            into d
            orderby d.Count descending
            select d;
            
        var test = GroupList.ToArray();
        if (test.Length == 1 || test.Length > 1 && test[0].Count != test[1].Count)
        {
            highKey = test[0].Alpha;
        }
        Console.WriteLine(highKey);
    }
}