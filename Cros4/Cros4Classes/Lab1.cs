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
                string[] inputLines = File.ReadAllLines(inputFile);

                string[] inputValues = inputLines[0].Split(' ');
                int n = int.Parse(inputValues[0]);
                int m = int.Parse(inputValues[1]);
                char r = char.Parse(inputValues[2]);

                string[] playerCards = inputLines[1].Split(' ');
                string[] targetCards = inputLines[2].Split(' ');

                bool canDefend = CanDefend(playerCards, targetCards, r);

                File.WriteAllText(outputFile, canDefend ? "Yes" : "No");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

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

        static bool CanCover(string playerCard, string targetCard, char r)
        {
            char playerRank = playerCard[0];
            char playerSuit = playerCard[1];
            char targetRank = targetCard[0];
            char targetSuit = targetCard[1];

            if (playerRank == r && targetRank != r)
            {
                return false;
            }

            if (playerRank == r && targetRank == r)
            {
                return playerSuit > targetSuit;
            }

            if (playerRank != r && targetRank == r)
            {
                return playerSuit == r;
            }

            return playerSuit == targetSuit && playerRank > targetRank;
        }
    }
}