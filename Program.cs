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

            //int[,] matrix = Matrix.ToTriagleMatrix(Matrix.CreateRandomMatrix(9, 9));
            //Matrix.PrintMatrix(matrix);
            //Matrix.PrintVector(Matrix.ToVectorMatrix(matrix));
            //Matrix.PrintMatrix(Matrix.ToMatrixVector(Matrix.ToVectorMatrix(matrix), 9));
            //int[,] smatrix = Matrix.CreateSparseRandomMatrix(9, 9);
            //Matrix.PrintMatrix(smatrix);
            //Matrix.PrintSparseVectors(Matrix.ToVectorsMatrix(smatrix));
            //Matrix.PrintMatrix(Matrix.MatrixFromVectors(Matrix.ToVectorsMatrix(smatrix), 9));
            //Matrix.PrintMatrix(Matrix.SplitByShapeVector(Matrix.ToVectorMatrix(matrix), 9));

            //Queen_8x8 chess = new();
            //chess.init(Chess.print_solution);
            //chess.solve();
            //Console.WriteLine($"Кол-во возможных решений: {chess.solution_count}");

            //double[] arr = Sorts.RandomArray(2000);
            //double[] arr1 = Sorts.HalfsortArray(2000);

            //Console.WriteLine($"{arr.Length} {arr1.Length}");

            //Sorts.direct(Sorts.ArrayCopy(arr));
            //Console.WriteLine($"Прямое включение & рандом // {Sorts.Count}");
            //Sorts.Count = 0;
            //Sorts.direct(Sorts.ArrayCopy(arr1));
            //Console.WriteLine($"Прямое включение & полусорт // {Sorts.Count}");
            //Sorts.Count = 0;

            //Sorts.select(Sorts.ArrayCopy(arr));
            //Console.WriteLine($"Прямой выбор & рандом // {Sorts.Count}");
            //Sorts.Count = 0;
            //Sorts.select(Sorts.ArrayCopy(arr1));
            //Console.WriteLine($"Прямое выбор & полусорт // {Sorts.Count}");
            //Sorts.Count = 0;

            //Sorts.swap(Sorts.ArrayCopy(arr));
            //Console.WriteLine($"Прямой обмен & рандом // {Sorts.Count}");
            //Sorts.Count = 0;
            //Sorts.swap(Sorts.ArrayCopy(arr1));
            //Console.WriteLine($"Прямое обмен & полусорт // {Sorts.Count}");
            //Sorts.Count = 0;

            //Sorts.shaker(Sorts.ArrayCopy(arr));
            //Console.WriteLine($"Шейкерный & рандом // {Sorts.Count}");
            //Sorts.Count = 0;
            //Sorts.shaker(Sorts.ArrayCopy(arr1));
            //Console.WriteLine($"Шейкерный & полусорт // {Sorts.Count}");
            //Sorts.Count = 0;

            //double[] arrcopy = Sorts.ArrayCopy(arr);
            //double[] arr1copy = Sorts.ArrayCopy(arr1);
            //Sorts.quick(arrcopy, 0, arrcopy.Length-1);
            //Console.WriteLine($"Быстрая & рандом // {Sorts.Count}");
            //Sorts.Count = 0;
            //Sorts.quick(arr1copy, 0, arr1copy.Length-1);
            //Console.WriteLine($"Быстрая & полусорт // {Sorts.Count}");
            //Sorts.Count = 0;

            //double[] arr2 = Sorts.RandomArray(5000);
            //double[] arr3 = Sorts.HalfsortArray(5000);
            //Sorts.quick(arr2, 0, arr2.Length - 1);
            //Console.WriteLine($"Быстрая & рандом // {Sorts.Count}");
            //Sorts.Count = 0;
            //Sorts.quick(arr3, 0, arr3.Length - 1);
            //Console.WriteLine($"Быстрая & полусорт // {Sorts.Count}");
            //Sorts.Count = 0;

            //double[] arr4 = Sorts.RandomArray(10000);
            //double[] arr5 = Sorts.HalfsortArray(10000);
            //Sorts.quick(arr4, 0, arr5.Length - 1);
            //Console.WriteLine($"Быстрая & рандом // {Sorts.Count}");
            //Sorts.Count = 0;
            //Sorts.quick(arr4, 0, arr5.Length - 1);
            //Console.WriteLine($"Быстрая & полусорт // {Sorts.Count}");
            //Sorts.Count = 0;

            //RandomGraph RndGraph = new(15);
            //RndGraph.PrintGraph();
            //MST.primMST(RndGraph.Graph);

            //DijkstaHandler DijHnd = new();
            //var g = new Graph();
            //var dijkstra = new Dijkstra(g);
            Map map = new("D:\\VSProjects\\SAOD\\lab.png");
            WaveAlg wave = new(map.lab);
            //int[] start_xy = map.GetStartCoords();
            //int[] end_xy = map.GetStartCoords();
            //wave.traceOut();
            wave.findPath(1, 78, 39, 1);
            wave.waveOut();
            map.CreateLabPathImg("D:\\VSProjects\\SAOD\\lab.png", wave.waveOutCoords());
            wave.traceOut();

        }
    }
}