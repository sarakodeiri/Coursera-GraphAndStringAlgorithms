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
            long[] none = { -1 };
            if (text.Length < pattern.Length)
                return none;
            else
                return new long[0];

        }
    }
}
