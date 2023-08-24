public class Program
{
     public static void Main(string[] args)
     {
         int inputCount=0;
         string inputString;
         
         inputCount = int.Parse(Console.ReadLine());
         
         for(int i=0;i<inputCount;++i)
         {
             int Last;
             
             inputString = Console.ReadLine();
             Last = inputString.Length-1;             
            
             Console.WriteLine($"{inputString[0]}{inputString[Last]}");
         }
     }
}