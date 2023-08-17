// See https://aka.ms/new-console-template for more information

using System;

class Program
{
    private char[,] board = { {'1','2','3' },{'4','5','6'},{'7','8','9'} };
    int whatPlayer = 1;
    int ConsoleTopPos = 3;
    static void Main()
    {
        Program game = new Program();
        game.PlayGame();
    }

    public void PlayGame()
    {
        while (true)
        {
            // 테이블 세팅
            string strPlayerTurn = String.Format("플레이어 {0}의 차례 : ", whatPlayer);
            Console.WriteLine("번호를 골라주세요");
            Console.WriteLine("플레이어 1: X 와 플레이어 2: O\n");
            Console.WriteLine(strPlayerTurn);
            PrintBoard(whatPlayer);
            Console.SetCursorPosition(strPlayerTurn.Length*2, ConsoleTopPos);
            ConsoleTopPos = 3;
            
            // 입력 받기
            string input;
            input = Console.ReadLine();
            Console.Clear();
            
            // 입력이 유효할 경우, 설정 후에 체크까지
            int num;
            if (passInput(input, out num))
            {
                num -= 1;
                int x = num % board.GetLength(1);
                int y = num / board.GetLength(1);
                if (board[y, x] == 'X' || board[y, x] == 'O')
                {
                    Console.WriteLine("다시 골라주십시오");
                    ++ConsoleTopPos;
                    continue;
                }

                // 해당 위치에 값 지정
                board[y, x] = 'X';
                if (whatPlayer == 2)
                {
                    board[y, x] = 'O';
                }

                //이겼는지 체크
                int winPlayer;
                if (isWin(out winPlayer))
                {
                    Console.WriteLine($"플레이어{winPlayer}가 이겼습니다");
                    break;
                }

                // 플레이어 변경
                if (whatPlayer == 1)
                    whatPlayer = 2;
                else
                {
                    whatPlayer = 1;
                }
            }
        }
        
        return;
    }

    private bool isWin(out int winPlayer)
    {
        winPlayer = 0;
        // 가로 한줄 체크
        bool isWin = false;
        for (int i = 0; i< board.GetLength(0); i++)
        {
            char target = board[i, 0];
            if(!SetPlayer(target,out winPlayer))
                continue;

            for (int j = 1; j < board.GetLength(1); j++)
            {
                if (target == board[i, j])
                {
                    isWin = true;
                }
                else
                {
                    isWin = false;
                    break;
                }
            }

            if (isWin)
                return true;
        }
        
        
        // 세로 한줄 체크
        for (int i = 0; i< board.GetLength(1); i++)
        {
            char target = board[0,i];
            if(!SetPlayer(target,out winPlayer))
                continue;

            for (int j = 1; j < board.GetLength(0); j++)
            {
                if (target == board[j, i])
                {
                    isWin = true;
                }
                else
                {
                    isWin = false;
                    break;
                }
            }
            if (isWin)
                return true;
        }

        // 대각선 체크
        char crossTarget = board[0, 0];
        if (SetPlayer(crossTarget, out winPlayer))
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                if (crossTarget == board[i, i])
                    isWin = true;
                else
                {
                    isWin = false;
                    break;
                }
            }
            
            if (isWin)
                return true;
        }

        crossTarget = board[0,board.GetLength(1)-1];
        if (SetPlayer(crossTarget, out winPlayer))
        {
            for (int i = board.GetLength(0) - 1; i >= 0; --i)
            {
                int boardX = board.GetLength(1)-1;
                if (crossTarget == board[boardX-i,i])
                    isWin = true;
                else
                {
                    isWin = false;
                    break;
                }
            }
        }

        return isWin;
    }

    private bool passInput(string input, out int num)
    {
        if (int.TryParse(input, out num))
        {
            if (!(0 < num && num <= board.Length))
            {
                Console.WriteLine("범위를 제대로 입력해주세요");
                ++ConsoleTopPos;
                return false;
            }
        }
        return true;
    }
    private void PrintBoard(int player)
    {
        for (int i = 0; i < board.GetLength(0); i++)
        {
            Console.WriteLine("     |     |");
            Console.WriteLine("  {0}  |  {1}  |  {2}",board[i,0],board[i,1],board[i,2]);
            Console.WriteLine("_____|_____|_____");
        }
    }

    private bool SetPlayer(char target, out int player)
    {
        player = -1;
        if (target == 'X')
        {
            player = 1;
            return true;
        }else if (target == 'O')
        {
            player = 2;
            return true;
        }
        return false;
    }
    
}