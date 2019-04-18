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
            List<(char, int)> lastCol = new List<(char, int)>(); //original bwt transform with indexes
            List<(char, int)> firstCol = new List<(char, int)>(); //sorted bwt transform 
            Dictionary<(char, int), (char, int)> mainData = new Dictionary<(char, int), (char, int)>();

            for (int i=0; i<bwt.Length; i++)
                lastCol.Add((bwt[i], i));

            firstCol = lastCol;
            firstCol.Sort();

            for (int i = 0; i < firstCol.Count(); i++)
                mainData[firstCol[i]] = lastCol[i];

            string reversedResult = string.Empty;

            (char, int) current = firstCol[0]; //($, 0)


            while (reversedResult.Length != bwt.Length)
            {
                reversedResult += current.Item1;
                current = mainData[current];
            }

            string res = reversedResult.ToCharArray().Reverse().ToString();
            return res;
        }


    }
}
