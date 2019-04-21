using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A7
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "AAA";
            var obj = new Q2CunstructSuffixArray("TD2");
            long[] suffixArray = obj.Solve(text);
        }
    }
}
