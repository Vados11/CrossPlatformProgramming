using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cros4Classes
{
    public class Lab3
    {
        public static void Run(string inputFile, string outputFile)
        {
            try
            {
                var lines = File.ReadAllLines(inputFile);

                if (lines.Length < 2)
                {
                    throw new Exception("Not enough input data");
                }

                var nm = lines[0].Split(' ').Select(int.Parse).ToArray();
                int n = nm[0];
                int m = nm[1];

                if (lines.Length < m + 1)
                {
                    throw new Exception("Not enough data about the edges of the graph.");
                }

                var edges = new List<Edge>();

                for (int i = 1; i <= m; i++)
                {
                    var edgeData = lines[i].Split(' ').Select(int.Parse).ToArray();

                    if (edgeData.Length != 3)
                    {
                        throw new Exception("Incorrect data for graph edge.");
                    }

                    edges.Add(new Edge(edgeData[0] - 1, edgeData[1] - 1, edgeData[2], i));
                }

                int bestCost = int.MaxValue;
                List<int> bestPath = null;

                for (int mask = 0; mask < (1 << m); mask++)
                {
                    adjList = new List<Edge>[n];
                    for (int i = 0; i < n; i++)
                    {
                        adjList[i] = new List<Edge>();
                    }

                    int currentCost = 0;

                    for (int j = 0; j < m; j++)
                    {
                        if ((mask & (1 << j)) != 0)
                        {
                            adjList[edges[j].U].Add(edges[j]);
                            currentCost += edges[j].Weight;
                        }
                    }

                    visited = new bool[n];
                    DFS(0);

                    if (visited.All(v => v) && currentCost < bestCost)
                    {
                        bestCost = currentCost;
                        bestPath = new List<int>();

                        for (int j = 0; j < m; j++)
                        {
                            if ((mask & (1 << j)) != 0)
                                bestPath.Add(edges[j].Index);
                        }
                    }
                }

                File.WriteAllText(outputFile, $"{bestCost} {bestPath.Count}\n{string.Join(" ", bestPath)}");
            }
            catch (Exception ex)
            {
                File.WriteAllText(outputFile, "-1"); // Вивести -1 у випадку помилки
                Console.WriteLine("Error: " + ex.Message);
                Console.ReadLine();
            }
        }

        public static List<Edge>[] adjList;
        public static bool[] visited;

        public static void DFS(int node)
        {
            visited[node] = true;
            foreach (var edge in adjList[node])
            {
                if (!visited[edge.V])
                    DFS(edge.V);
            }
        }
    }

    public class Edge
    {
        public int U { get; set; }
        public int V { get; set; }
        public int Weight { get; set; }
        public int Index { get; set; }

        public Edge(int u, int v, int weight, int index)
        {
            U = u;
            V = v;
            Weight = weight;
            Index = index;
        }
    }
}