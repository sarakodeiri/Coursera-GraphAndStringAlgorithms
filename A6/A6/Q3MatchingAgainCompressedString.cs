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
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, long, String[], long[]>)Solve);

        public long[] Solve(string text, long n, String[] patterns)
        {
            Dictionary<char, int> starts = new Dictionary<char, int>();
            Dictionary<char, List<int>> count = new Dictionary<char, List<int>>();

            PreprocessBWT(text, ref starts, ref count);
            List<long> results = new List<long>();
            for (int i = 0; i < patterns.Length; i++)
                results.Add(CountOccurrences(patterns[i], text, starts, count));

            return results.ToArray();

        }

        private void PreprocessBWT(string bwt, ref Dictionary<char, int> starts, ref Dictionary<char, List<int>> count)
        {
            string firstCol = bwt;
            firstCol = new string(bwt.OrderBy(s => s).ToArray());
            char[] allChars = { 'A', 'C', 'G', 'T', '$' };

            foreach (char ch in allChars)
            {
                starts[ch] = firstCol.IndexOf(ch);
                count[ch] = new List<int>();
            }

            int d, a, c, g, t;
            d = a = c = g = t = 0;

            count['$'].Add(d);
            count['A'].Add(a);
            count['C'].Add(c);
            count['G'].Add(g);
            count['T'].Add(t);
            for (int i = 0; i < bwt.Length; i++)
            {
                switch (bwt[i])
                {
                    case '$':
                        d++;
                        break;
                    case 'A':
                        a++;
                        break;
                    case 'C':
                        c++;
                        break;
                    case 'G':
                        g++;
                        break;
                    case 'T':
                        t++;
                        break;
                }
                count['$'].Add(d);
                count['A'].Add(a);
                count['C'].Add(c);
                count['G'].Add(g);
                count['T'].Add(t);
            }
        }
        
        private long CountOccurrences(string pattern, string bwt, Dictionary<char, int> starts, Dictionary<char, List<int>> count)
        {
            int top = 0;
            int bottom = bwt.Length - 1;
            while (top <= bottom)
            {
                if (pattern.Length != 0)
                {
                    char symbol = pattern.Last();
                    pattern = pattern.Remove(pattern.Length - 1, 1);
                    top = starts[symbol] + count[symbol][top];
                    bottom = starts[symbol] + count[symbol][bottom + 1] - 1;
                }

                else
                    return bottom - top + 1;
            }
            return 0;
        }
    }
}
