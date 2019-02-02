using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GraphSCC.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public static Dictionary<int, Node> ParseGraph(string inputFile)
        {
            var graph = new Dictionary<int, Node>();
            foreach (string line in File.ReadLines(inputFile))
            {
                List<int> values = line.Split('\t', ' ')
                    .Where(n => !string.IsNullOrWhiteSpace(n))
                    .Select(int.Parse)
                    .ToList();
                int id = values[0];
                int edge = values[1];

                if (!graph.ContainsKey(id)) graph.Add(id, new Node(id));
                if (!graph.ContainsKey(edge)) graph.Add(edge, new Node(edge));

                graph[id].AddEdge(edge);
                graph[edge].AddReverseEdge(id);
            }
            return graph;
        }
    }
}
