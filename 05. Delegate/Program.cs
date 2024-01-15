/****************************************************************
 * 대리자 (Delegate)
 * 
 * 특정 매개 변수 목록 및 반환 형식이 있는 함수에 대한 참조
 * 대리자 인스턴스를 통해 함수를 호출 할 수 있음
 * 반환형과 매개변수의 자료형이 일치하는 함수만 사용가능하다.
 * ! 함수를 담는 변수라고 생각할 수 있다 !
 ****************************************************************/

using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

namespace _05._Delegate
{
    /// <summary>
    /// 델리게이트
    /// </summary>
    class Program
    {
        // <델리게이트 정의>
        // delegate 반환형 델리게이트이름(매개변수들);
        public delegate float DelegateMethod1(float x, float y);
        public delegate void DelegateMethod2(string str);


        public float Plus(float left, float right) { return left + right; }
        public float Minus(float left, float right) { return left - right; }
        public float Multi(float left, float right) { return left * right; }
        public float Divide(float left, float right) { return left / right; }
        public void Message(string message) { Console.WriteLine(message); }


        // <델리게이트 사용>
        // 반환형과 매개변수가 일치하는 함수를 델리게이트 변수에 할당
        // 델리게이트 변수에 참조한 함수를 대리자를 통해 호출 가능
        void Main1()
        {
            DelegateMethod1 delegate1 = new DelegateMethod1(Plus);  // 델리게이트 인스턴스 생성
            DelegateMethod2 delegate2 = Message;                    // 간략한 문법의 델리게이트 인스턴스 생성

            delegate1.Invoke(20, 10);   // Plus(20, 10);            // Invoke를 통해 참조되어 있는 함수를 호출
            delegate2("메세지");        // Message("메세지");       // 간략한 문법의 델리게이트 함수 호출

            delegate1 = Plus;
            Console.WriteLine(delegate1(20, 10));       // output : 30
            delegate1 = Minus;
            Console.WriteLine(delegate1(20, 10));       // output : 10
            delegate1 = Multi;
            Console.WriteLine(delegate1(20, 10));       // output : 200
            delegate1 = Divide;
            Console.WriteLine(delegate1(20, 10));       // output : 2

            // delegate2 = Plus;        // error : 반환형과 매개변수가 일치하지 않은 함수는 참조 불가
        }

        static void Main(string[] args)
        {

        }
    }

    /// 델리게이트 일반화
    #region 델리게이트 일반화
    /// <summary>
    /// Func 델리게이트 (계산기)
    /// </summary>
    public class Calculator
    {
        Func<double, double, double> fd;
        double left;
        double right;
        public double Plus(double left, double right) { return left + right; }
        public double Minus(double left, double right) { return left - right; }
        public double Multi(double left, double right) { return left * right; }
        public double Divide(double left, double right) { return left / right; }
        public double Mod(double left, double right) { return left % right; }
        public void SetCommand(double left, char oper, double right)
        {
            this.left = left;
            this.right = right;
            // 계산금지
            switch (oper)
            {
                case '+':
                    fd = Plus;
                    break;
                case '-':
                    fd = Minus;
                    break;
                case '*':
                    fd = Multi;
                    break;
                case '/':
                    fd = Divide;
                    break;
                case '%':
                    fd = Mod;
                    break;
            }
        }
        public double Equal()
        {
            // 조건문 쓰기 금지
            return fd(left, right);
        }
    }
    public class TestCalculator
    {
        public static void Main()
        {
            Calculator cal = new Calculator();
            cal.SetCommand(2.5, '+', 3.2);
            double result = cal.Equal();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(result);
            Console.ResetColor();
        }
    }

    /// <summary>
    /// Action 델리게이트 (콜백함수)
    /// </summary>
    class CallBack
    {
        // 3. 콜백함수를 이용하여 플레이어를 조작하는 UI 버튼제작 (점프, 대시)
        abstract class Button
        {
            protected Action action;    //
            public virtual void OnClick()
            {
                if (action != null)
                {
                    action();
                }
            }
        }

        class DashButton : Button
        {
            public DashButton(Action action)
            {
                this.action = action;
            }
            public override void OnClick()
            {
                Console.WriteLine("대쉬버튼 클릭");
                base.OnClick();
            }
        }
        class JumpButton : Button
        {
            public JumpButton(Action action)
            {
                this.action = action;
            }
            public override void OnClick()
            {
                Console.WriteLine("점프버튼 클릭");
                base.OnClick();
            }
        }

        class Player
        {
            public void Dash()
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("플레이어 대시");
                Console.ResetColor();
            }
            public void Jump()
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("플레이어 점프");
                Console.ResetColor();
            }
        }

        class TestButton
        {
            static void Main(string[] args)
            {
                Player player = new Player();
                Button dashButton = new DashButton(player.Dash);    // 버튼 객체를 생성함과 동시에
                Button jumpButton = new JumpButton(player.Jump);    // Action 델리게이트에 할당

                dashButton.OnClick();   // OnClick 함수 호출 -> player.Dash실행 
                jumpButton.OnClick();   // OnClick 함수 호출 -> player.Jump실행
            }
        }

    }

    /// <summary>
    /// Predicate 델리게이트 (문자열 띄어쓰기 확인)
    /// </summary>
    class CheckSpace
    {
        static bool IsSentence(string str) 
        { 
            return str.Contains(' '); 
        }

        static void Main3(string[] argc)
        {
            Predicate<string> predicate;
            predicate = IsSentence; // bool형 함수를 predicate에 할당
            Console.WriteLine(predicate("짱 구"));
            Console.WriteLine(predicate("짱구"));
        }
    }

#endregion

    /// <summary>
    /// 지정자
    /// </summary>
    class Specifier
    {
        public class Item
        {
            public string name;
            public int level;
            public float weight;

            public Item(string name, int level, float weight)
            {
                this.name = name;
                this.level = level;
                this.weight = weight;
            }
        }

        void Main()
        {
            Item[] inventory = new Item[5];

            inventory[0] = new Item("포션", 1, 3.2f);
            inventory[1] = new Item("갑옷", 2, 1.2f);
            inventory[2] = new Item("방패", 3, 4.5f);
            inventory[3] = new Item("검", 7, 8.8f);
            inventory[4] = new Item("폭탄", 6, 12.6f);



            // 이름으로 찾기
            int findIndex = -1;
            string findName = "방패";
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i].name == findName)
                {
                    findIndex = i;
                    break;
                }
            }

            // 레벨로 찾기
            int findLevel = 6;
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i].level == findLevel)
                {
                    findLevel = i;
                    break;
                }
            }

            // 무게로 찾기
            float findWeight = 12.6f;
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i].weight == findWeight)
                {
                    findWeight = i;
                    break;
                }
            }

            // 찾아야할 것이 많아지면 위의 코드를 계속 작성해야함.
            int index1 = FindIndex(inventory, FindByName);                        // 방법1 : 함수생성
            int index1_ = FindIndex(inventory, value => value.name == "방패");    // 방법2 : 람다식

            int index2 = FindIndex(inventory, FindWeight6);
            int index2_ = FindIndex(inventory, value => value.weight == 6);
        }

        public static bool FindByName(Item item)
        {
            return item.name == "방패";
        }
        public static bool FindWeight6(Item item)
        {
            return item.weight == 6;
        }

        public static int FindIndex(Item[] inventory, Predicate<Item> predicate)
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                if (predicate(inventory[i]))
                {
                    return i;
                }
            }
            // 못찾은 경우
            return -1;
        }
    }

    /// <summary>
    /// 람다식
    /// </summary>
    class LamDa
    {
        // 잠깐 쓰고 버리는 함수들은 멤버함수로 만드는 것이 번거로움.
        bool Less1(int value)
        {
            return value < 1;
        }
        bool Less2(int value)
        {
            return value < 2;
        }
        bool Less3(int value)
        {
            return value < 3;
        }


        void Main()
        {
            Action<string> action;

            // <함수를 통한 할당>
            // 클래스에 정의된 함수를 직접 할당
            // 클래스의 멤버함수로 연결하기 위한 기능이 있을 경우 적합
            action = Message;


            // <무명메서드>
            // 함수를 통한 연결은 함수가 직접적으로 선언되어 있어야 사용 가능
            // 할당하기 위한 함수가 간단하고 **자주 사용될수록 비효율적
            // 간단한 표현식을 통해 함수를 즉시 작성하여 전달하는 방법
            // 전달하기 위한 함수가 간단하고 일회성으로 사용될 경우에 적합
            action = delegate (string str) { Console.WriteLine(str); };


            // <람다식>
            // 무명메서드의 간단한 표현식
            action = (str) => { Console.WriteLine(str); };
            action = str => Console.WriteLine(str);

        }

        void Message(string str) { Console.WriteLine(str); }
    }

    /// <summary>
    /// 델리게이트 체인
    /// </summary>
    class Chain
    {
        /***************************************************
        * 델리게이트 체인
        * 
        * 델리게이트 변수에 여러개의 함수를 참조하는 방법
        ***************************************************/

        // <델리게이트 체인>
        // 하나의 델리게이트 변수에 여러 개의 함수를 할당하는 것이 가능
        // +=, -= 연산자를 통해 할당을 추가하고 제거할 수 있음
        // = 연산자를 통해 할당할 경우 이전의 다른 함수들을 할당한 상황이 사라짐

        void Main()
        {
            Action action;
            action = Func2;     // 델리게이트 인스턴스를 Func2 로 초기화
            action += Func1;    // 델리게이트 인스턴스에 Func1 추가 참조
            action += Func3;    // 델리게이트 인스턴스에 Func3 추가 참조
            action();           // Func2, Func1, Func3 이 호출됨

            action -= Func1;    // 델리게이트 인스턴스에 Func1 참조 제거
            if (action != null) // 델리게이트 인스턴스에서 참조를 제거할 경우 참조하고 있는 함수가 없는 경우를 조심
                action();       // Func2, Func3 이 호출됨

            action += Func2;    // 같은 함수를 여러번 참조한 경우 여러번 호출됨
            action += Func2;
            action();           // Func2 3회, Func3 1회 호출됨

            action -= Func1;    // 델리게이트 인스턴스에 참조되지 않은 함수를 제거하는 경우 해당 작업이 무시됨

            action = Func1;     // 델리게이트 인스턴스에 = 을 통해 할당할 경우 이전의 참조된 상황이 사라짐
            action();           // Func1 이 호출됨
        }

        void Func1() { Console.WriteLine("Func1"); }
        void Func2() { Console.WriteLine("Func2"); }
        void Func3() { Console.WriteLine("Func3"); }

    }
}