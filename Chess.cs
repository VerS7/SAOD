using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SAOD
{
    delegate void Solution(int[] queens);
    internal class Chess
    {
        static void print(String s)
        {
            Console.WriteLine(s);
        }

        public static void print_solution(int[] queens)
        {
            String s = "";
            for (int i = 0; i < queens.Length; i++)
            {
                s += Char.ConvertFromUtf32(i + 65) + (queens[i] + 1).ToString();
                if (i < (queens.Length - 1)) s += ", ";
            }
            print(s);
        }
    }
    class Queen_8x8
    {
        protected Solution solution;

        public const int queens_count = 8;
        public const int diag_count = queens_count * 2 - 1;

        public ulong markers = 0xFFFF_FFFF_FFFF_FFFF;
        public int[] queens = new int[queens_count];

        ulong[] diag_LR = new ulong[diag_count];
        ulong[] diag_RL = new ulong[diag_count];


        ulong attacks_HZ = 0;
        ulong attacks_LR = 0;
        ulong attacks_RL = 0;

        public bool smart_check = true;

        public int solution_count = 0;
        public int solve_count = 0;
        public int reverse_count = 0;

        private int get_LR(int x, int y) { return x + y; }
        private int get_RL(int x, int y) { return (queens_count - 1 - x) + y; }


        public void init(Solution solution)
        {
            this.solution = solution;
            solution_count = 0;
            solve_count = 0;
            reverse_count = 0;
            markers = 0xFFFF_FFFF_FFFF_FFFF;
            for (int i = 0; i < queens.Length; i++)
            {
                queens[i] = -1;
            }
            for (int i = 0; i < diag_count; i++)
            {
                diag_LR[i] = 0;
                diag_RL[i] = 0;
            }
            for (int x = 0; x < queens_count; x++)
            {
                for (int y = 0; y < queens_count; y++)
                {
                    ulong mask = 1UL << ((x << 3) + y);
                    diag_LR[get_LR(x, y)] |= mask;
                    diag_RL[get_RL(x, y)] |= mask;
                }
            }
        }
        protected void step(int qx, int qy)
        {
            ulong q_CM = 0xFFUL << (qx << 3);
            ulong a_HZ = 0x0101_0101_0101_0101UL << qy;
            ulong a_LR = diag_LR[get_LR(qx, qy)];
            ulong a_RL = diag_RL[get_RL(qx, qy)];
            queens[qx] = qy;
            markers &= ~q_CM;
            attacks_HZ |= a_HZ;
            attacks_LR |= a_LR;
            attacks_RL |= a_RL;
            solve();
            attacks_HZ &= ~a_HZ;
            attacks_LR &= ~a_LR;
            attacks_RL &= ~a_RL;
            markers |= q_CM;
            queens[qx] = -1;
        }

        public void solve()
        {
            if (markers == 0)
            {
                solution_count++;
                solution(queens);
                return;
            }
            ulong attacks = attacks_HZ | attacks_LR | attacks_RL;
            ulong sum_vec_8 = BitOp.popcnt64_bv(attacks);
            ulong test = sum_vec_8 & 0x0808_0808_0808_0808UL;
            test &= markers;

            if (smart_check && test != 0)
            {
                return; 
            }
            test = sum_vec_8 + 0x0101_0101_0101_0101UL;
            test &= 0x0808_0808_0808_0808UL;
            test &= markers;
            int qx;
            int qy;
            ulong column;
            if (smart_check && test != 0)
            {
                reverse_count++;
                qx = BitOp.bsf64((test >> 3)) >> 3;
                column = (~attacks >> (qx << 3)) & 0xFFUL;
                qy = BitOp.bsf64(column);

                step(qx, qy);
                return;
            }
            qx = BitOp.bsf64(markers) >> 3;
            column = (~attacks >> (qx << 3)) & 0xFFUL;
            while (column != 0)
            {
                solve_count++;
                qy = BitOp.bsf64(column);
                column &= column - 1;
                step(qx, qy);
            }
            return;
        }
    }
    class BitOp
    {
        public static ulong popcnt64_bv(ulong value)
        {
            ulong result = value;
            result = result - ((result >> 1) & 0x5555_5555_5555_5555UL); 
            result = (result & 0x3333_3333_3333_3333UL) + ((result >> 2) & 0x3333_3333_3333_3333UL);
            result = (result + (result >> 4)) & 0x0F0F_0F0F_0F0F_0F0F;
            return result;
        }
        public static byte add64_bv(ulong value)
        {
            ulong result = value;
            result += result >> 8;
            result += result >> 16; 
            result += result >> 32; 
            return (byte)(result);
        }
        public static byte popcnt64(ulong value)
        {
            ulong result = value;
            result = result - ((result >> 1) & 0x5555_5555_5555_5555UL);  
            result = (result & 0x3333_3333_3333_3333UL) + ((result >> 2) & 0x3333_3333_3333_3333UL); 
            result = (result + (result >> 4)) & 0x0F0F_0F0F_0F0F_0F0F;
            result += result >> 8;
            result += result >> 16; 
            result += result >> 32; 
            return (byte)(result);
        }
        public static byte popcnt8(byte value)
        {
            ulong result = value;
            result = result - ((result >> 1) & 0x55); 
            result = (result & 0x33) + ((result >> 2) & 0x33); 
            result = (result + (result >> 4)) & 0x0F; 
            return (byte)(result);
        }
        public static int bsf64(ulong value)
        {
            if (value == 0) return -1;
            int bitpos = 0;

            ulong mask = 0xFFFF_FFFF_FFFF_FFFFUL;
            int step = 64;

            while ((step >>= 1) > 0)
            {
                if ((value & (mask >>= step)) == 0)
                {
                    value >>= step;
                    bitpos |= step;
                }
            }
            return bitpos;
        }
    }
}
