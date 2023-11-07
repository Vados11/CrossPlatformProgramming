using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cros4Classes
{
    public class Lab2
    {
        public static string Run(string inputFile)
        {
            try
            {
                string[] lines = inputFile.Split('\n');
                if (lines.Length < 3)
                {
                    throw new Exception("Not enough input data");
                }

                string[] coinValues = lines[1].Split(' ');
                int[] coins = Array.ConvertAll(coinValues, int.Parse);

                int N = int.Parse(lines[0]);
                int K = int.Parse(lines[2]);

                int[] dp = new int[K + 1];
                for (int i = 1; i <= K; i++)
                {
                    dp[i] = int.MaxValue;
                }
                dp[0] = 0;

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

                int result = dp[K] == int.MaxValue ? -1 : dp[K];
                return result.ToString();
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}
