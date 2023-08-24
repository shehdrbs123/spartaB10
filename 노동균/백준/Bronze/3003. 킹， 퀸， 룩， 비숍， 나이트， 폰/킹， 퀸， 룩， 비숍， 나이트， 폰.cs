public class Program
{
    public static void Main(string[] args)
    {
        int[] chessPiecesCount = new int[] {1,1,2,2,2,8};
        string[] inputString;
        
        inputString = Console.ReadLine().Split();
        
        for(int i=0;i<inputString.Length;++i)
        {
            int count;
            
            count = int.Parse(inputString[i]);
            chessPiecesCount[i] -= count;
            
        }
        
        Array.ForEach(chessPiecesCount,x => Console.Write($"{x} "));
        
    }
}