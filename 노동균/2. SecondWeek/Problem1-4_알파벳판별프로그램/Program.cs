// See https://aka.ms/new-console-template for more information

using System;

public class RandomGetUser
{
    public static void Main(string[] args)
    {
        char alpha;
        
        Console.Write("알파벳 입력 (한글자) : ");
        alpha = Console.ReadLine()[0];
        //아스키 코드표를 보면 해당 알고리즘을 이해할 수 있다.
        if ('a' <= alpha && alpha <= 'z' || 'A' <= alpha && alpha <= 'Z')
        {
            Console.WriteLine("이것은 알파벳입니다");
        }
        else
        {
            Console.WriteLine("이것은 알파벳이 아닙니다");
        }
    }
}