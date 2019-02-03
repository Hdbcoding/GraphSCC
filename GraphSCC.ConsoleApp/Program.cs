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
            string input = "scc.txt";
            var top5 = CalculateSCCSizes(input);
            top5.ForEach(Console.WriteLine);
            Console.WriteLine("press enter to exit");
            Console.ReadLine();
        }

        public static Dictionary<int, Node> ParseGraph(string inputFile)
        {
            var graph = new Dictionary<int, Node>();
            foreach (string line in File.ReadLines(inputFile))
            {
                List<int> values = line.Split('\t', ' ', ',')
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
            foreach (var node in graph.Values)
            {
                if (!node.Explored)
                {
                    CalculateFinishingTime(graph, node, ref time);
                }
            }
        }

        private static void CalculateFinishingTime(Dictionary<int, Node> graph, Node startingNode, ref int time)
        {
            var stack = new Stack<Node>();
            var executedStack = new Stack<Node>();
            stack.Push(startingNode);
            executedStack.Push(startingNode);
            while (stack.Any())
            {
                var n = stack.Pop();
                n.Explored = true;

                var edges = n.ReverseEdges.ToList();
                edges.Reverse();
                foreach (var id in edges)
                {
                    var edge = graph[id];
                    if (!edge.Explored)
                    {
                        stack.Push(edge);
                        executedStack.Push(edge);
                    }
                }
            }

            while (executedStack.Any())
            {
                var n = executedStack.Pop();
                n.FinishingTime = ++time;
            }
        }

        public static void CalculateSCCs(Dictionary<int, Node> graph)
        {
            MarkAllUnexplored(graph);
            var ordered = graph.Values.OrderByDescending(n => n.FinishingTime);
            foreach (var node in ordered)
            {
                if (!node.Explored)
                {
                    CalculateSCC(graph, node);
                }
            }
        }

        private static void CalculateSCC(Dictionary<int, Node> graph, Node startingNode)
        {
            var stack = new Stack<Node>();
            stack.Push(startingNode);
            while (stack.Count > 0)
            {
                var n = stack.Pop();
                n.Explored = true;
                n.LeaderId = startingNode.Id;

                var edges = n.Edges.ToList();
                edges.Reverse();
                foreach (var id in edges)
                {
                    var edge = graph[id];
                    if (!edge.Explored) stack.Push(edge);
                }
            }
        }

        private static void MarkAllUnexplored(Dictionary<int, Node> graph)
        {
            foreach (var node in graph.Values)
                node.Explored = false;
        }

        public static List<int> CalculateSCCSizes(string inputFile)
        {
            var graph = ParseGraph(inputFile);
            CalculateFinishingTimes(graph);
            CalculateSCCs(graph);
            var sccSizes = new Dictionary<int,int>();
            foreach (var node in graph.Values){
                if (sccSizes.ContainsKey(node.LeaderId)){
                    sccSizes[node.LeaderId] = sccSizes[node.LeaderId] + 1;
                } else {
                    sccSizes.Add(node.LeaderId, 1);
                }
            }

            var topSccs = sccSizes.OrderByDescending(n => n.Value).Select(n => n.Value).ToList();

            while (topSccs.Count < 5) topSccs.Add(0);

            return topSccs.Take(5).ToList();
        }
    }
}
