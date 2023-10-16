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
            var lines = File.ReadAllLines(inputFile);
            var nm = lines[0].Split(' ').Select(int.Parse).ToArray();
            int n = nm[0];
            int m = nm[1];

            var edges = new List<Edge>();

            for (int i = 1; i <= m; i++)
            {
                var edgeData = lines[i].Split(' ').Select(int.Parse).ToArray();
                edges.Add(new Edge(edgeData[0] - 1, edgeData[1] - 1, edgeData[2], i));
            }

            int bestCost = int.MaxValue;
            List<int> bestPath = null;

            for (int mask = 0; mask < (1 << m); mask++)
            {
                var adjList = new List<Edge>[n];
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

                var visited = new bool[n];
                DFS(0, visited, adjList);

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

        public static void DFS(int node, bool[] visited, List<Edge>[] adjList)
        {
            visited[node] = true;
            foreach (var edge in adjList[node])
            {
                if (!visited[edge.V])
                    DFS(edge.V, visited, adjList);
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