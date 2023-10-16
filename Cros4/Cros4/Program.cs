using Cros4Classes;
using System;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.Clear();
            Console.Write("Enter command: ");
            args = Console.ReadLine().Split(' ');

            try
            {
                if (args.Length < 1)
                {
                    Console.WriteLine("You need to specify a command");
                    PrintUsage();
                    continue;
                }

                string command = args[0];

                switch (command)
                {
                    case "version":
                        Console.WriteLine("Author: Davydenko Vladyslav");
                        Console.WriteLine("Version: 1.0");
                        break;

                    case "run":
                        if (args.Length < 2)
                        {
                            Console.WriteLine("You must specify a subcommand (lab1, lab2 or lab3)");
                            continue;
                        }

                        string labName = args[1];
                        if (labName != "lab1" && labName != "lab2" && labName != "lab3")
                        {
                            Console.WriteLine("Invalid lab number. Please specify lab1, lab2, or lab3");
                            continue;
                        }

                        string inputPath = GetCommandLineArgumentValue(args, "-I", "--input");
                        string outputPath = GetCommandLineArgumentValue(args, "-o", "--output");
                        SetDefaultPaths(ref inputPath, ref outputPath);

                        if (string.IsNullOrEmpty(inputPath))
                        {
                            Console.WriteLine("Input file not found");
                            continue;
                        }

                        string labNumberStr = labName.Substring(3);
                        int labNumber = int.Parse(labNumberStr);
                        LabExecutor.Run(labNumber, inputPath, outputPath);
                        break;

                    case "set-path":
                        string path = GetCommandLineArgumentValue(args, "-p", "--path");
                        if (string.IsNullOrEmpty(path))
                        {
                            Console.WriteLine("Path not specified");
                            continue;
                        }
                        Environment.SetEnvironmentVariable("LAB_PATH", path, EnvironmentVariableTarget.User);
                        Console.WriteLine($"The path to the folder with input and output files is set to: {path}");
                        break;

                    case "exit":
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine($"Unknown command: {args}");
                        PrintUsage();
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    static string GetCommandLineArgumentValue(string[] args, string shortArg, string longArg)
    {
        for (int i = 1; i < args.Length - 1; i++)
        {
            if ((args[i] == shortArg || args[i] == longArg) && !string.IsNullOrEmpty(args[i + 1]))
            {
                return args[i + 1];
            }
        }
        return null;
    }

    static void SetDefaultPaths(ref string inputPath, ref string outputPath)
    {
        if (string.IsNullOrEmpty(inputPath))
        {
            inputPath = Environment.GetEnvironmentVariable("LAB_PATH") ?? Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        }

        if (string.IsNullOrEmpty(outputPath))
        {
            outputPath = inputPath;
        }
    }

    static void PrintUsage()
    {
        Console.WriteLine("Cros4  version - display information about the program");
        Console.WriteLine("Cros4  run lab1|lab2|lab3");
        Console.WriteLine("Cros4  set-path -p|--path path - set the path to the folder");
    }
}