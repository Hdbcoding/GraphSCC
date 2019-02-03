using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace GraphSCC.Tests
{
    public static class TestCaseFactory
    {
        private const string _testDirectory = "testCases";

        public static IEnumerable<TestCaseData> TestCases
        {
            get
            {
                Dictionary<string, (string input, string output)> dict = new Dictionary<string, (string input, string output)>();
                IEnumerable<string> files = Directory.EnumerateFiles(_testDirectory);
                foreach (string entry in files)
                {
                    string key = entry.Substring(entry.IndexOf('_') + 1);
                    if (dict.ContainsKey(key))
                    {
                        dict[key] = (dict[key].input, entry);
                    }
                    else
                    {
                        dict.Add(key, (entry, null));
                    }
                }

                foreach ((string input, string output) value in dict.Values)
                {
                    yield return new TestCaseData(value.input, value.output);
                }
            }
        }
    }
}