using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _08._DesignTechnique
{
    class Training
    {
        class NandM3
        {
            void Main1()
            {
                int n = 0, m = 0;
                string[] temp = Console.ReadLine().Split();
                n = int.Parse(temp[0]);
                m = int.Parse(temp[1]);
                StringBuilder sb = new StringBuilder();
                Go(new int[m], sb, n, m, 0);
                Console.WriteLine(sb.ToString());
            }

            static void Go(int[] arr, StringBuilder sb, int n, int m, int c)
            {
                if (c == m)
                {
                    for (int i = 0; i < arr.Length; i++)
                    {
                        sb.Append(arr[i]);
                        sb.Append(' ');
                    }
                    sb.AppendLine();
                    return;
                }

                for (int i = 0; i < n; i++)
                {
                    arr[c] = i + 1;
                    Go(arr, sb, n, m, c + 1);
                }
            }

        }

        class ATM
        {
            // 누적합
            public static int n = 0, sum = 0, preSum;
            public static int[] arr;
            void Main2()
            {
                n = int.Parse(Console.ReadLine());
                string[] temp = Console.ReadLine().Split();
                arr = new int[n];
                for(int i=0; i<n; i++)
                {
                    arr[i] = int.Parse(temp[i]);
                }
                Array.Sort(arr);

                foreach(int i in arr)
                {
                    preSum += i;
                    sum += preSum;
                }
                Console.WriteLine(sum);
            }
        }

        class MakeConfetti
        {
            // 분할정복 풀이
            static int[,] arr;
            static int n = 0, white = 0, blue = 0;
            static void Main(string[] argc)
            {
                n = int.Parse(Console.ReadLine());
                arr = new int[n, n];
                for (int i = 0; i < n; i++)
                {
                    string[] temp = Console.ReadLine().Split(' ');
                    for (int j = 0; j < n; j++)
                    {
                        arr[i, j] = int.Parse(temp[j]);
                    }
                }

                Go(0, 0, n);

                Console.WriteLine(white);
                Console.WriteLine(blue);    
            }

            static void Go(int x, int y, int _n)
            {
                int whiteSpace = 0;
                for(int i=y; i<_n+y; i++)
                {
                    for(int j=x; j<_n+x; j++)
                    {
                        if (arr[i, j] == 0) whiteSpace++;
                    }
                }

                if (whiteSpace == 0)
                {
                    blue++;
                }
                else if (whiteSpace == _n * _n)
                {
                    white++;
                }
                else
                {
                    Go(x, y, _n / 2);
                    Go(x + _n / 2, y, _n / 2);
                    Go(x, y + _n / 2, _n / 2);
                    Go(x + _n / 2, y + _n / 2, _n / 2);
                }
            }
        }
    }
}
