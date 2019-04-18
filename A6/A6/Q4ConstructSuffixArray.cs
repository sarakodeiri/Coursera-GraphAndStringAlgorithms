using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    public class Q4ConstructSuffixArray : Processor
    {
        public Q4ConstructSuffixArray(string testDataName) : base(testDataName)
        {
            //ExcludeTestCaseRangeInclusive(48, 50);
        }

        public override string Process(string inStr) => 
            TestTools.Process(inStr, (Func<String, long[]>)Solve);

        public long[] Solve(string text)
        {
            List<(string, long)> data = new List<(string, long)>();
            for (int i=0; i<text.Length; i++)
                data.Add((text.Substring(i), i));

            data.Sort();

            List<long> result = new List<long>();
            for (int i = 0; i < data.Count(); i++)
                result.Add(data[i].Item2);
            
            return result.ToArray();
        }
    }
}
