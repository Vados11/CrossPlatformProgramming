using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Cros1
{
    class Program
    {
        static void Main()
        {
            try
            {
                string[] inputLines = File.ReadAllLines("INPUT.txt");

                string[] inputValues = inputLines[0].Split(' ');
                int n = int.Parse(inputValues[0]); // Кількість карт в руках гравця
                int m = int.Parse(inputValues[1]); // Кількість карт, які потрібно відбити
                char r = char.Parse(inputValues[2]); // Козирний костюм

                string[] playerCards = inputLines[1].Split(' '); // Карти в руках гравця
                string[] targetCards = inputLines[2].Split(' '); // Карти, які потрібно відбити

                // Перевіряємо, чи можна дати відсіч
                bool canDefend = CanDefend(playerCards, targetCards, r);

                File.WriteAllText("OUTPUT.txt", canDefend ? "Так" : "Ні");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
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
