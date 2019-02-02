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

        public static void CalculateFinishingTimes(Dictionary<int, Node> graph)
        {
            int time = 0;
            foreach (var node in graph.Values){
                if (!node.Explored){
                    CalculateFinishingTime(graph, node, ref time);
                }
            }
        }

        private static void CalculateFinishingTime(Dictionary<int, Node> graph, Node node, ref int time)
        {
            node.Explored = true;
            foreach (var id in node.ReverseEdges){
                var edge = graph[id];
                if (!edge.Explored) CalculateFinishingTime(graph, edge, ref time);
            }
            node.FinishingTime = ++time;
        }

        public static void CalculateSCCs(Dictionary<int, Node> graph)
        {

        }

        public static void MarkAllUnexplored(Dictionary<int, Node> graph)
        {
            foreach (var node in graph.Values)
                node.Explored = false;
        }
    }
}
