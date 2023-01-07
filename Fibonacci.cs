using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAOD
{
    internal class Fibonacci
    {
        public static int Count;
        public static long imp_fib(int x)
        {
            if (x == 0) return 0;
            int prev = 0;
            int next = 1;
            for (int i = 1; i < x; i++)
            {
                int sum = prev + next;
                prev = next;
                next = sum;
                Count += 1;
            }
            return next;
        }
        public static long rec_fib(int n)
        {
            Count += 1;
            return (n < 2) ? n : rec_fib(n - 1) + rec_fib(n - 2);
        }
    }
}
