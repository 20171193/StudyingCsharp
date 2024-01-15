using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace _02._LinkedList
{
    //< 실습 >

    //사용자의 입력으로 숫자를 입력 받아서 (마이너스도 허용)
    //음수는 앞에 추가하고, 양수는 뒤에 추가하여
    //음수&양수를 반으로 나누는 연결리스트 구현
    //입력 받을 때마다 처음부터 끝까지 출력 진행

    //////////////////////////////////////////////////////////////
    internal class Training
    {
        class LinkedListTest
        {
            public LinkedList<float> list;
            private bool isLoop;

            public LinkedListTest()
            {
                list = new LinkedList<float>();
                isLoop = true;
            }
            public void PrintMenual()
            {
                Console.Write("숫자를 입력하세요:");
            }
            public bool InputLine()
            {
                float num = 0f;
                try
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    float.TryParse(Console.ReadLine(), out num);
                    if (num < 0) list.AddFirst(num);
                    else list.AddLast(num);
                    Console.WriteLine();
                    Console.ResetColor();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            public void PrintList()
            {
                foreach(float i in list)
                {
                    if (i < 0)
                        Console.ForegroundColor = ConsoleColor.Green;
                    else
                        Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($"{i} ");
                }
                Console.WriteLine();
                Console.ResetColor();
            }
            public void ProgramLoop()
            {
                while(isLoop)
                {
                    PrintMenual();
                    isLoop = InputLine();
                    PrintList();
                }
            }
        }



        //요세푸스 순열 문제

        // 요세푸스 문제는 다음과 같다.
        // 1번부터 N번까지 N명의 사람이 원을 이루면서 앉아있고, 양의 정수 K(≤ N)가 주어진다. 
        // 이제 순서대로 K번째 사람을 제거한다. 한 사람이 제거되면 남은 사람들로 이루어진 원을 따라 이 과정을 계속해 나간다. 
        // 이 과정은 N명의 사람이 모두 제거될 때까지 계속된다. 원에서 사람들이 제거되는 순서를 (N, K)-요세푸스 순열이라고 한다. 
        // 예를 들어 (7, 3)-요세푸스 순열은 <3, 6, 2, 7, 5, 1, 4>이다.
        // N과 K가 주어지면 (N, K)-요세푸스 순열을 구하는 프로그램을 작성하시오.

        public class Josephus
        {
            private LinkedList<int> list;
            public Josephus()
            {
                list = new LinkedList<int>();   
            }

            public List<int> RetJosephusPermutation(int n, int k)
            {
                // 리스트 활용
                for(int i=1; i<=n; i++)
                {
                    list.AddLast(i);
                }

                List<int> ret = new List<int>();

                LinkedListNode<int> curNode = list.First;
                int count = 1;
                while (ret.Count < list.Count)
                {
                    if (count == 3) 
                    {
                        count = 0;
                        ret.Add(curNode.Value);
                    }

                    curNode = curNode.Next;
                    if (curNode == null) curNode = list.First;
                    count++;
                }
                return ret;
            }
        }


        //A+) 풍선터트리기 문제
        //  - 1번부터 N번까지 N개의 풍선이 원형으로 놓여 있고.
        //  - i번 풍선의 오른쪽에는 i+1번 풍선이 있고,왼쪽에는 i-1번 풍선이 있다.
        //  - 단, 1번 풍선의 왼쪽에 N번 풍선이 있고, N번 풍선의 오른쪽에 1번 풍선이 있다.
        //  - 각 풍선 안에는 종이가 하나 들어있고, 종이에는 -N보다 크거나 같고, N보다 작거나 같은 정수가 하나 적혀있다.
        //
        // - 이 풍선들을 다음과 같은 규칙으로 터뜨린다.
        //  1. 우선, 제일 처음에는 1번 풍선을 터뜨린다.
        //  2. 다음에는 풍선 안에 있는 종이를 꺼내어 그 종이에 적혀있는 값만큼 이동하여 다음 풍선을 터뜨린다.
        //  3. 양수가 적혀 있을 경우에는 오른쪽으로, 음수가 적혀 있을 때는 왼쪽으로 이동한다.
        //  4. 이동할 때에는 이미 터진 풍선은 빼고 이동한다.

        //예를 들어 다섯 개의 풍선 안에 차례로 3, 2, 1, -3, -1이 적혀 있었다고 하자.
        //이 경우 3이 적혀 있는 1번 풍선, -3이 적혀 있는 4번 풍선, -1이 적혀 있는 5번 풍선, 1이 적혀 있는 3번 풍선, 2가 적혀 있는 2번 풍선의 순서대로 터지게 된다.


        public struct Balloon
        {
            public int index;
            public int num;
            public Balloon(int index, int num)
            {
                this.index = index;
                this.num = num; 
            }
        }
        public class BombBalloon
        {
            private bool loop;
            private LinkedList<Balloon> list;

            public BombBalloon() 
            {
                loop = true;
                list = new LinkedList<Balloon>();
            }

            public void SettingBalloon(int n)
            {
                for(int i=0; i<n; i++)
                {
                    Console.Write($"{i + 1}번:");
                    int temp = 0;
                    int.TryParse(Console.ReadLine(), out temp);
                    Balloon balloon = new Balloon(i+1,temp);
                    list.AddLast(balloon);
                }
            }

            public void RenderMainMenual()
            {
                Console.Write("풍선 개수를 입력하세요:");
            }

            public int InputLine()
            {
                int num = 0;
                int.TryParse(Console.ReadLine(), out num);
                return num;
            }

            public void GameLoop()
            {
                RenderMainMenual();
                SettingBalloon(InputLine());

                LinkedListNode<Balloon> node = list.First;
                Console.WriteLine($"{node.Value.index}번 풍선 pop : {node.Value.num}");
                int count = node.Value.num;
                node = node.Next;
                list.RemoveFirst();

                while (list.Count > 0)
                {
                    int temp = Math.Abs(count);
                    while(temp > 0)
                    {
                        if (count == 0) break;
                        else if(count < 0)
                        {
                            node = node.Previous;
                            if (node == null) node = list.Last;
                        }
                        else
                        {
                            node = node.Next;
                            if (node == null) node = list.First;
                        }
                        temp--;
                    }
                    count = node.Value.num;
                    Console.WriteLine($"{node.Value.index}번 풍선 pop : {count}");
                    LinkedListNode<Balloon> next;
                    if (count < 0)
                    {
                        next = node.Previous;
                        if (next == null) next = list.Last;
                        count += 1;
                    }
                    else
                    {
                        next = node.Next;
                        if (next == null) next = list.First;
                        count -= 1;
                    }
                    list.Remove(node);
                    node = next;
                }
            }
        }

        static void Main(string[] argc)
        {
            // Test 1~3
            //LinkedListTest llt = new LinkedListTest();
            //llt.ProgramLoop();

            // Test 4
            //Josephus jose = new Josephus();
            //Console.ForegroundColor = ConsoleColor.Yellow;
            //foreach(int i in jose.RetJosephusPermutation(7,3))
            //{
            //    Console.Write($"{i} ");
            //}
            //Console.ResetColor();

            // Test A++
            BombBalloon bb = new BombBalloon();
            bb.GameLoop();
        }
    }
}
