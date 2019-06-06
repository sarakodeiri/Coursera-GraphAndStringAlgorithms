using System;
using TestCommon;

namespace A9
{
    public class Q1InferEnergyValues : Processor
    {
        public Q1InferEnergyValues(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, double[,], double[]>)Solve);

        public double[] Solve(long MATRIX_SIZE, double[,] matrix)
        {
            ReducedRowEchelonForm(matrix);

            int rowCount = matrix.GetLength(0);
            int columnCount = matrix.GetLength(1);

            double val = 0;
            double[] result = new double[MATRIX_SIZE];

            for (int i = rowCount - 1; i >= 0; i--)
            {
                val = matrix[i, columnCount - 1];

                for (int x = columnCount - 2; x > i - 1; x--)
                    val -= matrix[i, x] * result[x];
                
                result[i] = val / matrix[i, i];

            }

          //Proximity handler
            for (int i = 0; i < result.Length; i++)
                    result[i] = Math.Round(result[i] * 2) / 2;

            return result;
        }


        private static void ReducedRowEchelonForm(double[,] matrix)
        {
            int pivot = 0;
            int rowCount = matrix.GetLength(0);
            int columnCount = matrix.GetLength(1);

            for (int r = 0; r < rowCount; r++)
            {
                if (columnCount <= pivot)
                    break;

                int i = r;

                while (matrix[i, pivot] == 0)
                {
                    i++;
                    if (i == rowCount)
                    {
                        i = r;
                        pivot++;
                        if (columnCount == pivot)
                        {
                            pivot--;
                            break;
                        }
                    }
                }

                for (int j = 0; j < columnCount; j++) //Swap
                {
                    double temp = matrix[r, j];
                    matrix[r, j] = matrix[i, j];
                    matrix[i, j] = temp;
                }

                double div = matrix[r, pivot];

                if (div != 0)
                    for (int j = 0; j < columnCount; j++)
                        matrix[r, j] /= div;

                for (int j = 0; j < rowCount; j++)
                {
                    if (j != r)
                    {
                        double sub = matrix[j, pivot];
                        for (int k = 0; k < columnCount; k++)
                            matrix[j, k] -= (sub * matrix[r, k]);
                    }
                }

                pivot++;
            }
        }
    }
}
