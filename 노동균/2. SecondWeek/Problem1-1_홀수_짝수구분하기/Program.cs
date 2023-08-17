// See https://aka.ms/new-console-template for more information

using System;

public class RandomGetUser
{
    public static void Main(string[] args)
    {
        int number;
        
        Console.Write("숫자를 입력하면 홀/짝 구분을 해드립니다 : ");
        number = int.Parse(Console.ReadLine());
        if ((number %= 2) == 0)
        {
            Console.WriteLine("짝수 입니다.");
        }
        else
        {
            Console.WriteLine("홀수 입니다.");
        }
    }
}