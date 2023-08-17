// See https://aka.ms/new-console-template for more information

using System;

public class Program
{
    public static void Main(string[] args)
    {
        string[] strRockPaperScissors = { "가위", "바위", "보" };
        int enemyIdx;
        string enemy;

        enemyIdx = new Random().Next(0, 3);
        enemy = strRockPaperScissors[enemyIdx];

        while (true)
        {
            int userIdx = -1;

            while ( !(0 <= userIdx && userIdx < 3))
            {
                Console.Write("가위 : 0, 바위 : 1, 보 : 2  => ");
                userIdx = int.Parse(Console.ReadLine());
            }
            if (enemyIdx == userIdx)
            {
               Console.WriteLine("비겼습니다");
               break;
            }else if (enemyIdx == 0 && userIdx == 1 ||
                      enemyIdx == 1 && userIdx == 2 ||
                      enemyIdx == 2 && userIdx == 0)
            {
                Console.WriteLine("이겼습니다");
            }
            else
            {
                Console.WriteLine("졌습니다");            
            }
        }
    }
}