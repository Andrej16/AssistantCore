using System;
using System.Threading;

namespace AssistantCore.Algorithms
{
    public class GameOfLife
    {
        public static int Print()
        {
            int rows = 10; // Number of rows in the grid
            int cols = 10; // Number of columns in the grid

            // Initialize grid with random live/dead cells
            int[,] grid = new int[rows, cols];
            Random random = new Random();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    grid[i, j] = random.Next(2); // 0 for dead, 1 for alive
                }
            }

            while (true)
            {
                Console.Clear();
                PrintGrid(grid);
                grid = GenerateNextGeneration(grid);
                Thread.Sleep(500); // Pause for 500 milliseconds
            }

            return 1;
        }

        static void PrintGrid(int[,] grid)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    Console.Write(grid[i, j] == 1 ? "■" : " "); // Print alive cells as ■ and dead cells as space
                }
                Console.WriteLine();
            }
        }

        static int CountLiveNeighbors(int[,] grid, int r, int c)
        {
            int count = 0;
            for (int i = r - 1; i <= r + 1; i++)
            {
                for (int j = c - 1; j <= c + 1; j++)
                {
                    if (i == r && j == c) continue;
                    if (i >= 0 && i < grid.GetLength(0) && j >= 0 && j < grid.GetLength(1))
                    {
                        count += grid[i, j];
                    }
                }
            }
            return count;
        }

        static int[,] GenerateNextGeneration(int[,] grid)
        {
            int[,] newGrid = new int[grid.GetLength(0), grid.GetLength(1)];
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    int liveNeighbors = CountLiveNeighbors(grid, i, j);
                    if (grid[i, j] == 1)
                    {
                        newGrid[i, j] = (liveNeighbors == 2 || liveNeighbors == 3) ? 1 : 0;
                    }
                    else
                    {
                        newGrid[i, j] = (liveNeighbors == 3) ? 1 : 0;
                    }
                }
            }
            return newGrid;
        }
    }
}