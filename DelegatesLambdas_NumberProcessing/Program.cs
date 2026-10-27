using System;
using System.Collections.Generic;

namespace DelegatesAndLambdas_NumberProcessing
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
            Console.WriteLine("Исходный список чисел: " + string.Join(", ", numbers));

            Func<int, bool> filter = n => n % 2 == 0;
            Func<int, int> transform = n => n * n;

            List<int> historyList = new List<int>();

            Action<int> printAction = x => Console.WriteLine($"[Вывод на экран]: {x}");
            Action<int> saveAction = x => historyList.Add(x);

            Action<int> multicastingAction = printAction + saveAction;

            Console.WriteLine("\n--- Обработка по условиям Варианта 1 ---");
            foreach (int number in numbers)
            {
                if (filter(number))
                {
                    int squared = transform(number);
                    multicastingAction(squared);
                }
            }

            Console.WriteLine($"Элементов сохранено в отдельном списке: {historyList.Count}");

            Console.WriteLine();
            Console.WriteLine("=== Интерактивный режим обработки чисел ===");

            while (true)
            {
                try
                {
                    Console.Write("Введите целые числа через пробел (или 'stop' для выхода): ");
                    string input = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(input) || input.Trim().ToLower() == "stop")
                    {
                        break;
                    }

                    string[] parts = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    List<int> userNumbers = new List<int>();
                    foreach (string part in parts)
                    {
                        userNumbers.Add(int.Parse(part));
                    }

                    List<int> userHistory = new List<int>();
                    Action<int> userPrint = x => Console.WriteLine($"Результат конвейера: {x}");
                    Action<int> userSave = x => userHistory.Add(x);
                    Action<int> userMulti = userPrint + userSave;

                    Console.WriteLine("Результат конвейера для введённых данных:");
                    foreach (int num in userNumbers)
                    {
                        if (filter(num))
                        {
                            int res = transform(num);
                            userMulti(res);
                        }
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка: вводите только целые числа через пробел.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }
    }
}