using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixMultiply
{
    internal class Matrix
    {
        public int Rows { get; }
        public int Columns { get; }
        public double[,] Data { get; }

        private static Random random = new Random();

        public Matrix(int rows, int cols)
        {
            Rows = rows;
            Columns = cols;
            Data = new double[rows, cols];
        }

        public void FillRandom()
        {
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                    Data[i, j] = random.NextDouble() * 10;
        }

        public void Print()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                    Console.Write($"{Data[i, j]:F2}\t");
                Console.WriteLine();
            }
        }
    }
}
