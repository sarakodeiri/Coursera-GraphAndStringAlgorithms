using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A6
{
    class Program
    {
        static void Main(string[] args)
        {
            var obj = new Q3MatchingAgainCompressedString("TD3");
            string test = "ACCAT$";
            var blah = new string[2];
            obj.Solve(test, 5, blah);

        }
    }
}
