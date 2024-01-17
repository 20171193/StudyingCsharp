using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 백준 13904번 - 과제
// 웅찬이는 과제가 많다.
// 하루에 한 과제를 끝낼 수 있는데, 과제마다 마감일이 있으므로 모든 과제를 끝내지 못할 수도 있다.
// 과제마다 끝냈을 때 얻을 수 있는 점수가 있는데, 마감일이 지난 과제는 점수를 받을 수 없다.
// 웅찬이는 가장 점수를 많이 받을 수 있도록 과제를 수행하고 싶다.
// 웅찬이를 도와 얻을 수 있는 점수의 최댓값을 구하시오.

// 첫 줄에 정수 N (1 ≤ N ≤ 1,000)이 주어진다.
// 다음 줄부터 N개의 줄에는 각각 두 정수 d (1 ≤ d ≤ 1,000)와 w(1 ≤ w ≤ 100)가 주어진다.
// d는 과제 마감일까지 남은 일수를 의미하며, w는 과제의 점수를 의미한다.

// dp풀이
// 
namespace _06._Heap
{
    internal class Homework
    {
        private int n;
        private int maxDay;
        private int ret;
        PriorityQueue<int, int> sttPq;
        Dictionary<int, PriorityQueue<int, int>> dic;
        public Homework(int n)
        {
            this.n = n;
            maxDay = 0;
            ret = 0;
            sttPq = new PriorityQueue<int, int>();
            dic = new Dictionary<int, PriorityQueue<int, int>>();
            InputSetting();
        }

        public void InputSetting()
        {
            for (int i = 0; i < n; i++)
            {
                string[] temp = Console.ReadLine().Split(' ');
                int day = int.Parse(temp[0]);
                if (day >= n) continue;
                int value = int.Parse(temp[1]);
                maxDay = Math.Max(maxDay, day);
                if (dic.ContainsKey(day))
                {
                    dic[day].Enqueue(value, -value);
                }
                else
                {
                    PriorityQueue<int, int> _pq = new PriorityQueue<int, int>();
                    _pq.Enqueue(value, -value);
                    dic.Add(day, _pq);
                }
                //// order = 숙제하는데 소요되는 시간
                //pq.Enqueue(int.Parse(temp[1]), int.Parse(temp[0]));
            }

            for (int i = maxDay; i >= 1; i--)
            {
                if (dic.ContainsKey(i))
                {
                    if (sttPq.Count > 0 && sttPq.Peek() > dic[i].Peek()) ret += sttPq.Dequeue();
                    else ret += dic[i].Dequeue();

                    while (dic[i].Count > 0)
                    {
                        int temp = dic[i].Dequeue();
                        sttPq.Enqueue(temp, -temp);
                    }
                }
                else if (sttPq.Count > 0)
                {
                    ret += sttPq.Dequeue();
                }
            }
        }
        public void PrintResult()
        {
            Console.WriteLine(ret);
        }


        void Main1()
        {
            int n = int.Parse(Console.ReadLine());
            Homework homework = new Homework(n);
            homework.PrintResult();
        }
    }

    class OfficeRoom
    {
        public struct Dist
        {
            public int start;
            public int end;
            public Dist(int start, int end)
            {
                this.start = start;
                this.end = end;
            }
        }

        private int n;
        private List<Dist> list;

        private int result;
        public int Result { get { return result; } }
        
        public OfficeRoom(int n)
        {
            this.n = n;
            list = new List<Dist>(n);
            Input();
        }

        public void Input()
        {
            for(int i =0; i<n; i++)
            {
                string[] temp = Console.ReadLine().Split(' ');
                Dist dist;
                dist.start = int.Parse(temp[0]);
                dist.end = int.Parse(temp[1]);
                list.Add(dist);
            }

            list.Sort((Dist a, Dist b) => a.start.CompareTo(b.start) == 0 ? b.end.CompareTo(a.end) : a.start.CompareTo(b.start));

            Dist cur;
            cur.start = 0;
            cur.end = 0;
            for(int i=0; i<list.Count; i++)
            {
                if(cur.end <= list[i].start)
                {
                    cur.end = list[i].end;
                }
                else if(cur.end > list[i].end)
                {
                    cur = list[i];
                }
            }
        }

        static void Main(string[] argc)
        {
            int n = int.Parse(Console.ReadLine());
            OfficeRoom oRoom = new OfficeRoom(n);
            Console.WriteLine(oRoom.Result);
        }
    }
}
