using System;

namespace week2
{
    internal class Program
    {
        static char[] arr = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static int i = 0;
        static int player = 0;
        static int choice;
        static int flag = 0;
        static void Main(string[] args)
        {
            /*
            RandomGame
            */
            do
            {
                Console.Clear();
                Console.WriteLine("플레이어 1: X 와 플레이어 2: O\n");
                Console.WriteLine("플레이어 {0}의 차례\n", (player + 1));
                Board();
                Console.WriteLine(flag);
                do
                {
                    Console.WriteLine("입력하세요(1~9): ");
                    int num = int.Parse(Console.ReadLine()) - 1;
                    if (num < 9 || num >= 0)
                    {
                        if (arr[num] != 'X' && arr[num] != 'O')
                        {
                            if (player == 0)
                            {
                                arr[num] = 'X';
                            }
                            else if (player == 1)
                            {
                                arr[num] = 'O';
                            }
                            player++;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("죄송합니다. {0} 행은 이미 {1}로 표시되어 있습니다.\n다시 입력하세요", (num + 1), arr[num]);
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("유효하지 않는 행입니다");
                    }
                } while (true);
                flag = CheckWin();
                player %= 2;
            } while (flag==0);
            Console.Clear();
            Console.WriteLine("플레이어 1: X 와 플레이어 2: O\n");
            Board();
            if (flag == 1)
            {
                Console.WriteLine("플레이어{0}이 승리하였습니다",player);
            }
            else if (flag == -1)
            {
                Console.WriteLine("무승부");
            }
        }

        static void RandomGame()
        {
            Random random = new Random();
            int num = new Random().Next(1, 101);
            int attempt = 0;    //시도횟수
            int guesses = 0;

            Console.WriteLine("숫자 맞추기 게임을 시작합니다. 1에서 100까지의 숫자 중 하나를 맞춰보세요.");
            while (guesses != num)
            {
                Console.Write("숫자를 입력하세요: ");
                attempt++;
                guesses = int.Parse(Console.ReadLine());
                if (guesses > num)
                {
                    Console.WriteLine("너무 큽니다!");
                }
                else if (guesses < num)
                {
                    Console.WriteLine("너무 작습니다!");
                }
                else
                {
                    Console.WriteLine("축하합니다! {0}번 만에 숫자를 맞추었습니다.", attempt);
                }
            }
        }

        static void Board()
        {
            Console.WriteLine("     |    |    ");
            Console.WriteLine($"  {arr[0]}  |  {arr[1]} |  {arr[2]}  ");
            Console.WriteLine("_____|____|____");
            Console.WriteLine("     |    |    ");
            Console.WriteLine($"  {arr[3]}  |  {arr[4]} |  {arr[5]}  ");
            Console.WriteLine("_____|____|____");
            Console.WriteLine("     |    |    ");
            Console.WriteLine($"  {arr[6]}  |  {arr[7]} |  {arr[8]}  ");
            Console.WriteLine("     |    |    ");
        }

        static int CheckWin()
        {
            for(int i = 0; i < 9; i+=3) //가로승리
            {
                if (arr[i] == arr[i+1] && arr[i+1] == arr[i+2])
                {
                    return 1;
                }
            }
            for(int i = 0; i < 3; i++)  //세로승리
            {
                if (arr[i] == arr[i+3] && arr[i+3] == arr[i + 6])
                {
                    return 1;
                }
            }
            if (arr[0] == arr[4] && arr[0] == arr[8] || arr[2] == arr[4] && arr[2] == arr[6])
            {
                return 1;
            }
            else if (arr[0] != '1' && arr[1] != '2' && arr[2] != '3' && arr[3] != '4' && arr[4] != '5' && arr[5] != '6' &&
                arr[6] != '7' && arr[7] != '8' && arr[8] != '9')
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}