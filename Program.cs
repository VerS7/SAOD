using System;
using System.Numerics;

namespace SAOD
{
    class MainProgram
    {
        public static void Main()
        {
            //Console.WriteLine($"{Factorial.imp_fac(17)}, {Factorial.Count}");
            //Factorial.Count = 0;
            //Console.WriteLine($"{Factorial.rec_fac(17)}, {Factorial.Count}");
            //Factorial.Count = 0;
            //Console.WriteLine($"{Factorial.linq_fac(17)}, {Factorial.Count}");
            //Console.WriteLine($"{Fibonacci.imp_fib(25)}, {Fibonacci.Count}");
            //Fibonacci.Count = 0;
            //Console.WriteLine($"{Fibonacci.rec_fib(20)}, {Fibonacci.Count}");

            int[,] matrix = Matrix.ToTriagleMatrix(Matrix.CreateRandomMatrix(9, 9));
            Matrix.PrintMatrix(matrix);
            Matrix.PrintVector(Matrix.ToVectorMatrix(matrix));
            Matrix.PrintMatrix(Matrix.ToMatrixVector(Matrix.ToVectorMatrix(matrix), 9));
            int[,] smatrix = Matrix.CreateSparseRandomMatrix(9, 9);
            Matrix.PrintMatrix(smatrix);
            Matrix.PrintSparseVectors(Matrix.ToVectorsMatrix(smatrix));
            Matrix.PrintMatrix(Matrix.MatrixFromVectors(Matrix.ToVectorsMatrix(smatrix), 9));
            //
            //Matrix.PrintMatrix(Matrix.SplitByShapeVector(Matrix.ToVectorMatrix(matrix), 9));
        }
    }
}