using NUnit.Framework;
using GraphSCC.ConsoleApp;
using System.IO;
using System;

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
            var thing = File.ReadAllLines(inputFile);
            Console.WriteLine(Path.GetFileNameWithoutExtension(inputFile.Substring(inputFile.LastIndexOf('_') + 1)));
            Assert.AreEqual(thing.Length, graph.Count);
        }
    }
}