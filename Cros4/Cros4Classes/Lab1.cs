using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cros4Classes
{
    public class Lab1
    {
        public static void Run(string inputFile, string outputFile)
        {
            try
            {
                // Читаємо вхідні дані з файлу INPUT.TXT
                string[] inputLines = File.ReadAllLines(inputFile);

                if (inputLines.Length != 3)
                {
                    Console.WriteLine("The input data format is incorrect. Enter the correct data in the INPUT.TXT file");
                    Console.ReadLine();
                    return;
                }

                // Розділяємо рядки вхідних даних на окремі значення
                string[] inputValues = inputLines[0].Split(' ');

                if (inputValues.Length != 3)
                {
                    Console.WriteLine("The input data format is incorrect. Enter the correct data in the INPUT.TXT file");
                    Console.ReadLine();
                    return;
                }

                int n, m;
                if (!int.TryParse(inputValues[0], out n) || !int.TryParse(inputValues[1], out m))
                {
                    Console.WriteLine("An error occurred when reading the number of cards. Enter the correct data in the INPUT.TXT file");
                    Console.ReadLine();
                    return;
                }

                char r;
                if (inputValues[2].Length != 1 || !char.TryParse(inputValues[2], out r))
                {
                    Console.WriteLine("Error reading trump suit. Enter the correct data in the INPUT.TXT file");
                    Console.ReadLine();
                    return;
                }

                string[] playerCards = inputLines[1].Split(' ');
                string[] targetCards = inputLines[2].Split(' ');

                if (playerCards.Length != n || targetCards.Length != m)
                {
                    Console.WriteLine("Incorrect number of cards in the player's hands or cards to reflect. Check the data in the INPUT.TXT file");
                    Console.ReadLine();
                    return;
                }

                // Перевіряємо, чи можна дати відсіч
                bool canDefend = CanDefend(playerCards, targetCards, r);

                // Записуємо результат у файл OUTPUT.TXT
                File.WriteAllText(outputFile, canDefend ? "Yes" : "No");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.ReadLine();
            }
        }

        // Функція, яка перевіряє, чи можна дати відсіч
        static bool CanDefend(string[] playerCards, string[] targetCards, char r)
        {
            foreach (var targetCard in targetCards)
            {
                bool canDefendCard = false;
                foreach (var playerCard in playerCards)
                {
                    if (CanCover(playerCard, targetCard, r))
                    {
                        canDefendCard = true;
                        playerCards = playerCards.Where(card => card != playerCard).ToArray();
                        break;
                    }
                }
                if (!canDefendCard)
                {
                    return false;
                }
            }
            return true;
        }

        // Функція, яка перевіряє, чи може карта playerCard прикрити карту targetCard з козирного костюму r
        static bool CanCover(string playerCard, string targetCard, char r)
        {
            char playerRank = playerCard[0];
            char playerSuit = playerCard[1];
            char targetRank = targetCard[0];
            char targetSuit = targetCard[1];

            if (playerRank == r && targetRank != r)
            {
                // Якщо карта в руках гравця - козир і карта для відбиття - не козир, то можна прикрити тільки козирним козиром
                return false;
            }

            if (playerRank == r && targetRank == r)
            {
                // Якщо обидві карти - козирні, то можна прикрити тільки старшим козиром
                return playerSuit > targetSuit;
            }

            if (playerRank != r && targetRank == r)
            {
                // Якщо карта в руках гравця - не козир, а карта для відбиття - козир, то можна прикрити тільки козиром
                return playerSuit == r;
            }

            // Якщо обидві карти - не козир, то можна прикрити тільки старшою картою тієї ж масті
            return playerSuit == targetSuit && playerRank > targetRank;
        }
    }
}