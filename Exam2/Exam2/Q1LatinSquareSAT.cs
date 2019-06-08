using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace Exam2
{
    public class Q1LatinSquareSAT : Processor
    {
        public Q1LatinSquareSAT(string testDataName) : base(testDataName)
        {}

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int,int?[,],string>)Solve);

        public override Action<string, string> Verifier =>
            TestTools.SatVerifier;


        public string Solve(int dim, int?[,] square)
        {
            int variableCount = dim * dim * dim;
            //int clauseCount = ((dim * (dim - 1) / 2) + 1) * ((dim * dim * 2) + 1); //NOT CORRECT

            List<string> result = new List<string>();

            //Each space should have at least and at most one number in it
            for (int i = 0; i < dim; i++)
                for (int j = 0; j < dim; j++)
                {
                    int[] atLeastOneNum = new int[dim];
                    for (int k = 0; k < dim; k++)
                        atLeastOneNum[k] = VarNum(i, j, k, dim);
                    result.Add(string.Join(" ", atLeastOneNum));

                    for (int k = 0; k < atLeastOneNum.Length - 1; k++)
                        for (int b = k + 1; b < atLeastOneNum.Length; b++)
                            result.Add($"-{atLeastOneNum[k]} -{atLeastOneNum[b]}");
                }

            //Check each row
            for (int i=0; i<dim; i++)
                for (int k = 0; k < dim; k++)
                {
                    int[] rowChecker = new int[dim];
                    int[] columnChecker = new int[dim];

                    for (int j = 0; j < dim; j++)
                    {
                        columnChecker[j] = VarNum(j, i, k, dim);
                        rowChecker[j] = VarNum(i, j, k, dim);
                    }

                    result.Add(string.Join(" ", rowChecker));
                    result.Add(string.Join(" ", columnChecker));

                    for (int x = 0; x < rowChecker.Length - 1; x++)
                        for (int b = x + 1; b < rowChecker.Length; b++)
                        {
                            result.Add($"-{rowChecker[x]} -{rowChecker[b]}");
                            result.Add($"-{columnChecker[x]} -{columnChecker[b]}");
                        }
                }

            ////Check each column
            //for (int i = 0; i < dim; i++)
            //    for (int k = 0; k < dim; k++)
            //    {
            //        int[] checker = new int[dim];
            //        for (int j = 0; j < dim; j++)
            //            checker[j] = VarNum(j, i, k, dim);

            //        result.Add(string.Join(" ", checker));

            //        for (int x = 0; x < checker.Length - 1; x++)
            //            for (int b = x + 1; b < checker.Length; b++)
            //    }




            //Check with already filled spots
            for (int i = 0; i < dim; i++)
                for (int j = 0; j < dim; j++)
                    if (square[i, j].HasValue)
                    {
                        int? num = square[i, j].Value;

                        result.Add($"{VarNum(i, j, num.Value, dim)}");

                        for (int row = 0; row < dim; row++)
                            if (row != j)
                                result.Add($"-{VarNum(i, row, num.Value, dim)}");

                        for (int col = 0; col < dim; col++)
                            if (col != i)
                                result.Add($"-{VarNum(col, j, num.Value, dim)}");
                        
                    }

            int clauseCount = result.Count() - 1;
            result.Insert(0, $"{variableCount} {clauseCount}");

            return string.Join("\n", result);
        }

        private static int VarNum(int i, int j, int k, int dim) => i*dim*dim + j*dim + k + 1;
    }
}
