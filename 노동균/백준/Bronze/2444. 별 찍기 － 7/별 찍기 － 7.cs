public class Program
{
    public static void Main(string[] args)
    {
        int count;
        int middle;
        int i=0;
        count = int.Parse(Console.ReadLine());
        middle = count -1;
               
        
        for(;i<count;++i)
        {
             for(int j=0;j<count*2;j++)
             {
                 if(middle-i <= j  && j <= middle+i )
                     Console.Write("*");
                 else if(j <= middle+i)
                     Console.Write(" ");
             }
            Console.WriteLine();
        }
        
        for(i-=2;i>=0;--i)
        {
            for(int j=0;j<count*2;j++)
             {
                 if(middle-i <= j  && j <= middle+i )
                     Console.Write("*");
                 else if(j <= middle+i)
                     Console.Write(" ");
             }
            if(i!=0)
                Console.WriteLine();
        }
    }
}