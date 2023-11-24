using System;

class Program
{
    static void Main()
    {
        int numCompanies = 15;
        int[,] debts = GenerateRandomDebts(numCompanies);

        Console.WriteLine("Матрица взаимодолгов:");
        DisplayMatrix(debts);

        Console.WriteLine("\nВзаимоотчет:");
        ConductSettlement(debts);
    }

    // Генерация случайных долгов между предприятиями
    static int[,] GenerateRandomDebts(int numCompanies)
    {
        Random random = new Random();
        int[,] debts = new int[numCompanies, numCompanies];

        for (int i = 0; i < numCompanies; i++)
        {
            for (int j = 0; j < numCompanies; j++)
            {
                if (i != j) // Не учитываем долги предприятий самим себе
                {
                    debts[i, j] = random.Next(1, 101); // Генерируем случайные долги от 1 до 100
                }
            }
        }

        return debts;
    }

    // Вывод матрицы в консоль
    static void DisplayMatrix(int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        Console.Write("   "); // Пустая ячейка для отступа

        // Вывод заголовков столбцов (A, B, C...)
        for (int i = 0; i < cols; i++)
        {
            Console.Write($"{(char)('A' + i),4}"); // Заголовки столбцов
        }
        Console.WriteLine();

        for (int i = 0; i < rows; i++)
        {
            Console.Write($"{(char)('A' + i),2} "); // Заголовки строк

            for (int j = 0; j < cols; j++)
            {
                Console.Write(matrix[i, j].ToString().PadLeft(4)); // Выравниваем числа
            }
            Console.WriteLine();
        }
    }

    // Проведение взаимоотчета
    static void ConductSettlement(int[,] debts)
    {
        int numCompanies = debts.GetLength(0);

        for (int i = 0; i < numCompanies; i++)
        {
            for (int j = 0; j < numCompanies; j++)
            {
                if (i != j && debts[i, j] > 0)
                {
                    Console.WriteLine($"Предприятие {(char)('A' + i)} погашает долг {debts[i, j]} предприятию {(char)('A' + j)}");
                    debts[i, j] = 0; // Погашаем долг
                }
            }
        }

        Console.WriteLine("\nВзаимоотчет завершен.");
    }
}
