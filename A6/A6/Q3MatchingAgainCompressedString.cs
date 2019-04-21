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

        // Preprocess the Burrows-Wheeler Transform bwt of some text
        // and compute as a result:
        //   * starts - for each character C in bwt, starts[C] is the first position
        //       of this character in the sorted array of
        //       all characters of the text.
        //   * occ_count_before - for each character C in bwt and each position P in bwt,
        //       occ_count_before[C][P] is the number of occurrences of character C in bwt
        //       from position 0 to position P inclusive.
        //
        private void PreprocessBWT(string bwt, ref Dictionary<char, int> starts, ref Dictionary<char, List<int>> count)
        {
            string firstCol = bwt;
            firstCol = new string(bwt.OrderBy(s => s).ToArray());

            starts['$'] = firstCol.IndexOf('$');
            starts['A'] = firstCol.IndexOf('A');
            starts['C'] = firstCol.IndexOf('C');
            starts['G'] = firstCol.IndexOf('G');
            starts['T'] = firstCol.IndexOf('T');

            count['$'] = new List<int>();
            count['A'] = new List<int>();
            count['C'] = new List<int>();
            count['G'] = new List<int>();
            count['T'] = new List<int>();

            int dol, a, c, g, t;
            dol = a = c = g = t = 0;

            for (int i = 0; i < bwt.Length; i++)
            {
                switch (bwt[i])
                {
                    case '$':
                        dol++;
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
                count['$'].Add(dol);
                count['A'].Add(a);
                count['C'].Add(c);
                count['G'].Add(g);
                count['T'].Add(t);
            }
        }

        // Compute the number of occurrences of string pattern in the text
        // given only Burrows-Wheeler Transform bwt of the text and additional
        // information we get from the preprocessing stage - starts and occ_counts_before.
        private long CountOccurrences(string pattern, string bwt, Dictionary<char, int> starts, Dictionary<char, List<int>> count)
        {
            int top = 0;
            int bottom = bwt.Length - 1;
            while (top <= bottom)
            {
                if (pattern.Length != 0)
                {
                    char symbol = pattern.Last();
                    pattern.Remove(pattern.Length - 1, 1);
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
