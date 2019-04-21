using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class Q3PatternMatchingSuffixArray : Processor
    {
        public Q3PatternMatchingSuffixArray(string testDataName) : base(testDataName)
        {
            this.VerifyResultWithoutOrder = true;
            ExcludeTestCaseRangeInclusive(3, 50);
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, long, string[], long[]>)Solve, "\n");

        private long[] Solve(string text, long n, string[] patterns)
        {
            text += "$";
            var obj = new Q2CunstructSuffixArray("TD2");
            var suffixArray = obj.Solve(text);
            List<long> result = new List<long>();

            for (int i = 0; i < patterns.Length; i++)
                result.Add(PatternMatching(text, patterns[i], suffixArray));

            for (int i = 0; i < result.Count(); i++)
                if (result[i] == -1)
                    result.Remove(result[i]);
            result.Distinct();

            long[] none = { -1 };

            return result.Count() == 0 ? none : result.ToArray();
        }

        private long[] PatternMatching(string text, string pattern, long[] suffixArray)
        {
            int minIndex = 0;
            int maxIndex = text.Length;
            int midIndex;
            while (minIndex < maxIndex)
            {
                midIndex = (minIndex + maxIndex) / 2;

                string suffix = text.Substring((int)suffixArray[midIndex], text.Length - (int)suffixArray[midIndex]);
                if (pattern.CompareTo(suffix) > 0)
                    minIndex = midIndex + 1;
                else
                    maxIndex = midIndex;

            }

            int start = minIndex;
            maxIndex = text.Length - 1;

            while (minIndex < maxIndex)
            {
                midIndex = (minIndex + maxIndex) / 2;
                string suffix = text.Substring((int)suffixArray[midIndex], text.Length - (int)suffixArray[midIndex]);
                if (pattern.CompareTo(suffix) < 0)
                    maxIndex = midIndex;
                else
                    minIndex = midIndex + 1;
            }

            List<long> result = new List<long>();
            int end = maxIndex;
            if (start <= end)
                for (int i = start; i < end; i++)
                    result.Add(suffixArray[i]);

            return result.ToArray();
        }

    }
}