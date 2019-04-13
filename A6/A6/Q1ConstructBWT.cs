using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    public class Q1ConstructBWT : Processor
    {
        public Q1ConstructBWT(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<String, String>)Solve);

        public string Solve(string text)
        {
            List<string> rotations = new List<string>();
            string doubleText = text + text;
            int n = text.Length;

            for (int i = 0; i < n; i++)
                rotations.Add(doubleText.Substring(i, n));

            rotations.Sort();

            string result = string.Empty;

            foreach (string rotation in rotations)
                result += rotation.Last();
            return result;
        }
    }
}
