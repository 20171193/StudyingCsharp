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
            static void Go(StringBuilder sb, int n, int m, int c)
            {
                if(c == m)
                {
                    Console.WriteLine(sb.ToString());
                    return;
                }

                for(int i=0; i<n; i++)
                {
                    StringBuilder temp = new StringBuilder();
                    temp.Append(sb.ToString());
                    temp.Append(i + 1);
                    temp.Append(' ');
                    Go(temp, n, m, c + 1);  // 
                    //Go(sb, n, m, c + 1);
                }
            }


            static void Main(string[] argc)
            {
                int n = 0, m = 0;
                string[] temp = Console.ReadLine().Split();
                n = int.Parse(temp[0]);
                m = int.Parse(temp[1]);

                Go(new StringBuilder(), n, m, 0);
            }
        }

        //class 
    }
}
