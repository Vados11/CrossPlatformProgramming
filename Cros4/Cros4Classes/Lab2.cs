using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cros4Classes
{
    public class Lab2
    {
        public static void Run(string inputFile, string outputFile)
        {
            try
            {
                // Читаємо вхідні дані з файлу
                string[] lines = File.ReadAllLines(inputFile);
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
                File.WriteAllText(outputFile, result.ToString());
            }
            catch (Exception ex)
            {
                File.WriteAllText(outputFile, "-1"); // Вивести -1 у випадку помилки
                Console.WriteLine("Error: " + ex.Message);
                Console.ReadLine();
            }
        }
    }
}