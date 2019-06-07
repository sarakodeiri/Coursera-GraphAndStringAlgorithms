using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A9
{
    public class Q2OptimalDiet : Processor
    {
        public Q2OptimalDiet(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int,int, double[,], String>)Solve);

        public string Solve(int N,int M, double[,] matrix1)
        {

            double[] finalExpression = new double[M];
            for (int i = 0; i < finalExpression.Length; i++)
                finalExpression[i] = matrix1[N,i];

            double[][] allEquations = new double[N + M][];
            for (int i=0; i<allEquations.Length; i++)
                allEquations[i] = new double[M + 1];

            for (int i=0; i<allEquations.GetLength(0); i++)
                for (int j=0; j <= M; j++)
                {
                    if (i < N)
                        allEquations[i][j] = matrix1[i, j];
                    else if (j == i - N)
                        allEquations[i][j] = 1;
                }

            



            return "result";
        }

    }
}
