// See https://aka.ms/new-console-template for more information

//using System;
using System;
namespace HelloWorld
{
    internal class program
    {
        void Main(string[] args)
        {
            /////////////////////////////////
            // string name;
            // int age;
            //
            // Console.Write("이름 : ");
            // name = Console.ReadLine();
            // Console.Write("나이 : ");
            // age = int.Parse(Console.ReadLine());
            //
            // Console.WriteLine("이름은 {0} 이고 나이는 {1}살입니다",name, age);
            
            // 두 수를 입력받고 사칙연산 계산기
            //
            // int num1, num2, result;
            //
            // Console.Write("첫번째 수 : ");
            // num1 = int.Parse(Console.ReadLine());
            // Console.Write("두번째 수 : ");
            // num2 = int.Parse(Console.ReadLine());
            //
            // result = num1 + num2;
            // Console.WriteLine($"{num1}와 {num2}의 합은 {num1+num2}입니다");
            
            // 온도 변환기 만들기
            // int Celsius;
            // int Fahrenheit;
            // Console.Write("섭씨 온도를 입력하세요 ");
            // Celsius = int.Parse(Console.ReadLine());
            // Fahrenheit = Celsius * 9 / 5 + 32;
            // Console.WriteLine($"변환된 화씨의 온도 {Fahrenheit}");
            
            int tall;
            int weight;
            double BMI;

            Console.Write("키 : ");
            tall = int.Parse(Console.ReadLine());
            Console.Write("몸무게 : ");
            weight = int.Parse(Console.ReadLine());

            BMI = (double)weight/ ((double)tall*(double)tall);
            Console.WriteLine($"BMI : {BMI}");
        }
    }
}