using System;
using System.Threading;

namespace AssistantCore.Algorithms;

public class CellularAutomaton
{
    public static int Print()
    {
        int width = 20; // Ширина картки
        int height = 20; // Висота картки

        // Створення початкового масиву
        bool[,] grid = new bool[width, height];
        Random random = new Random();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y] = random.NextDouble() > 0.5;
            }
        }

        // Запуск клітинного автомату
        for (int i = 0; i < 1000; i++) // Кількість поколінь
        {
            Thread.Sleep(500);
            bool[,] newGrid = new bool[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int aliveNeighbors = CountAliveNeighbors(grid, x, y);
                    if (grid[x, y])
                    {
                        // Якщо клітина жива
                        newGrid[x, y] = aliveNeighbors >= 2 && aliveNeighbors <= 3;
                    }
                    else
                    {
                        // Якщо клітина мертва
                        newGrid[x, y] = aliveNeighbors == 3;
                    }
                }
            }
            grid = newGrid;
            // Виведення результату
            PrintToConsole(width, height, grid);
        }

        return 1;
    }

    private static void PrintToConsole(int width, int height, bool[,] grid)
    {
        Console.Clear();
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Console.Write(grid[x, y] ? "#" : ".");
            }
            Console.WriteLine();
        }
    }

    static int CountAliveNeighbors(bool[,] grid, int x, int y)
    {
        int count = 0;
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0) continue;
                int nx = x + dx;
                int ny = y + dy;
                if (nx >= 0 && nx < grid.GetLength(0) && ny >= 0 && ny < grid.GetLength(1))
                {
                    if (grid[nx, ny]) count++;
                }
            }
        }
        return count;
    }
}

