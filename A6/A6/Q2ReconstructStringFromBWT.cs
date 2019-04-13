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
            //ExcludedTestCases(31, 40);
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, String>)Solve);

        public string Solve(string bwt)
        {

            List<(char, int)> lastColumn = new List<(char, int)>();
            List<(char, int)> firstColumn = new List<(char, int)>();


            


            firstColumn = lastColumn;
            firstColumn.Sort();

            return null;

        }
        
    }
}
