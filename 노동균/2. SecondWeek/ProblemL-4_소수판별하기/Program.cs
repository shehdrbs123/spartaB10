// See https://aka.ms/new-console-template for more information

using System;

class Program
{
    // 주어진 숫자가 소수인지 판별하는 함수
    static bool IsPrime(int num)
    {
        // TODO::코드를 작성하세요
        int targetNum = (int)Math.Sqrt(num);
        for (int i = 2; i < targetNum; i++)
        {
            if (num / i == 0)
            {
                return true;
            }
        }
        return false;
    }

    static void Main()
    {
        Console.Write("숫자를 입력하세요: "); // 사용자에게 숫자 입력 요청
        int num = int.Parse(Console.ReadLine()); // 사용자가 입력한 값을 정수로 변환하여 저장

        if (IsPrime(num)) // 입력 받은 숫자가 소수라면
        {
            Console.WriteLine(num + "은 소수입니다."); // 소수임을 출력
        }
        else // 소수가 아니라면
        {
            Console.WriteLine(num + "은 소수가 아닙니다."); // 소수가 아님을 출력
        }
    }
}