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
            Dictionary<(char, int), (char, int)> mainData = new Dictionary<(char, int), (char, int)>(); //نگاشت
            int n = bwt.Length;

            for (int i = 0; i < n; i++)
            {
                lastCol.Add((bwt[i], i));
                firstCol.Add((bwt[i], i));
            }

            firstCol.Sort();


            for (int i = 0; i < n; i++)
                mainData[firstCol[i]] = lastCol[i];

            StringBuilder reversedResult = new StringBuilder();

            (char, int) current = firstCol[0]; //($, 0)


            for (int i = 0; i < n; i++)
            {
                reversedResult.Append(current.Item1);
                current = mainData[current];
            }

            return new string(reversedResult.ToString().Reverse().ToArray());
        }


    }
}