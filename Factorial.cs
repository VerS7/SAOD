using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SAOD
{
    internal class Factorial
    {
        public static int Count;
        public static long imp_fac(int n)
        {
            long factorial = 1;
            for (int i = 1; i <= n; i++)
            {
                factorial *= i;
                Count += 1;
            }
            return factorial;
        }
        public static long rec_fac(int n)
        {
            if (n == 0)
            {
                Count += 1;
                return 1;
            }
            else
            {
                Count += 1;
                return n * rec_fac(n - 1);
            }
        }
        public static long linq_fac(int n)
        {
            long factorial = 1;
            Count += n;
            return Enumerable.Range(1, n).Aggregate(factorial, (f, i) => f * i);
        }
    }
}
