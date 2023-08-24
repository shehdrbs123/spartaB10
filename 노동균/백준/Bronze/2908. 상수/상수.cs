public class Program
{
    public static void Main(string[] args)
    {
        string[] inputString;
        string strNum1;
        string strNum2;
        int intNum1;
        int intNum2;
        int result;
        
        inputString = Console.ReadLine().Split();
        strNum1 = new String(inputString[0].Reverse().ToArray());
        strNum2 = new String(inputString[1].Reverse().ToArray());
        
        intNum1 = int.Parse(strNum1);
        intNum2 = int.Parse(strNum2);
        
        result = intNum1;
        if(intNum1 < intNum2)
            result = intNum2;
        
        Console.WriteLine(result);
    }
}