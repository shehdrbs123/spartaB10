using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

class Program
{
    static void Main(string[] args)
    {
        void DrawMap(int w, int h)
        {
            for (int i = 0; i <= w; i += 2)
            {
                Point p1 = new Point(i, 0, '□');
                Point p2 = new Point(i, h, '□');
                p1.Draw();
                p2.Draw();
            }
            for (int i = 0; i <= h; i++)
            {
                Point p1 = new Point(0, i, '□');
                Point p2 = new Point(w, i, '□');
                p1.Draw();
                p2.Draw();
            }
        }

        int mapWidth = 80;
        int mapHeight = 20;

        DrawMap(mapWidth, mapHeight);

        // 뱀의 초기 위치와 방향을 설정하고, 그립니다.
        Point p = new Point(4, 5, '*');
        Snake snake = new Snake(p, 4, Direction.RIGHT);
        snake.Draw();

        // 음식의 위치를 무작위로 생성하고, 그립니다.
        FoodCreator foodCreator = new FoodCreator(mapWidth, mapHeight, '$');
        Point food = foodCreator.CreateFood(snake.body);
        food.Draw();


        while (true)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey keyInput = Console.ReadKey(true).Key;

                if (keyInput == ConsoleKey.LeftArrow)
                {
                    snake.currentDir = Direction.LEFT;
                }
                else if (keyInput == ConsoleKey.RightArrow)
                {
                    snake.currentDir = Direction.RIGHT;
                }
                else if (keyInput == ConsoleKey.UpArrow)
                {
                    snake.currentDir = Direction.UP;
                }
                else if (keyInput == ConsoleKey.DownArrow)
                {
                    snake.currentDir = Direction.DOWN;
                }
            }

            snake.Move();

            // 음식 먹었을때 처리
            if(snake.head.IsHit(food))
            {
                snake.AddTail();
                food = foodCreator.CreateFood(snake.body);
                food.Draw();
            }

            // 머리가 몸통을 박았을 때 처리
            if (snake.HeadHitBody())
            {
                return;
            }

            // 머리가 맵 가장자리에 닿을 시 처리
            if(snake.head.x == 0 || snake.head.x == mapWidth ||
                snake.head.y == 0 || snake.head.y == mapHeight)
                return;


            Thread.Sleep(100); // 게임 속도 조절

            // 뱀의 상태를 출력
            Console.SetCursorPosition(0, 22);
            Console.Write("현재 길이 : {0}         |          먹은 음식 : {1}", snake.length, snake.length - 4);
        }
    }
}

public class Snake
{
    public Point head = new Point(0, 0, '*');
    public List<Point> body = new List<Point>(); 
    public int length = 0;
    public Direction currentDir;

    public Snake(Point initPos, int initLength, Direction direction)
    {
        head = initPos;
        length = initLength;
        currentDir = direction;

        for(int i = 0; i < length; i++)
        {
            Point newBody = new Point(head.x, head.y, 'o');
            body.Add(newBody);
        }
    }

    public bool HeadHitBody()
    {
        for(int i = 0; i < body.Count; i++)
        {
            if (body[i].IsHit(head))
            {
                return true;
            }
        }

        return false;
    }

    public void Draw()
    {
        head.Draw();
        for(int i = 0; i < body.Count; i++)
        {
            body[i].Draw();
        }
    }

    public void AddTail()
    {
        Point newBody = new Point(body[body.Count - 1].x, body[body.Count - 1].y, 'o');
        body.Add(newBody);

        length += 1;
    }

    public void Move()
    {
        // 새 위치를 그려주기위해 기존의 헤드와 바디를 지움
        head.Clear();
        for(int i = 0; i < body.Count; i++)
        {
            body[i].Clear();
        }

        // 바디가 헤드를 따라가게 만들기 위해 각 꼬리는 앞 꼬리의 위치 정보를 받아 새 위치 갱신
        for(int i = body.Count-1; i >= 1; i--)
        {
            body[i].x = body[i - 1].x;
            body[i].y = body[i - 1].y;
        }
        body[0].x = head.x;
        body[0].y = head.y;

        switch(currentDir)
        {
            case Direction.LEFT:
                head.x -= 1;
                break;
            case Direction.RIGHT:
                head.x += 1;
                break;
            case Direction.UP:
                head.y -= 1;
                break;
            case Direction.DOWN:
                head.y += 1;
                break;
        }

        // 새 위치에서 새로 그려주기
        head.sym = '*';
        head.Draw();
        for (int i = 0; i < body.Count; i++)
        {
            body[i].sym = 'o';
            body[i].Draw();
        }
    }
}

public class FoodCreator
{
    private int xLimit;
    private int yLimit;
    private char sym;
    public FoodCreator(int xLimit, int yLimit, char sym)
    {
        this.xLimit = xLimit;
        this.yLimit = yLimit;
        this.sym = sym;
    }

    public Point CreateFood(List<Point> snakeBody)
    {
        bool creatable = false;
        Point foodPos = new Point(0, 0, sym);

        while (!creatable)
        {
            creatable = true;

            int posx = new Random().Next(1, xLimit - 1);
            int posy = new Random().Next(1, yLimit - 1);

            foodPos = new Point(posx, posy, sym);

            for(int i = 0; i < snakeBody.Count; i++)
            {
                if (snakeBody[i].IsHit(foodPos))
                {
                    creatable = false;

                    break;
                }
            }
        }

        return foodPos;
    }
}

public class Point
{
    public int x { get; set; }
    public int y { get; set; }
    public char sym { get; set; }

    // Point 클래스 생성자
    public Point(int _x, int _y, char _sym)
    {
        x = _x;
        y = _y;
        sym = _sym;
    }

    // 점을 그리는 메서드
    public void Draw()
    {
        Console.SetCursorPosition(x, y);
        Console.Write(sym);
    }

    // 점을 지우는 메서드
    public void Clear()
    {
        sym = ' ';
        Draw();
    }

    // 두 점이 같은지 비교하는 메서드
    public bool IsHit(Point p)
    {
        return p.x == x && p.y == y;
    }
}
// 방향을 표현하는 열거형입니다.
public enum Direction
{
    LEFT,
    RIGHT,
    UP,
    DOWN
}
