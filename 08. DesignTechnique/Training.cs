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

            static int PrefixSum(int _n, int _sum, int _preSum)
            {
                if (_n >= n) return _sum;
                _preSum += arr[_n];
                _sum += _preSum;
                return PrefixSum(_n + 1, _sum, _preSum);
            }

            void Main2()
            {
                n = int.Parse(Console.ReadLine());
                string[] temp = Console.ReadLine().Split();
                arr = new int[n];
                for (int i = 0; i < n; i++)
                {
                    arr[i] = int.Parse(temp[i]);
                }
                Array.Sort(arr);

                // 반복문 사용
                //foreach (int i in arr)
                //{
                //    preSum += i;
                //    sum += preSum;
                //}
                //Console.WriteLine(sum);

                // 재귀 사용
                Console.WriteLine(PrefixSum(0, 0, 0));
            }
        }

        class MakeConfetti
        {
            // 분할정복 풀이
            static int[,] arr;
            static int n = 0, white = 0, blue = 0;
            void Main3()
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
                for (int i = y; i < _n + y; i++)
                {
                    for (int j = x; j < _n + x; j++)
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

        class IntegerTriangle
        {
            static int n = 0, sum = 0;
            static int[,] dp;

            void Main4()
            {
                n = int.Parse(Console.ReadLine());
                dp = new int[n, n];
                for (int i = 0; i < n; i++)
                {
                    string[] temp = Console.ReadLine().Split(' ');
                    for (int j = 0; j < i + 1; j++)
                    {
                        dp[i, j] = int.Parse(temp[j]);
                    }
                }

                for (int i = 1; i < n; i++)
                {
                    dp[i, 0] += dp[i - 1, 0];
                    for (int j = 1; j < i + 1; j++)
                    {
                        if (dp[i - 1, j] < dp[i - 1, j - 1])
                        {
                            dp[i, j] += dp[i - 1, j-1];
                        }
                        else
                        {
                            dp[i, j] += dp[i - 1, j];
                        }
                    }
                }

                for (int i = 0; i < n; i++)
                {
                    sum = Math.Max(dp[n - 1, i], sum);
                }
                Console.WriteLine(sum);
            }
        }

        class HanoiTower
        {
            // n 원판이 1번 기둥의 꼭대기,
            // n-1 원판이 2번 기둥의 꼭대기에 위치해야함. 

            // 1 - 3
            // 1 - 2
            // 3 - 2
            // 1 - 3
            // 2 - 1
            // 2 - 3

            static int n = 0, k = 0;
            static Stack<int>[] st;
            static StringBuilder sb = new StringBuilder();
            static void Go(int _n, int start, int end)
            {
                int middle = 3 - (start + end);
                if (_n == 1)
                {
                    k++;
                    st[end].Push(st[start].Pop());
                    if (n > 20) return;
                    sb.Append(start + 1);
                    sb.Append(" ");
                    sb.Append(end + 1);
                    sb.AppendLine();
                    return;
                }
                Go(_n - 1, start, middle);
                Go(1, start, end);
                Go(_n - 1, middle, end);
            }
            static void Main(string[] argc)
            {
                n = int.Parse(Console.ReadLine());
                st = new Stack<int>[3];
                st[0] = new Stack<int>();
                st[1] = new Stack<int>();
                st[2] = new Stack<int>();

                for(int i=n; i>=1; i--)
                {
                    st[0].Push(i);
                }
                Go(n, 0, 2);

                Console.WriteLine(k);
                if (n > 20) return;
                Console.WriteLine(sb.ToString());
            }
        }
    }
}
