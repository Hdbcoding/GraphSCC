using System.Collections.Generic;

namespace GraphSCC.ConsoleApp
{
    public class Node
    {
        public int Id { get; set; }
        public List<int> Edges { get; set; } = new List<int>();
        public List<int> ReverseEdges { get; set; } = new List<int>();
        public Node(int id)
        {
            Id = id;
        }
        public void AddEdge(int id) => Edges.Add(id);
        public void AddReverseEdge(int id) => ReverseEdges.Add(id);
    }
}