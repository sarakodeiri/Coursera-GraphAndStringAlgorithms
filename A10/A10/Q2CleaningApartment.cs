using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A3
{
    public class Q2CleaningApartment : Processor
    {
        public Q2CleaningApartment(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, int, long[,], string[]>)Solve);

        public string[] Solve(int V, int E, long[,] matrix)
        {
            int variableCount = V * V;
            int clauseCount = 2 * V + V * V * (V - 1) + ((V * (V - 1) / 2) - E) / (V - 1);

            List<string> result = new List<string>
            {
                $"{variableCount} {clauseCount}"
            };

            List<(long, long)> Edges = new List<(long, long)>();
            for (int i = 0; i < matrix.GetLength(0); i++)
                Edges.Add((matrix[i,0], matrix[i,1]));

            for (int i = 1; i <= V; i++)
            {
                int[] currentRow = new int[V];
                int[] currentCol = new int[V];

                for (int j = 1; j <= V; j++)
                {
                    currentRow[j - 1] = VarNum(j, i, V);
                    currentCol[j - 1] = VarNum(i, j, V);
                }

                result.Add(String.Join(" ", currentRow));
                result.Add(String.Join(" ", currentCol));

                for (int k = 0; k < currentRow.Length - 1; k++)
                    for (int b = k + 1; b < currentRow.Length; b++)
                    {
                        result.Add($"-{currentRow[k]} -{currentRow[b]}");
                        result.Add($"-{currentCol[k]} -{currentCol[b]}");
                    }
            }

            for (int k = 0; k<V; k++)
                for (int i=1; i<=V; i++)
                    for (int j=1; j<=V; j++)
                    {
                        if (!Edges.Contains((i, j)) && !Edges.Contains((j, i)))
                        {
                            int one = VarNum(k, i, V);
                            int two = VarNum(k+1, j, V);
                            result.Add($"-{one} -{two}");
                        }

                    }
            


            return result.ToArray();
        }

        private static int VarNum(int place, int nodeNum, int nodeCount) 
            => nodeNum * nodeCount + place - nodeCount;

        public override Action<string, string> Verifier { get; set; } =
            TestTools.SatVerifier;
    }
}
