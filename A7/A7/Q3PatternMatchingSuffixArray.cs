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
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, long, string[], long[]>)Solve, "\n");

        private long[] Solve(string text, long n, string[] patterns)
        {
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

        private long PatternMatching(string text, string pattern, long[] suffixArray)
        {
            int minIndex = 0;
            int maxIndex = text.Length - 1;
            int midIndex;
            while (minIndex < maxIndex)
            {
                midIndex = (minIndex + maxIndex) / 2;
                
                //suffix of text starting at position suffixArray(midIndex) = comparator
                string comparator = text.Substring((int)suffixArray[midIndex], text.Length);
                string[] set = { pattern, comparator };
                Array.Sort(set);
                bool paran = Array.IndexOf(set, comparator) < Array.IndexOf(set, pattern) ? true : false;

                if (paran)
                    minIndex++;
                else
                    maxIndex = midIndex;

            }

            int start = minIndex;
            maxIndex = text.Length - 1;

            while(minIndex < maxIndex)
            {
                midIndex = (minIndex + maxIndex) / 2;
                //suffix of text starting at position suffixArray(midIndex) = comparator
                string comparator = text.Substring((int)suffixArray[midIndex], text.Length);
                string[] set = { pattern, comparator };
                Array.Sort(set);
                bool paran = Array.IndexOf(set, comparator) > Array.IndexOf(set, pattern) ? true : false;
                if (paran)
                    maxIndex = midIndex;
                else
                    minIndex++;
            }

            int end = maxIndex;
            return start > end ? start : -1;
        }

    }
}
