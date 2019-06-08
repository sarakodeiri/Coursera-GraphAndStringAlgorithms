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
            List <(int, int) > modified = new List<(int, int)>();

            for (int i=0; i<dim; i++)
                for (int j=0; j<dim; j++)
                {
                    if (!square[i, j].HasValue)
                    {
                        modified.Add((i, j));

                        int?[] column = new int?[dim];
                        int?[] row = new int?[dim];
                        for (int b = 0; b < dim; b++)
                        {
                            row[b] = square[i, b];
                            column[b] = square[b, j];
                        }

                        for (int k = 0; k < dim;)
                        {
                            if (row.Contains(k) || column.Contains(k))
                            {
                                if (k == dim - 1)
                                    return "UNSATISFIABLE";
                                k++;
                            }

                            else
                            {
                                square[i, j] = k;
                                break;
                            }

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
