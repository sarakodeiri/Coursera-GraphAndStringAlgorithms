using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    public class Q3MatchingAgainCompressedString : Processor
    {
        public Q3MatchingAgainCompressedString(string testDataName) : base(testDataName)
        {
            ExcludeTestCaseRangeInclusive(4, 30);
        }

        public override string Process(string inStr) => 
        TestTools.Process(inStr, (Func<String, long, String[], long[]>)Solve);

        public long[] Solve(string text, long n, String[] patterns)
        {
            Dictionary<char, int> firstOccurences = new Dictionary<char, int>
            {
                ['$'] = text.IndexOf('$'),
                ['A'] = text.IndexOf('A'),
                ['C'] = text.IndexOf('C'),
                ['G'] = text.IndexOf('G'),
                ['T'] = text.IndexOf('T')
            };

            Q1ConstructBWT obj = new Q1ConstructBWT("TD1");
            string lastCol = obj.Solve(text);

            long[] result = new long [n];

            for (int i=0; i<result.Length; i++)
                result[i] = BetterBWMatching(firstOccurences, lastCol, patterns[i]);
            

            return result;
        }

        private long BetterBWMatching(Dictionary<char, int> firstOccurences, string lastCol, string pattern)
        {
            long top = 0;
            long bottom = lastCol.Length - 1;
            while (top <= bottom)
            {
                if (pattern.Length != 0)
                {
                    char symbol = pattern.Last();
                    pattern.Remove(pattern.Length - 1, 1);
                    var subArray = lastCol.Substring((int)top, (int) bottom - (int) top + 1).ToArray();
                    bool contains = Array.Exists(subArray, element => element == symbol);
                    if (contains)
                    {
                        top = firstOccurences[symbol] + Count(symbol, top, lastCol);
                        bottom = firstOccurences[symbol] + Count(symbol, bottom + 1, lastCol) - 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                    return bottom - top + 1;
            }
            return 0;
        }

        private long Count(char symbol, long i, string lastCol)
        {
            string inspect = lastCol.Substring(0, (int)i);
            long res = 0;
            for (int j = 0; j < inspect.Length; j++)
                if (lastCol[j] == symbol)
                    res++;
            return res;
        }
    }
}
