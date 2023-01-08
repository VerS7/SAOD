using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SAOD
{
    internal class Sorts
    {
        public static int Count;

        public static double[] ArrayCopy(double[] array)
        {
            double[] copy = new double[array.Length];
            Array.Copy(array, copy, array.Length);
            return copy;
        }
        public static double[] RandomArray(int len)
        {
            double[] array = new double[len];
            Random rnd = new();
            for (int i = 0; i < len; i++)
            {
                array[i] = rnd.Next(-1000, 1000);
            }
            return array;
        }
        public static double[] HalfsortArray(int len)
        {
            int[] firsthalf = Enumerable.Range(1000, 1000).ToArray();
            Array.Sort(firsthalf);
            double[] secondhalf = RandomArray(len-1000);
            double[] newarray = new double[firsthalf.Length + secondhalf.Length];
            Array.Copy(firsthalf, newarray, firsthalf.Length);
            Array.Copy(secondhalf, 0, newarray, firsthalf.Length, secondhalf.Length);
            return newarray;
        }
        public static double[] direct(double[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                double value = array[i]; 
                int index = i;
                while ((index > 0) && (array[index - 1] > value))
                {
                    Count += 1;
                    array[index] = array[index - 1];
                    index--;   
                }
                array[index] = value;
            }
            return array;
        }
        public static double[] select(double[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[j] < array[min])
                    {
                        Count += 1;
                        min = j;
                    }
                }
                double temp = array[min];
                array[min] = array[i];
                array[i] = temp;
            }
            return array;
        }
        public static double[] swap(double[] array)
        {
            double temp;
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[i] > array[j])
                    {
                        Count += 1;
                        temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                }
            }
            return array;
        }
        static void Swap(ref double e1, ref double e2)
        {
            var temp = e1;
            e1 = e2;
            e2 = temp;
        }
        public static double[] shaker(double[] array)
        {
            for (var i = 0; i < array.Length / 2; i++)
            {
                var swapFlag = false;
                for (var j = i; j < array.Length - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        Count += 1;
                        Swap(ref array[j], ref array[j + 1]);
                        swapFlag = true;
                    }
                }
                for (var j = array.Length - 2 - i; j > i; j--)
                {
                    if (array[j - 1] > array[j])
                    {
                        Count += 1;
                        Swap(ref array[j - 1], ref array[j]);
                        swapFlag = true;
                    }
                }
                if (!swapFlag)
                {
                    break;
                }
            }
            return array;
        }
        public static double[] quick(double[] array)
        {

        }
    }
}
