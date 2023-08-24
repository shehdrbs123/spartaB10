public class Program
{
    public static void Main(string[] args)
    {
        int num;
        bool[] isSubmit;
        
        isSubmit = new bool[30];
        
        Array.Fill<bool>(isSubmit,false);
        
        for(int i=0;i<28;++i)
        {
            num = int.Parse(Console.ReadLine());
            isSubmit[num-1] = true;
        }
        
        for(int i=0;i<30;++i)
        {
            if(isSubmit[i] == false)
                Console.WriteLine(i+1);
        }
       
        
    }
}