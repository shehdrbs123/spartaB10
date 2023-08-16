using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace week3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int foodCount = 0;
            int mapx = 80, mapy = 20;
            int gameSpeed = 200; 
            DrawWalls(mapx, mapy, foodCount);
            Point p = new Point(4, 5, '*');
            Snake snake = new Snake(p, 4, Direction.RIGHT);

            FoodCreator foodCreator = new FoodCreator(mapx, mapy, '$');
            Point food = foodCreator.Create();
            food.Draw();

            snake.Draw();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;

                    switch (key)
                    {
                        case ConsoleKey.UpArrow:
                            snake.direction = Direction.UP;
                            break;
                        case ConsoleKey.DownArrow:
                            snake.direction = Direction.DOWN;
                            break;
                        case ConsoleKey.LeftArrow:
                            snake.direction = Direction.LEFT;
                            break;
                        case ConsoleKey.RightArrow:
                            snake.direction = Direction.RIGHT;
                            break;
                    }
                }
                if (snake.Eat(food))
                {
                    foodCount++;
                    food.Draw();

                    food = foodCreator.Create();
                    food.Draw();
                    if (gameSpeed > 10)
                    {
                        gameSpeed -= 10;
                    }
                }
                else
                {
                    snake.Move();
                }
                Console.SetCursorPosition(0, mapy + 1);
                Console.Write("먹은 음식 양 : {0}", foodCount);
                if (snake.Hit_Tail() || snake.Hit_Wall())
                {
                    break;
                }

                Thread.Sleep(gameSpeed);
            }
            WriteGameOver();
        }

        static void WriteGameOver()
        {
            int xOffset = 25;
            int yOffset = 22;
            Console.SetCursorPosition(xOffset, yOffset++);
            Console.Write("============================");
            Console.SetCursorPosition(xOffset, yOffset++);
            Console.Write("         GAME OVER");
            Console.SetCursorPosition(xOffset, yOffset++);
            Console.Write("============================");
        }

        static void DrawWalls(int x, int y, int foodCount)
        {
            for (int i = 0; i < x; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("#");
                Console.SetCursorPosition(i, y);
                Console.Write("#");
            }

            for (int i = 0; i < y; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("#");
                Console.SetCursorPosition(x, i);
                Console.Write("#");
            }

            
        }
    }
    public enum Direction
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }

    public class Point
    {
        public int x, y;
        public char sym;
        public Point(int x, int y, char sym)
        {
            this.x = x;
            this.y = y;
            this.sym = sym;
        }

        public void Draw()
        {
            Console.SetCursorPosition(x, y);
            Console.Write(sym);
        }

        public void Clear()
        {
            sym = ' ';
            Draw();
        }

        public bool IsHit(Point p)
        {
            if (p.x == x && p.y == y)
                return true;
            return false;
        }
    }

    public class Snake
    {
        List<Point>body;
        public Direction direction;
        public Snake(Point tail, int length, Direction direction)
        {
            body = new List<Point> ();
            for (int i = 0;i < length;i++)
            {
                Point p = new Point(tail.x, tail.y, '*');
                body.Add(p);
                tail.x++;
            }
            this.direction = direction;
        }

        public void Draw()
        {
            foreach (Point p in body)
            {
                p.Draw();
            }
        }
        public bool Eat(Point food)
        {
            Point head = HeadMove();
            if (head.IsHit(food))
            {
                food.sym = head.sym;
                body.Add(food);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Move()
        {
            Point tail = body.First();
            body.Remove(tail);
            Point head = HeadMove();
            body.Add(head);

            tail.Clear();
            head.Draw();
        }

        public Point HeadMove()
        {
            Point head = body.Last();
            Point next = new Point(head.x, head.y, head.sym);
            switch (direction)
            {
                case Direction.LEFT:
                    next.x -= 2;
                    break;
                case Direction.RIGHT:
                    next.x += 2;
                    break;
                case Direction.UP:
                    next.y--;
                    break;
                case Direction.DOWN:
                    next.y++;
                    break;
            }
            return next;
        }

        public bool Hit_Wall()
        {
            Point head = body.Last();
            if (head.x >= 80 || head.x <= 0 || head.y <=0 || head.y >= 20)
            {
                return true;
            }
            return false;
        }

        public bool Hit_Tail()
        {
            Point head = body.Last();

            for(int i = 0; i < body.Count - 2; i++) 
            {
                if (head.IsHit(body[i]))
                {
                    return true;
                }
            }
            return false;
        }
    }
    
    public class FoodCreator
    {
        int mapX;
        int mapY;
        char sym;
        Random ran = new Random();

        public FoodCreator(int mapX, int mapY, char sym)
        {
            this.mapX = mapX;
            this.mapY = mapY;
            this.sym = sym;
        }

        public Point Create()
        {
            int x = ran.Next(1, mapX - 2);
            int y = ran.Next(1, mapY - 2);

            x = x % 2 == 1 ? x : x + 1;
            Point p = new Point(x, y, sym);
            return p;
        }
    }
}
