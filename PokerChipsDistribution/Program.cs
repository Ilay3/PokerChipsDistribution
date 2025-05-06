using System;
using System.Linq;

namespace PokerChipsDistribution
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Программа для расчета минимального количества ходов для равномерного распределения фишек");
            Console.WriteLine("----------------------------------------------------------------");

            // Тестовые примеры
            int[] test1 = { 1, 5, 9, 10, 5 };
            int[] test2 = { 1, 2, 3 };
            int[] test3 = { 0, 1, 1, 1, 1, 1, 1, 1, 1, 2 };

            Console.WriteLine("Тест 1:");
            Console.WriteLine($"Исходное распределение: [{string.Join(", ", test1)}]");
            Console.WriteLine($"Ожидаемый результат: 12");
            Console.WriteLine($"Полученный результат: {MinimumMoves(test1)}");
            Console.WriteLine();

            Console.WriteLine("Тест 2:");
            Console.WriteLine($"Исходное распределение: [{string.Join(", ", test2)}]");
            Console.WriteLine($"Ожидаемый результат: 1");
            Console.WriteLine($"Полученный результат: {MinimumMoves(test2)}");
            Console.WriteLine();

            Console.WriteLine("Тест 3:");
            Console.WriteLine($"Исходное распределение: [{string.Join(", ", test3)}]");
            Console.WriteLine($"Ожидаемый результат: 1");
            Console.WriteLine($"Полученный результат: {MinimumMoves(test3)}");
            Console.WriteLine();

            // Ввод пользовательских данных
            Console.WriteLine("Введите ваш пример (числа через запятую):");
            try
            {
                string input = Console.ReadLine();
                int[] userChips = input.Split(',')
                    .Select(s => int.Parse(s.Trim()))
                    .ToArray();

                Console.WriteLine($"Исходное распределение: [{string.Join(", ", userChips)}]");
                Console.WriteLine($"Результат: {MinimumMoves(userChips)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обработке ввода: {ex.Message}");
            }
        }

        /// <summary>
        /// Вычисляет минимальное количество ходов для равномерного распределения фишек
        /// </summary>
        /// <param name="chips">Массив, представляющий текущее распределение фишек</param>
        /// <returns>Минимальное количество ходов</returns>
        static int MinimumMoves(int[] chips)
        {
            int n = chips.Length;
            int totalChips = chips.Sum();

            // Проверка возможности равномерного распределения
            if (totalChips % n != 0)
                return -1; // Невозможно распределить поровну

            int targetChipsPerSeat = totalChips / n;

            // Вычисляем разницу между текущим и целевым количеством фишек для каждого места
            int[] diff = new int[n];
            for (int i = 0; i < n; i++)
            {
                diff[i] = chips[i] - targetChipsPerSeat;
            }

            // Пробуем все возможные начальные позиции (так как стол круглый)
            int minMoves = int.MaxValue;

            for (int start = 0; start < n; start++)
            {
                int moves = 0;
                int balance = 0;

                for (int i = 0; i < n; i++)
                {
                    int pos = (start + i) % n;
                    balance += diff[pos];
                    moves += Math.Abs(balance);
                }

                minMoves = Math.Min(minMoves, moves);
            }

            return minMoves;
        }
    }
}