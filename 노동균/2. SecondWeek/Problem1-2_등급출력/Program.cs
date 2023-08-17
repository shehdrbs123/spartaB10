// See https://aka.ms/new-console-template for more information

using System;

public class RandomGetUser
{
    public static void Main(string[] args)
    {
        int score = 0;
        string grade;
        
        Console.Write("점수를 입력하면 등급을 반환합니다(0~100) : ");
        score = int.Parse(Console.ReadLine());
        
        grade = GetGradeWithIf(score);
        
        Console.WriteLine($"등급은 {grade} 입니다");        
    }

    public static string GetGradeWithSwitch(int score)
    {
        string grade = "Bronze";
        int tensScore = score / 10;
        switch (tensScore)
        {
            case 10 :
                grade = "Challenger";
                break;
            case 9 :
                grade = "GrandMaster";
                break;
            case 8 :
                grade = "Master";
                break;
            case 7 :
                grade = "Diamond";
                break;
            case 6 :
                grade = "Platinum";
                break;
            case 5 :
                grade = "Gold";
                break;
            case 4 :
                grade = "Silver";
                break;
        }

        return grade;
    }

    public static string GetGradeWithIf(int score)
    {
        string grade = "Bronze";
        if (score == 100)
            grade = "Challenger";
        else if (score >= 90)
            grade = "GrandMaster";
        else if (score >= 80)
            grade = "Master";
        else if (score >= 70)
            grade = "Diamond";
        else if (score >= 60)
            grade = "Platinum";
        else if (score >= 50)
            grade = "Gold";
        else if (score >= 40)
            grade = "Silver";

        return grade;
    }
}