// See https://aka.ms/new-console-template for more information

public class Program
{
    public static void Main(string[] args)
    {
        int selectedNum;
        

        selectedNum = new Random().Next(0, 101);
        while (true)
        {
            int userNum=-1;
            while (!(0 <= userNum && userNum <= 100))
            {
                Console.Write("숫자를 골라주세요 (0~100) : "); 
                if (!int.TryParse(Console.ReadLine(), out userNum))
                {
                    Console.WriteLine("숫자 맞춰서 입력해주세요");  
                }
            }

            if (selectedNum > userNum)
            {
                Console.WriteLine($"{userNum}보다 큽니다.");
            }else if (selectedNum < userNum)
            {
                Console.WriteLine($"{userNum}보다 작습니다.");
            }
            else
            {
                Console.WriteLine("정답입니다");
                break;
            }
        }
    }    
}
