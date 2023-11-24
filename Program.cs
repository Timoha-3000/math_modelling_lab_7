using System;

class Program
{
    static void Main()
    {
        int numCompanies = 15;
        int[,] debts = GenerateRandomDebts(numCompanies);

        Console.WriteLine("Матрица взаимодолгов:");
        DisplayMatrix(debts);

        Console.WriteLine("\nСначала рассчитываем сальдо:");
        int[] initialBalances = CalculateBalance(debts);

        Console.WriteLine("\nПроводим взаимоотчет:");
        int[] finalBalances = ConductSettlement(debts, initialBalances);

        Console.WriteLine("\nСальдо до взаимоотчета:");
        DisplayBalances(initialBalances);

        Console.WriteLine("\nСальдо после взаимоотчета:");
        DisplayBalances(finalBalances);

        if (CheckBalancesEquality(initialBalances, finalBalances))
        {
            Console.WriteLine("\nСальдо совпадают.");
        }
        else
        {
            Console.WriteLine("\nСальдо не совпадают. Программа не может выполнить взаиморасчеты.");
        }
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

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(matrix[i, j].ToString().PadLeft(4)); // Выравниваем числа
            }
            Console.WriteLine();
        }
    }

    // Расчет сальдо предприятий
    static int[] CalculateBalance(int[,] debts)
    {
        int numCompanies = debts.GetLength(0);
        int[] balances = new int[numCompanies];

        for (int i = 0; i < numCompanies; i++)
        {
            int totalDebtToOthers = 0;
            int totalDebtFromOthers = 0;

            for (int j = 0; j < numCompanies; j++)
            {
                if (i != j)
                {
                    totalDebtToOthers += debts[i, j];
                    totalDebtFromOthers += debts[j, i];
                }
            }

            balances[i] = totalDebtFromOthers - totalDebtToOthers;

            Console.WriteLine($"Сальдо для предприятия {(char)('A' + i)}: {balances[i]}");
        }

        return balances;
    }

    // Проведение взаимоотчета с коррекцией сальдо
    static int[] ConductSettlement(int[,] debts, int[] balances)
    {
        int numCompanies = debts.GetLength(0);
        int[] updatedBalances = new int[numCompanies];
        Array.Copy(balances, updatedBalances, numCompanies);

        for (int i = 0; i < numCompanies; i++)
        {
            for (int j = 0; j < numCompanies; j++)
            {
                if (i != j && debts[i, j] > 0)
                {
                    if (updatedBalances[i] > 0 && updatedBalances[j] < 0)
                    {
                        int payment = Math.Min(debts[i, j], Math.Abs(updatedBalances[j]));
                        debts[i, j] -= payment;
                        updatedBalances[i] -= payment;
                        updatedBalances[j] += payment;
                    }
                }
            }
        }

        return updatedBalances;
    }

    // Вывод сальдо в консоль
    static void DisplayBalances(int[] balances)
    {
        int numCompanies = balances.Length;

        for (int i = 0; i < numCompanies; i++)
        {
            Console.WriteLine($"Сальдо для предприятия {(char)('A' + i)}: {balances[i]}");
        }
    }

    // Проверка совпадения сальдо до и после взаимоотчета
    static bool CheckBalancesEquality(int[] initialBalances, int[] finalBalances)
    {
        for (int i = 0; i < initialBalances.Length; i++)
        {
            if (initialBalances[i] != finalBalances[i])
            {
                return false;
            }
        }
        return true;
    }
}