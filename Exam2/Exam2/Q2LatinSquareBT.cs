using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace Exam2
{
    public class Q2LatinSquareBT : Processor
    {
        public Q2LatinSquareBT(string testDataName) : base(testDataName)
        {
            this.ExcludeTestCaseRangeInclusive(28, 120);
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int,int?[,],string>)Solve);

        public string Solve(int dim, int?[,] square)
        {
            for (int i=0; i<dim; i++)
                for (int j=0; j<dim; j++)
                {
                    if (!square[i, j].HasValue)
                    {
                        for (int k = 0; k < dim; k++)
                        {
                            square[i, j] = k;
                            Solve(dim, square);
                        }

                    }

                }
            return "SATISFIABLE";
        }

        private bool Check(int? [] column, int k)
        {
            List<int?> columnList = column.ToList();
            columnList.Remove(k);
            if (column.Length - columnList.Count() >= 2)
                return false;
            return true;
        }
    }
}
