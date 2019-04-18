using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    public class Q2ReconstructStringFromBWT : Processor
    {
        public Q2ReconstructStringFromBWT(string testDataName) : base(testDataName)
        {
            ExcludeTestCaseRangeInclusive(31, 40);
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, String>)Solve);

        public string Solve(string bwt)
        {
            int a, c, g, t;
            a = c = g = t = 0;

            List<(char, int)> originalBWT = new List<(char, int)>();
            List<(char, int)> sortedBWT = new List<(char, int)>();
            Dictionary<(char, int), (char, int)> mainData = new Dictionary<(char, int), (char, int)>();

            for (int i=0; i<bwt.Length; i++)
            {
                char current = bwt[i];
                if (current == 'A')
                {
                    originalBWT.Add(('A', a));
                    a++;
                }
                else if (current == 'C')
                {
                    originalBWT.Add(('C', a));
                    a++;
                }
                else
                    originalBWT.Add(('$', 0));
                

            }


            return null;
        }


    }
}
