using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

// 아래와 같이 추가와 삭제가 순서대로 진행될 경우 큐의 출력 순서를 적어주자.(코딩없이)
//  추가(1, 2, 3, 4, 5),                                                      모두 꺼내기: 1 2 3 4 5
//  추가(1, 2, 3), 꺼내기(2번), 추가(4, 5, 6), 꺼내기(1번), 추가(7),          모두 꺼내기: 4 5 6 7
//  추가(3, 2, 1), 꺼내기(1번), 추가(6, 5, 4), 꺼내기(3번), 추가(9, 8, 7)     모두 꺼내기: 5 4 9 8 7
//  추가(1, 3, 5), 꺼내기(1번), 추가(2, 4, 8), 꺼내기(2번), 추가(1, 3),       모두 꺼내기: 2 4 8 1 3
//  추가(3, 2, 1), 꺼내기(2번), 추가(3, 2, 1), 꺼내기(2번), 추가(3, 2, 1),    모두 꺼내기: 2 1 3 2 1

namespace _04._Queue
{

//    아래 문제를 보고 스택 또는 큐를 써서 해결하시오.

//      <작업 프로세스>
//       각 작업이 몇시간이 걸리는 작업인지 포함한 배열이 있으며,
//       하루에 8시간씩 일할 수 있는 회사가 있음.
//       남는시간없이 주어진 일을 계속한다고 했을때.
//       각각의 작업이 끝나는 날짜를 결과 배열로 출력

       // int[] ProcessJob(int[] jobList) { }

//    예시 : [4, 4, 12, 10, 2, 10]
//    결과 : [1, 1, 3, 4, 4, 6]
//    해석 : 1일차에 0, 1 클리어 : 0번째 작업의 4/4 + 1번째 작업의 4/4 완료,
//          2일차에                    : 2번째 작업의 8/12 완료
//          3일차에 2 클리어     : 2번째 작업의 4/12 완료 + 3번째 작업의 4/10 완료
//          4일차에 3, 4 클리어 : 3번째 작업의 6/10 완료 + 4번째 작업의 2/2 완료
//          5일차에                    : 5번째 작업의 8/10 완료
//          6일차에 5 클리어     : 5번째 작업의 2/10 완료
    internal class Training
    {
        public Queue<int> q;
        public List<int> proccessList;
        private bool loopGame;

        public const int MyWorkTime = 8;

        public Training()
        {
            q = new Queue<int>();
            proccessList = new List<int>();
            loopGame = true;
        }

        public void PrintProccessResult()
        {
            Console.Write("\n작업분 : ");
            Console.ForegroundColor = ConsoleColor.Green;
            foreach(int i in proccessList)
            {
                Console.Write($"{i} ");
            }
            Console.ResetColor();
            Console.Write("\n종료일시 :");
            Console.ForegroundColor = ConsoleColor.Magenta;
            // 출력용 리스트
            List<int> printList = new List<int>();
            // 큐 사용 문제해결
            for(int i =0; i<proccessList.Count; i++)
            {
                q.Enqueue(proccessList[i]); 
            }

            int temp = MyWorkTime;
            int dayCount = 1;
            while(q.Count > 0)
            {
                if(q.Peek() <= temp)
                {
                    temp -= q.Peek();
                    q.Dequeue();
                    Console.Write($"{dayCount} ");
                }
                else
                {
                    temp += MyWorkTime;
                    dayCount++;
                }
            }
            Console.ResetColor();
        }

        public bool RenderAndInput()
        {
            int days = -1, dayCounting = 1;
            do
            {
                Console.Write("총 일수를 작성하세요(종료:0) : ");
                int.TryParse(Console.ReadLine(), out days);
                if (days == 0) return false;
            } while (days == -1);

            while(days-- > 0)
            {
                int workTime = -1;
                do
                {
                    Console.Write($"{dayCounting}일차 작업시간:");
                    int.TryParse(Console.ReadLine(), out workTime);
                } while (workTime == -1);
                proccessList.Add(workTime);
                dayCounting++;
            }
            return true;
        }
        public void RenderAndDataClear()
        {
            q.Clear();
            proccessList.Clear();  

            Thread.Sleep(4000);
            Console.Clear();
        }
        public void LoopGame()
        {
            while(loopGame)
            {
                loopGame = RenderAndInput();
                if (!loopGame) return;
                PrintProccessResult();
                RenderAndDataClear();
            }
        }

        //static void Main(string[] argc)
        //{
        //    Training training = new Training();
        //    training.LoopGame();
        //}
    }
    class StackQueueTest
    {
        public enum ActionType
        {
            NULL,
            PUSH,
            POP,
            POPALL,
            INIT
        }

        private Queue<int> myQ;
        private Stack<int> myStack;

        private List<int> popQList;
        private List<int> popStackList;

        private bool loop;

        public StackQueueTest()
        {
            myQ = new Queue<int>();
            myStack = new Stack<int>();

            popQList = new List<int>();
            popStackList = new List<int>();

            loop = true;
        }

        public void PushData()
        {
            Console.WriteLine("추가할 데이터를 입력하세요 (공백으로 구분)");
            string input = Console.ReadLine();

            StringBuilder sb = new StringBuilder();
            for(int i=0; i<input.Length; i++)
            {
                if (input[i] == ' ' || i == input.Length-1)
                {
                    if(i == input.Length - 1) sb.Append(input[i]);
                    
                    int num = 0;
                    int.TryParse(sb.ToString(), out num);
                    myQ.Enqueue(num);
                    myStack.Push(num);
                    sb.Clear();
                }
                else
                {
                    sb.Append(input[i]);
                }
            }
        }
        public void PopData(ActionType actionType)
        {
            if(actionType == ActionType.POPALL)
            {
                while (myQ.Count > 0)   // 스택과 큐는 사이즈가 동일
                {
                    popQList.Add(myQ.Peek());
                    myQ.Dequeue();

                    popStackList.Add(myStack.Peek());
                    myStack.Pop();
                }
                return;
            }


            int count = -1;
            do
            {
                Console.Write("꺼낼 요소의 개수를 입력하세요:");
                int.TryParse(Console.ReadLine(), out count);

                if(count != -1)
                {
                    if(myQ.Count < count || myStack.Count < count)
                    {
                        count = -1;
                    }    
                }
            } while (count == -1);

            for(int i =0; i<count; i++)
            {
                popQList.Add(myQ.Peek());
                myQ.Dequeue();

                popStackList.Add(myStack.Peek());
                myStack.Pop();
            }
        }
        public void InitData()
        {
            myQ.Clear();
            myStack.Clear();
            popQList.Clear();
            popStackList.Clear();
        }
        public void Rendering()
        {
            Console.WriteLine("<삽입한 원소>");
            Console.Write("큐 : ");
            Console.ForegroundColor = ConsoleColor.Green;
            Queue<int> tempQ = new Queue<int>(myQ);
            while(tempQ.Count > 0)
            {
                Console.Write($"{tempQ.Peek()} ");
                tempQ.Dequeue();
            }
            Console.ResetColor();

            Console.Write("\n스택 : ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Stack<int> tempStack = new Stack<int>(myStack);
            while (tempStack.Count > 0)
            {
                Console.Write($"{tempStack.Peek()} ");
                tempStack.Pop();
            }
            Console.ResetColor();

            Console.WriteLine("\n\n<꺼낸 원소>");
            Console.Write("큐 : ");
            Console.ForegroundColor = ConsoleColor.Green;
            foreach(int i in popQList)
            {
                Console.Write($"{i} ");
            }
            Console.ResetColor();

            Console.Write("\n스택 : ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (int i in popStackList)
            {
                Console.Write($"{i} ");
            }
            Console.ResetColor();
            Console.WriteLine("\n");
        }
        public void RenderClear()
        {
            Thread.Sleep(800);
            Console.Clear();
        }
        public void Input()
        {
            Console.Write("|  추가:1  |  꺼내기:2  |  모두 꺼내기:3  |  데이터 초기화:4  | 종료:0  | : ");
            ActionType actionType = ActionType.NULL;
            do
            {
                ConsoleKeyInfo inputKey = Console.ReadKey();
                switch (inputKey.Key)
                {
                    case ConsoleKey.D1:
                        actionType = ActionType.PUSH;
                        break;
                    case ConsoleKey.D2:
                        actionType = ActionType.POP;
                        break;
                    case ConsoleKey.D3:
                        actionType = ActionType.POPALL;
                        break;
                    case ConsoleKey.D4:
                        actionType = ActionType.INIT;
                        break;
                    case ConsoleKey.D0:
                        return;
                    default:
                        actionType = ActionType.NULL;
                        break;
                }
            } while (actionType == ActionType.NULL);
            Console.WriteLine();

            switch (actionType)
            {
                case ActionType.PUSH:
                    PushData();
                    break;
                case ActionType.POP:
                case ActionType.POPALL:
                    PopData(actionType);
                    break;
                case ActionType.INIT:
                    InitData();
                    break;
            }
        }

        public void Loop()
        {
            while(loop)
            {
                Rendering();
                Input();
                RenderClear();
            }
        }

        static void Main(string[] argc)
        {
            StackQueueTest sqtest = new StackQueueTest();
            sqtest.Loop();
        }
    }
}
