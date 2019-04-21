using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class Q2CunstructSuffixArray : Processor
    {
        public Q2CunstructSuffixArray(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, long[]>)Solve);

        private long[] SortCharacters(char[] textArray)
        {
            throw new NotImplementedException();
        }

        private long[] ComputeCharClasses(char[] textArray, long[] order)
        {
            throw new NotImplementedException();
        }

        private long[] SortDoubled(char[] textArray, int l, long[] order, long[] clazz)
        {
            throw new NotImplementedException();
        }

        private long[] UpdateClasses(long[] order, long[] clazz, int l)
        {
            throw new NotImplementedException();
        }

        private long[] Solve(string text)
        {
            char[] textArray = text.ToCharArray();
            long[] order = SortCharacters(textArray);
            long[] @class = ComputeCharClasses(textArray, order);
            int l = 1;
            while (l < text.Length)
            {
                order = SortDoubled(textArray, l, order, @class);
                @class = UpdateClasses(order, @class, l);
                l *= 2;
            }
            return order;
        }

        
    }
}
