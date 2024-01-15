using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


// <문제 1>

// 아래와 같이 추가와 삭제가 순서대로 진행될 경우 스택의 출력 순서를 적어주자.(코딩없이)
//  추가(1, 2, 3, 4, 5),                                                      모두 꺼내기: 5 4 3 2 1
//  추가(1, 2, 3), 꺼내기(2번), 추가(4, 5, 6), 꺼내기(1번), 추가(7),          모두 꺼내기: 7 5 4 1
//  추가(3, 2, 1), 꺼내기(1번), 추가(6, 5, 4), 꺼내기(3번), 추가(9, 8, 7)     모두 꺼내기: 7 8 9 2 3
//  추가(1, 3, 5), 꺼내기(1번), 추가(2, 4, 8), 꺼내기(2번), 추가(1, 3),       모두 꺼내기: 3 1 2 3 1
//  추가(3, 2, 1), 꺼내기(2번), 추가(3, 2, 1), 꺼내기(2번), 추가(3, 2, 1),    모두 꺼내기: 1 2 3 3 3

namespace _03._Stack
{
    // <문제 2>

    //  <괄호 검사기를 구현하자>
    //  괄호 : 괄호가 바르게 짝지어졌다는 것은 열렸으면 짝지어서 닫혀야 한다는 뜻

    //  텍스트를 입력으로 받아서 괄호가 유효한지 판단하는 함수
    //  bool IsOk(string text) { }
    //  검사할 괄호 : [], { }, ()
    //  예시: () 완성, (() 미완성, [) 미완성, [[(){}]] 완성
    internal class Training
    {
        private bool gameLoop;
        public Training()
        {
            gameLoop = true;
        }

        bool CheckBracket(string str)
        {
            Stack<char> stack = new Stack<char>();
            for(int i =0; i<str.Length; i++)
            {
                if (str[i] == '(' || str[i] == '[' || str[i] == '{') 
                { 
                    // 열린 괄호는 무조건 추가
                    stack.Push(str[i]); 
                }
                else
                {
                    // 열린 괄호 이전에 닫힌 괄호가 먼저 나온경우
                    if (stack.Count == 0) return false;

                    // 대칭되는 괄호일 경우 pop
                    switch(str[i])
                    {
                        case ')':
                            if (stack.Peek() == '(') stack.Pop();
                            else return false;
                            break;
                        case ']':
                            if (stack.Peek() == '[') stack.Pop();
                            else return false;
                            break;
                        case '}':
                            if (stack.Peek() == '{') stack.Pop();
                            else return false;
                            break;
                        default:
                            return false;
                    }
                }
            }
            if (stack.Count > 0) return false;  // 열린 괄호로 끝난 경우
            return true;
        }

        public void GameLoop()
        {
            while(gameLoop)
            {
                Render();
                string temp = Input();
                if (temp.Length < 1 || temp[0] == '0') return;
                PrintResult(CheckBracket(temp));
                RenderClear();
            }
        }
        public void Render()
        {
            Console.WriteLine("괄호를 입력하세요(종료:0) :");
        }
        public void RenderClear()
        {
            Thread.Sleep(1000);
            Console.Clear();
        }
        private void PrintResult(bool condition)
        {
            if (condition)
            {
                Console.WriteLine("올바른 괄호입니다.");
            }
            else
            {
                Console.WriteLine("틀린 괄호입니다.");
            }
        }
        public string Input()
        {
            string temp = Console.ReadLine();
            return ReturnBracket(temp);
        }
        private string ReturnBracket(string str)
        {
            StringBuilder strbd = new StringBuilder();

            for(int i =0; i<str.Length; i++)
            {
                if (str[i] == '{' || str[i] == '}' || str[i] == '(' || str[i] == ')' || str[i] == '[' || str[i] == ']')
                    strbd.Append(str[i]);   
            }
            return strbd.ToString();    
        }
        static void Main(string[] argc)
        {
            Training training = new Training();
            training.GameLoop();
        }
    }
}
