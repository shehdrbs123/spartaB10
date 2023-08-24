public class Program
{
    public static void Main(string[] args)
    {
        int basketCount = 0;
        int inputCount = 0;
        int[]? basket = null;
        string[]? inputString = null;
        
        inputString = Console.ReadLine().Split();
        basketCount = int.Parse(inputString[0]);
        inputCount = int.Parse(inputString[1]);
        
        basket = Enumerable.Range(1,basketCount).ToArray();
        
        for(int i=0;i<inputCount;++i)
        {
            int startIdx=0;
            int endIdx=0;
            
            inputString = Console.ReadLine().Split();
            startIdx = int.Parse(inputString[0])-1;
            endIdx = int.Parse(inputString[1])-1;
                        
            while(startIdx <= endIdx)
            {
                int tmp = basket[startIdx];
                basket[startIdx] = basket[endIdx];
                basket[endIdx] = tmp;
                ++startIdx;
                --endIdx;
            }
        }
          
        Array.ForEach(basket,x=> Console.Write($"{x} "));
    }
}