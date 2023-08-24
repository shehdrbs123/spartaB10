public class Program
{
    public static void Main(string[] args)
    {
        int basketCount=0;
        int pushCount=0;
        int pushStart=0;
        int pushEnd=0;
        int ballNum=0;
        int[]? ballBasket;
        string[]? startInput;
        
        startInput = Console.ReadLine().Split(' ');
        
        basketCount = int.Parse(startInput[0]);
        pushCount = int.Parse(startInput[1]);
        
        ballBasket = new int[basketCount];
        
        for(int i=0;i<pushCount;++i)
        {
            string[]? pushString;                
                
            pushString = Console.ReadLine().Split(' ');
            pushStart = int.Parse(pushString[0])-1;
            pushEnd = int.Parse(pushString[1])-1;
            ballNum = int.Parse(pushString[2]);
            
            for(int j =pushStart; j <= pushEnd;++j)
                ballBasket[j] = ballNum;
        }
        
        Array.ForEach(ballBasket, num => Console.Write($"{num} "));
    }
}