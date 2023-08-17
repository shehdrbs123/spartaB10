// See https://aka.ms/new-console-template for more information
class SnakeGame
{
    static void Main(string[] args)
    {
        SnakePlayer player = new SnakePlayer(300);
        player.Play();
    }
}
public enum FieldType
{
    NONE = ' ', BLOCK = '■', FOOD = '●', BLANK = '□'
}
public class SnakePlayer
{
    private Board board;
    private Snake snake;
    private FoodCreateor foodCreator;
    private int foodCount = 0;
    private readonly int gameSpeed;
    public SnakePlayer(int GameSpeedmiliseconds)
    {
        gameSpeed = GameSpeedmiliseconds;
    }

    public void Play()
    {
        //Console.CursorVisible = false;
        if (SetBoard())
        {
            OperateGame();
        }
    }

    private bool SetBoard()
    {
        bool isOk = true;
        board = new Board(20, 20);
        snake = new Snake();
        foodCreator = new FoodCreateor();
        Point initPos = new Point(2, 2, FieldType.BLOCK);
        snake.Init(initPos,Direction.RIGHT);
        board.LocateObject(snake.Root);
        
        foodCreator.SetPosFood(board);
        return isOk;
    }
    
    
    private void OperateGame()
    {
        bool isDie = false;
        while (!isDie)
        {
            board.Update(snake.snakeTails.Count-1);
            
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case 'w':
                    case 'W':
                        if(snake.direction != Direction.DOWN)
                            snake.direction = Direction.UP;
                        break;
                    case 's':
                    case 'S':
                        if(snake.direction != Direction.UP)
                            snake.direction = Direction.DOWN;
                        break;
                    case 'a':
                    case 'A':
                        if(snake.direction != Direction.RIGHT)
                            snake.direction = Direction.LEFT;
                        break;
                    case 'd':
                    case 'D':
                        if(snake.direction != Direction.LEFT)
                            snake.direction = Direction.RIGHT;
                        break;
                    default:
                        continue;
                }
            }
            Thread.Sleep(gameSpeed);
            //다음 위치를 미리 보고 판단
            Point newPos = new Point();
            snake.NextPos(ref newPos);
            if (!board.IsCanPlace(newPos.x, newPos.y))
            {
                isDie = true;
                Console.Write("사망하셨습니다");
            }else if (board.IsFood(newPos.x, newPos.y))
            {
                snake.Eat();
                foodCreator.SetPosFood(board);
            }
            else
            {
                Point erasePos = snake.snakeTails.Peek();
                board.ClearPos(erasePos.x,erasePos.y);
                snake.Move();
            }

            //board.LocateObject(snake.Root);
            board.LocateObject(snake.snakeTails.ToArray());
  
        }
    }
    
    public class Snake
    {
        public Queue<Point> snakeTails { private set; get; }
        public Point Root { private set; get; }
        public Direction direction;
        public Point newPos { private set; get; }
        public Snake()
        {
            snakeTails = new Queue<Point>();
        }
        
        public void Init(Point firstPos, Direction direction)
        {
            Root = firstPos;
            this.direction = direction;
            snakeTails.Enqueue(Root);
        }
        public void AddBody(Point p)
        {
            snakeTails.Enqueue(p);
        }
        public void Move()
        {
            Root = newPos;
            AddBody(newPos);
            snakeTails.Dequeue();
        }

        public void Eat()
        {
            Root = newPos;
            AddBody(newPos);
        }

        public void NextPos(ref Point newPos)
        {
            newPos.x = Root.x;
            newPos.y = Root.y;
            newPos.sym = Root.sym;
            
            switch (direction)
            {
                case Direction.UP :
                    newPos.y -= 1;
                    break;
                case Direction.DOWN :
                    newPos.y += 1;
                    break;
                case Direction.LEFT :
                    newPos.x -= 1;
                    break;
                case Direction.RIGHT :
                    newPos.x += 1;
                    break;
            }

            this.newPos = newPos;
        }
        
    }

    public class Board
    {
        private char[,] board;
        private int x;
        private int y;
        private bool isUpdated = true;
        private Random rand = new Random();
        /// <summary>
        /// 보드 사이즈를 넣어주면됨
        /// </summary>
        /// <param name="x">열 길이</param>
        /// <param name="y">행 길이</param>
        public Board(int x, int y)
        {
            this.x = x;
            this.y = y;
            board = new char[x, y];
            SetDefaultBoard();
        }
        
        private void SetDefaultBoard()
        {
            // 블랭크 채우기
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = (char)FieldType.BLANK;
                }
            }
            //외곽선 그리기
            for (int i = 0; i < board.GetLength(1); i++)
            {
                board[0, i] = (char)FieldType.BLOCK;
                board[board.GetLength(0) - 1, i] = (char)FieldType.BLOCK;
            }

            for (int i = 0; i < board.GetLength(0); i++)
            {
                board[i, 0] = (char)FieldType.BLOCK;
                board[i, board.GetLength(1) - 1] = (char)FieldType.BLOCK;
            }
        }

        private void PrintBoard(int foodCount)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Console.Write(board[j, i]);
                }
                Console.WriteLine();
            }
            Console.WriteLine($"먹은 먹이의 수 {foodCount}");
        }

        public bool LocateObject(int x, int y, FieldType newField )
        {
            if (!IsCanPlace(x,y))
            {
                return false;
            }
            board[x, y] = (char)newField;
            isUpdated = true;
            
            return true;
        }

        public bool LocateObject(Point pos)
        {
            return LocateObject(pos.x, pos.y, pos.sym);
        }
        
        public bool LocateObject(Point[] posList)
        {
            foreach (var pos in posList)
            {
                LocateObject(pos.x, pos.y, pos.sym);
            }

            return true;
        }

        public void Update(int foodCount)
        {
            if (isUpdated)
            {
                isUpdated = false;
                Console.Clear();
                PrintBoard(foodCount);
            }
        }

        public int GetLenX()
        {
            return x;
        }

        public int GetLenY()
        {
            return y;
        }
        
        public bool IsCanPlace(int x, int y)
        {
            return (char)FieldType.BLOCK != board[x, y];
        }

        public Point RandomPoint(FieldType type)
        {
            while (true)
            {
                int x = rand.Next(1, GetLenX() - 1);
                int y = rand.Next(1, GetLenY() - 1);
                if (IsCanPlace(x, y))
                    return new Point(x, y, type);
            }
        }

        public void ClearPos(int x, int y)
        {
            board[x, y] = (char)FieldType.BLANK;
            isUpdated = true;
        }

        public bool IsFood(int x, int y)
        {
            return board[x, y] == (char)FieldType.FOOD;
        }
    }

    public class FoodCreateor
    {
        private Point FoodPos;
    
        public void SetPosFood(Board board)
        {
            Random rand = new Random();
            while (true)
            {
                Point newPoint = board.RandomPoint(FieldType.FOOD);

                if (board.LocateObject(newPoint.x,newPoint.y, newPoint.sym))
                    break;
            }
        }
    }
    
    public class Point
    {
        public int x { get; set; }
        public int y { get; set; }
        
        public FieldType sym { get; set; }
        
        // Point 클래스 생성자
        public Point(){}
        public Point(int _x, int _y, FieldType _sym)
        {
            x = _x;
            y = _y;
            sym = _sym;
        }
        
        
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