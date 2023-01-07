using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace SAOD
{
    internal class Matrix
    {
        public static int[,] CreateRandomMatrix(int _i, int _j)
        {
            int[,] matrix = new int[_i, _j];
            var random = new Random();
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(0); j++)
                    matrix[i, j] = random.Next(1, 9);
            return matrix;
        }
        public static int[,] CreateSparseRandomMatrix(int _i, int _j)
        {
            int[,] matrix = new int[_i, _j];
            var random = new Random();
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(0); j++)
                    if (random.Next(0, 9) > 5)
                    {
                        matrix[i, j] = random.Next(0, 9);
                    }        
            return matrix;
        }
        public static int[,] ToTriagleMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = i + 1; j < matrix.GetLength(0); j++)
                    matrix[j, i] = 0;
            return matrix;
        }
        public static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public static int[] ToVectorMatrix(int[,] matrix)
        {
            List<int> vector = new();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    if (matrix[i, j] != 0)
                    {
                        vector.Add(matrix[i, j]);
                    }
                }
            }
            return vector.ToArray();
        }
        public static void PrintVector(int[] vector)
        {
            foreach (var elem in vector)
            {
                Console.Write(elem + " ");
            }
            Console.WriteLine("\n");
        }
        public static void PrintSparseVectors(List<List<int>> vectors)
        {
            foreach (var elem in vectors)
            {
                foreach (var e in elem)
                {
                    Console.Write(e + " ");
                }
                Console.WriteLine("\n");
            }
        }
        public static List<int[]> SplitByShapeVector(int[] vector, int shape)
        {
            List<int[]> shapes = new();
            List<int> vec = vector.ToList();
            int s = shape++;
            for (int i = 0; i < shape; i++)
            {
                List<int> row = new();
                int n = s - i;
                for (int j = 0; j < n; j++)
                {
                    row.Add(vec.FirstOrDefault());
                    vec.Remove(vec.FirstOrDefault());
                }
                shapes.Add(row.ToArray());

            }
            return shapes;
        }
        public static void PrintMatrix(List<int[]> matrix)
        {
            for (int i = 0; i < matrix.Count; i++)
            {
                foreach (var elem in matrix[i])
                {
                    Console.Write(elem + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public static List<int[]> ToMatrixVector(int[] vector, int shape)
        {
            List<int[]> matrix = new();
            List<int[]> shapes = SplitByShapeVector(vector, shape);
            for (int i = 0;  i < shapes.Count-1; i++)
            {
                List<int> row = shapes[i].ToList();
                for (int x = 0; x < shape - shapes[i].Length; x++)
                {
                    row = row.Prepend(0).ToList();
                }
                matrix.Add(row.ToArray());
            }
            return matrix;
        }
        public static List<List<int>> ToVectorsMatrix(int[,] matrix)
        {
            List<int> icoords = new();
            List<int> jcoords = new();
            List<int> values = new();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    if (matrix[i, j] != 0)
                    {
                        icoords.Add(i);
                        jcoords.Add(j);
                        values.Add(matrix[i, j]);
                    }
                }
            }
            return new List<List<int>> { icoords, jcoords, values };
        }
        public static int[,] MatrixFromVectors(List<List<int>> vectors, int shape)
        {
            int[,] matrix = new int[shape, shape];
            for (int i = 0; i < vectors[2].Count; i++)
            {
                matrix[vectors[0][i], vectors[1][i]] = vectors[2][i];
            }
            return matrix;
        }
    }
}
