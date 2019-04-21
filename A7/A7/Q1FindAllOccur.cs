using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class Q1FindAllOccur : Processor
    {
        public Q1FindAllOccur(string testDataName) : base(testDataName)
        {
			this.VerifyResultWithoutOrder = true;
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, String, long[]>)Solve, "\n");

        public long[] Solve(string text, string pattern)
        {
            string patternDollarText = pattern + "$" + text;
            long[] prefixFuncRes = ComputeprefixFunction(patternDollarText);
            List<long> result = new List<long>();
            int n = pattern.Length;
            for (int i = n; i < patternDollarText.Length; i++)
                if (prefixFuncRes[i] == n)
                    result.Add(i - 2 * n);

            long[] none = { -1 };

            return result.Count() == 0 ? none : result.ToArray();
        }

        private long[] ComputeprefixFunction(string str)
        {
            long[] result = new long[str.Length];
            result[0] = 0;
            int border = 0;
            for (int i = 1; i<str.Length; i++)
            {
                while (border > 0 && str[i] != str[border])
                    border = (int)result[border - 1];
                if (str[i] == str[border])
                    border++;
                else
                    border = 0;
                result[i] = border;
            }
            return result;
        }
    }
}
