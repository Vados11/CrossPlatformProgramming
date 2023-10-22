using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cros2
{
    internal class Program
    {
        static void Main()
        {
            try
            {
                // Читаємо вхідні дані з файлу
                string[] lines = File.ReadAllLines("INPUT.TXT");
                if (lines.Length < 3)
                {
                    throw new Exception("Not enough input data");
                }

                int N = int.Parse(lines[0]);
                int[] coins = lines[1].Split().Select(int.Parse).ToArray();
                int K = int.Parse(lines[2]);

                // Ініціалізуємо масив dp значеннями, що відповідають нескінченності
                int[] dp = new int[K + 1];
                for (int i = 1; i <= K; i++)
                {
                    dp[i] = int.MaxValue;
                }

                // Встановлюємо початкове значення dp[0]
                dp[0] = 0;

                // Обчислюємо мінімальну кількість монет для кожної можливої суми від 1 до K
                for (int i = 1; i <= K; i++)
                {
                    foreach (int coin in coins)
                    {
                        if (i - coin >= 0 && dp[i - coin] != int.MaxValue)
                        {
                            dp[i] = Math.Min(dp[i], dp[i - coin] + 1);
                        }
                    }
                }

                // Виводимо результат у вихідний файл
                int result = dp[K] == int.MaxValue ? -1 : dp[K];
                File.WriteAllText("OUTPUT.TXT", result.ToString());
            }
            catch (Exception ex)
            {
                File.WriteAllText("OUTPUT.TXT", "-1"); // Вивести -1 у випадку помилки
                Console.WriteLine("Error: " + ex.Message);
                Console.ReadLine();
            }
        }
    }
}
