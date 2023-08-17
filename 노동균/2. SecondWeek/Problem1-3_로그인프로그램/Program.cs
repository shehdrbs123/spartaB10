// See https://aka.ms/new-console-template for more information

// See https://aka.ms/new-console-template for more information

using System;

public class RandomGetUser
{
    public static void Main(string[] args)
    {
        string id = "myid";
        string password = "mypassword";
        string inputid;
        string inputPassword;
        
        Console.Write("아이디 : ");
        inputid = Console.ReadLine();
        Console.Write("패스워드 : ");
        inputPassword = Console.ReadLine();

        if (id.CompareTo(inputid) == 0 && password.CompareTo(inputPassword) == 0)
        {
            Console.WriteLine($"{id}님이 입장하셨습니다.");
        }
        else
        {
            Console.WriteLine("아이디나 패스워드가 틀렸습니다");
        }
    }
}