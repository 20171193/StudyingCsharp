using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Training
{
    public class CheatKey
    {
        private Dictionary<string, Action> cheatDic;

        private bool loop;

        public event Action OnMoneyCheat;
        public event Action OnWinCheat;

        public CheatKey()
        {
            OnMoneyCheat += ShowMeTheMoney;
            OnWinCheat += ThereIsNoCowLevel;

            cheatDic = new Dictionary<string, Action>();
            cheatDic.Add("showmethemoney", OnMoneyCheat);
            cheatDic.Add("thereisnocowlevel", OnWinCheat);
            loop = true;
        }

        public void Run(string cheatKey)
        {
            // 치트키 변환
            //  1. 띄어쓰기 제거
            //  2. 대문자 -> 소문자 변환
            string cheat = cheatKey.ToLower();
            cheatDic.TryGetValue(cheat, out Action act);
            act?.Invoke();
        }
        public void ShowMeTheMoney()
        {
            Console.WriteLine("골드를 늘려주는 치트키 발동!\n");
        }
        public void ThereIsNoCowLevel()
        {
            Console.WriteLine("바로 승리합니다 치트키 발동!\n");
        }

        public void Render()
        {
            Console.Write("치트키를 입력하세요:");
        }
        public void Input()
        {
            Run(Console.ReadLine());
        }
        public void RunGame()
        {
            while (loop)
            {
                Render();
                Input();
            }
        }
    }

    public class Program
    {
        void Main2()
        {
            CheatKey cheatKey = new CheatKey();
            cheatKey.RunGame();
        }
    }
}


namespace CodingTest
{
    #region 백준 17296 비밀번호 만들기
    //public class Program
    //{
    //    static void Main(string[] argc)
    //    {
    //        Dictionary<string, string> dic = new Dictionary<string, string>();
    //        List<string> answer = new List<string>();
    //        int n = 0, m = 0;
    //        string[] nm = Console.ReadLine().Split(' ');
    //        n = int.Parse(nm[0]);
    //        m = int.Parse(nm[1]);

    //        for (int i = 0; i < n; i++)
    //        {
    //            string[] temp = Console.ReadLine().Split(' ');
    //            dic.Add(temp[0], temp[1]);
    //        }

    //        for (int i = 0; i < m; i++)
    //        {
    //            answer.Add(dic[Console.ReadLine()]);
    //        }
    //        foreach (string a in answer)
    //        {
    //            Console.WriteLine(a);
    //        }
    //    }
    //}
    #endregion
    #region 백준 5568번 카드 놓기
    class Program
    {
        static public HashSet<string> set = new HashSet<string>();
        
        static void Perm(string[] list, string[] result, bool[] used, int depth,int n, int r)
        {
            if(depth == r)
            {
                StringBuilder sb = new StringBuilder();
                foreach(string s in result)
                {
                    sb.Append(s);
                }
                set.Add(sb.ToString());
                return;
            }

            for(int i = 0; i< n; i++)
            {
                if (!used[i])
                {
                    result[depth] = list[i];
                    used[i] = true;
                    Perm(list, result, used, depth + 1, n, r);
                    used[i] = false;
                }
            }
        }

        static void Main(string[] argc)
        {
            int n = 0, k = 0;
            n = int.Parse(Console.ReadLine());
            k = int.Parse(Console.ReadLine());
            bool[] used = new bool[n];
            string[] str = new string[n];
            for (int i=0; i<n; i++)
            {
                str[i] = Console.ReadLine();
            }

            Perm(str, new string[n], used, 0, n, k);

            Console.WriteLine(set.Count);
        }
    }
    #endregion
}

