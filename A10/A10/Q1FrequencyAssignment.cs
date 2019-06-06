using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TestCommon;

namespace A3
{
    public class Q1FrequencyAssignment : Processor
    {
        public Q1FrequencyAssignment(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, int, long[,], string[]>)Solve);

        public string[] Solve(int V, int E, long[,] matrix)
        {
            int variableCount = 3 * V;
            int clauseCount = 4 * V + 3 * E;

            List<string> result = new List<string>();

            for (int i=1; i<=V; i++)
            {
                string[] atLeastOneColor = new string[3];
                for (int j = 0; j < atLeastOneColor.Length; j++)
                    atLeastOneColor[j] = VarNum(i, j).ToString();
                result.Add($"{atLeastOneColor[0]} {atLeastOneColor[1]} {atLeastOneColor[2]}");

                for (int k = 0; k < atLeastOneColor.Length - 1; k++)
                    for (int b = k + 1; b < atLeastOneColor.Length; b++)
                        result.Add($"-{atLeastOneColor[k]} -{atLeastOneColor[b]}");
            }

            for (int i=0; i<matrix.GetLength(0); i++)
            {
                for (int j=0; j<3; j++)
                {
                    int firstValue = VarNum((int)matrix[i, 0], j);
                    int secondValue = VarNum((int)matrix[i, 1], j);
                    result.Add($"-{firstValue} -{secondValue}");
                }
            }

            return result.ToArray();
        }

        private static int VarNum(int n, int c) => n * 3 + c - 2;

        public override Action<string, string> Verifier { get; set; } =
            TestTools.SatVerifier;
    }
}
