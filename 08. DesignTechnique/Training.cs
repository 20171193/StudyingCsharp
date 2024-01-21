﻿using System;
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

            static void Main(string[] argc)
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

        //class 
    }
}
