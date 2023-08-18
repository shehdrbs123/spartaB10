using System;

namespace PrivateProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while(true){
                StartScene();
                Console.Clear();
            }
        }

        public static void StartScene()
        {
            Console.WriteLine("\n스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리\n");
            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
            string actionStr = Console.ReadLine();
            int actionNum = 0;
            int.TryParse(actionStr, out actionNum);

            while (actionNum != 1 && actionNum != 2)
            {
                Console.Write("\n잘못 입력하셧습니다. \n다시 입력하세요\n>> ");
                actionStr = Console.ReadLine();
                int.TryParse(actionStr, out actionNum);
            }

            Console.WriteLine();

            if (actionNum == 1)
            {
                Console.WriteLine("====================================");
                Console.WriteLine("    상태 보기 창으로 이동합니다.");
                Console.WriteLine("====================================");
            }
            else if(actionNum == 2)
            {
                Console.WriteLine("====================================");
                Console.WriteLine("    인벤토리 창으로 이동합니다.");
                Console.WriteLine("====================================");
            }
            Thread.Sleep(1000);
        }
    }

    
}