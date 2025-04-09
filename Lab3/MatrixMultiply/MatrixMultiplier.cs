using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixMultiply
{
    internal class MatrixMultiplier
    {
        public static Matrix Multiply(Matrix a, Matrix b, int threadCount)
        {
            if (a.Columns != b.Rows)
                throw new ArgumentException("Nie można mnożyć tych macierzy.");

            Matrix result = new Matrix(a.Rows, b.Columns);

            ParallelOptions options = new ParallelOptions { MaxDegreeOfParallelism = threadCount };

            Parallel.For(0, a.Rows, options, i =>
            {
                for (int j = 0; j < b.Columns; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < a.Columns; k++)
                        sum += a.Data[i, k] * b.Data[k, j];
                    result.Data[i, j] = sum;
                }
            });

            return result;
        }
        public static Matrix MultiplyWithThreads(Matrix a, Matrix b, int threadCount)
        {
            if (a.Columns != b.Rows)
                throw new ArgumentException("Nie można mnożyć tych macierzy.");

            Matrix result = new Matrix(a.Rows, b.Columns);
            Thread[] threads = new Thread[threadCount];
            int rowsPerThread = a.Rows / threadCount;

            for (int t = 0; t < threadCount; t++)
            {
                int startRow = t * rowsPerThread;
                int endRow = (t == threadCount - 1) ? a.Rows : startRow + rowsPerThread;

                threads[t] = new Thread(() =>
                {
                    for (int i = startRow; i < endRow; i++)
                    {
                        for (int j = 0; j < b.Columns; j++)
                        {
                            double sum = 0;
                            for (int k = 0; k < a.Columns; k++)
                                sum += a.Data[i, k] * b.Data[k, j];
                            result.Data[i, j] = sum;
                        }
                    }
                });

                threads[t].Start();
            }

            foreach (var thread in threads)
                thread.Join();

            return result;
        }
    }
}
