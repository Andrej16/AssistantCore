using AssistantCore.DataSources;
using System;

namespace AssistantCore.Algorithms;

public class SaluteAutomaton
{
    private readonly int rows;
    private readonly int cols;
    private readonly SaluteCell[,] grid;

    public SaluteAutomaton(int rows, int cols)
    {
        this.rows = rows;
        this.cols = cols;
        grid = new SaluteCell[rows, cols];

        // Ініціалізація початкового стану клітин (за вашими потребами)
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                grid[i, j] = new SaluteCell(initialState: false);
            }
        }
    }

    // Оновлення стану клітин
    public void Update()
    {
        var newGrid = new SaluteCell[rows, cols];

        // Ваші правила переходу між станами клітин
        // Наприклад, можливо, ви хочете, щоб клітини мигали або випускали "салюти"

        // Виведення поточного стану решітки (можна використовувати консоль)
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                grid[i, j].IsOn = DateTime.Now.Ticks % 2 == 0;

                Console.Write(grid[i, j].IsOn ? "+" : "-");
            }

            Console.WriteLine();
        }
    }

    public static int Print()
    {
        int rows = 10;
        int cols = 50;
        SaluteAutomaton automaton = new SaluteAutomaton(rows, cols);

        // Основний цикл симуляції
        for (int generation = 0; generation < 1000; generation++)
        {
            automaton.Update();
            // Додайте затримку, щоб бачити зміни в реальному часі
            System.Threading.Thread.Sleep(1000);
            Console.Clear(); // Очищення консолі для нового кадру
        }

        return 1;
    }
}
