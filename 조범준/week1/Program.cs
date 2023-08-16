namespace week1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* 사용자로부터 입력 받기
            Console.Write("이름을 입력하세요: ");
            string str = Console.ReadLine();
            Console.Write("나이를 입력하세요: ");
            int num = int.Parse(Console.ReadLine());
            Console.WriteLine("안녕하세요, {0}! 당신은 {1}세 이군요.", str, num);
            */

            /* 간단한 사칙연산 계산기 만들기
            Console.Write("첫번째 수를 입력하세여: ");
            int num1 = int.Parse(Console.ReadLine());
            Console.Write("두번째 수를 입력하세요: ");
            int num2 = int.Parse(Console.ReadLine());
            Console.WriteLine("더하기: {0}"(num1 + num2));
            Console.WriteLine("빼기: {0}", (num1 - num2));
            Console.WriteLine("곱하기: {0}", (num1 * num2));
            Console.WriteLine("나누기: {0}", (num1 / num2));
            */

            /* 온도 변환기
            Console.Write("섭씨 온도를 입력하세요: ");
            int num1 = int.Parse(Console.ReadLine());
            float num2 = num1 * 1.8f + 32;
            Console.Write("변환된 화씨 온도: {0}", (int)num2);
            */

            /*BMI 계산기 만들기
            Console.Write("키(m) : ");
            double num1 = double.Parse(Console.ReadLine());
            Console.Write("몸무게(kg) : ");
            double num2 = double.Parse(Console.ReadLine());
            double num3 = num2 / num1;
            num3 = num2 / (num1 * num1);
            Console.WriteLine("BMI : {0}", num3);
            */
        }
    }
}