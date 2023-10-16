namespace Cros4Classes
{
    public static class LabExecutor
    {
        public static void Run(int labNumber, string inputFile, string outputFile)
        {
            if (labNumber >= 1 && labNumber <= 3)
            {
                if (labNumber == 1)
                {
                    Lab1.Run(inputFile, outputFile);
                }
                else if (labNumber == 2)
                {
                    Lab2.Run(inputFile, outputFile);
                }
                else if (labNumber == 3)
                {
                    Lab3.Run(inputFile, outputFile);
                }
            }
            else
            {
                throw new ArgumentException("Invalid lab number.");
            }
        }
    }
}