using NUnit.Framework;
using GraphSCC.ConsoleApp;
using System.IO;
using System;
using System.Linq;

namespace GraphSCC.Tests
{
    public class GraphTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CanLoadTestCases()
        {
            foreach (var entry in TestCaseFactory.TestCases)
            {
                Assert.AreEqual(2, entry.OriginalArguments.Length);
                Assert.NotNull(entry.OriginalArguments[0]);
                Assert.NotNull(entry.OriginalArguments[1]);
            }
        }

        [Test, TestCaseSource(typeof(TestCaseFactory), "TestCases")]
        public void CanLoadGraphs(string inputFile, string outputFile)
        {
            var graph = Program.ParseGraph(inputFile);
            var count = int.Parse(Path.GetFileNameWithoutExtension(inputFile.Substring(inputFile.LastIndexOf('_') + 1)));
            Assert.LessOrEqual(graph.Count, count);
            Assert.GreaterOrEqual(graph.Count, count - 5);
        }

        [Test, TestCaseSource(typeof(TestCaseFactory), "TestCases")]
        public void CanCalculateFinishingTimes(string inputFile, string outputFile)
        {
            var graph = Program.ParseGraph(inputFile);
            Program.CalculateFinishingTimes(graph);
            //all nodes have a finishing time
            Assert.True(graph.Values.All(n => n.FinishingTime > 0));
            //each node has a unique finishing time 
            Assert.AreEqual(graph.Count, graph.Values.Select(n => n.FinishingTime).Distinct().Count());
        }
    }
}