using System;

namespace Cros4Classes
{
    public static class LabExecutor
    {
        public static string Run(int labNumber, string inputFile)
        {
            if (labNumber == 1)
            {
                return Lab1.Run(inputFile);
            }
            else if (labNumber == 2)
            {
                return Lab2.Run(inputFile);
            }
            else if (labNumber == 3)
            {
                return Lab3.Run(inputFile);
            }
            else
            {
                throw new ArgumentException("Invalid lab number.");
            }
        }
    }
}