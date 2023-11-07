using System;
using System.IO;
using System.Numerics;

namespace Cros4Classes
{
    public class Lab1
    {
        public static string Run(string inputFile)
        {
            string[] inputLines = inputFile.Split('\n'); // Розбиваємо рядок на окремі рядки

            if (inputLines.Length != 3)
            {
                return "The input data format is incorrect. Please enter the correct data.";
            }

            string[] inputValues = inputLines[0].Split(' ');

            if (inputValues.Length != 3)
            {
                return "The input data format is incorrect. Please enter the correct data.";
            }

            if (!int.TryParse(inputValues[0], out int n) || !int.TryParse(inputValues[1], out int m))
            {
                return "An error occurred when reading the number of cards. Please enter the correct data.";
            }

            char r = inputValues[2][0];

            string[] playerCards = inputLines[1].Split(' ');
            string[] targetCards = inputLines[2].Split(' ');

            if (playerCards.Length != n || targetCards.Length != m)
            {
                return "Incorrect number of cards in the player's hands or cards to reflect. Please check the data.";
            }

            bool canDefend = CanDefend(playerCards, targetCards, r);

            return canDefend ? "Yes" : "No";
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
