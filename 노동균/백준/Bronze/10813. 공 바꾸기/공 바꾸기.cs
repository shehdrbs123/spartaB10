public class Program
{
    public static void Main(string[] args)
    {
        int basketCount = 0;
        int inputCount = 0;
        int[]? basket = null;
        string[]? inputString = null;
        
        inputString = Console.ReadLine().Split(' ');
        basketCount = int.Parse(inputString[0]);
        inputCount = int.Parse(inputString[1]);
        
        basket = new int[basketCount];
        for(int i=0;i<basketCount;++i)
            basket[i] = i+1;
        
        for(int i=0;i<inputCount;++i)
        {
            int rIdx = 0;
            int lIdx = 0;
            int tmp = 0;
            
            inputString = Console.ReadLine().Split(' ');
            rIdx = int.Parse(inputString[0])-1;
            lIdx = int.Parse(inputString[1])-1;
            
            tmp = basket[rIdx];
            basket[rIdx] = basket[lIdx];
            basket[lIdx] = tmp;
        }
        
        Array.ForEach(basket,x=> Console.Write($"{x} "));
    }
}