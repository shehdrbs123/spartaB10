namespace week5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] ints = { 5, 1, 2, 3, 5 ,3,2};
            Console.WriteLine(LargestRectangleArea(ints));

            /*int[][] image = new int[][] { new int[] { 1, 1, 1 }, new int[] { 1, 1, 0 }, new int[] { 1, 0, 1 } };

            image = FloodFill(image, 1,1,2);
            for(int i = 0; i < image.Length; i++)
            {
                for(int j = 0; j < image[i].Length; j++)
                {
                    Console.Write(image[i][j]);
                }
                Console.WriteLine();
            }*/


            /*int[] i = new int[] { 0,1,0,3,2,3};

            Console.WriteLine(LengthOfLIS(i));*/

        }

        //74번 문제
        public static int LargestRectangleArea(int[] heights)
        {
            Stack<int> stack = new Stack<int>();
            stack.Push(-1);     //스택에 요소 추가
            int maxarea = 0;
            for (int i = 0; i < heights.Length; ++i)
            {   //peek 최근 넣은 요소를 리턴 받지만 pop처럼 요소를 삭제시키지 않음
                while (stack.Peek() != -1 && heights[stack.Peek()] >= heights[i])// 스택 최상단 바의 높이가 현재 바의 높이보다 크거나 같은 경우 
                {
                    maxarea = Math.Max(maxarea, heights[stack.Pop()] * (i - stack.Peek() - 1)); // 최대 넓이를 갱신
                }
                stack.Push(i);
                Console.WriteLine(maxarea + " " + i);
            }

            while (stack.Peek() != -1) // 스택에 남아있는 바들을 처리 
                maxarea = Math.Max(maxarea, heights[stack.Pop()] * (heights.Length - stack.Peek() - 1));

            return maxarea;
        }


        //733번 문제
        public static int[][] FloodFill(int[][] image, int sr, int sc, int color)
        {
            int[][] dirs = new int[][] { new int[] { 1, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 0, -1 } };
            if (image[sr][sc] == color)
                return image;
            int oldColor = image[sr][sc];
            image[sr][sc] = color;
            foreach (var dir in dirs)
            {
                int x = sr + dir[0];
                int y = sc + dir[1];
                if (x >= 0 && x < image.Length && y >= 0 && y < image[sr].Length && image[x][y] == oldColor)
                {
                    FloodFill(image, x, y, color);
                }
            }
            return image;
        }

        //300번 문제
        public static int LengthOfLIS(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return 0;
            int max = 1;
            int num;

            int[] dp = new int[nums.Length];

            dp[0] = 1;
            for (int i = 1; i < nums.Length; i++)
            {
                num = 0;
                for (int j = 0; j < i; j++)
                {
                    if (nums[j] < nums[i])
                    {
                        num = Math.Max(num, dp[j]);
                    }

                    Console.Write($"{num}/ ");
                }
                dp[i] = num + 1;
                max = Math.Max(max, dp[i]);
                Console.WriteLine($"      {dp[i]}/");
            }
            return max;
        }
    }

}