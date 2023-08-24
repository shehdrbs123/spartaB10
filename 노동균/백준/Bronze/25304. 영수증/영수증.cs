using System;
public class Program
{
    public static void Main(string[] args)
    {
        int printedGold=0;
        int totalGold=0;
        int printedCount=0;
        
        printedGold = int.Parse(Console.ReadLine());
        printedCount = int.Parse(Console.ReadLine());
        
        for(int i=0;i<printedCount;++i)
        {
            string[] check;
            int gold;
            int Amount;
            
            check = Console.ReadLine().Split(' ');
            gold = int.Parse(check[0]);
            Amount = int.Parse(check[1]);
            totalGold += gold * Amount;
        }
        
        if(printedGold == totalGold)
        {
            Console.WriteLine("Yes");
        }else
        {
            Console.WriteLine("No");
        }
    }
}