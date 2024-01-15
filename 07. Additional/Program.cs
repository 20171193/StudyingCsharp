using System.Runtime.CompilerServices;
using System.Text;
using static _07._Additional.Partial;

namespace _07._Additional
{
    #region 1. C# 제공 메서드

    #region string 메서드
    class StringMethod
    {
        void Main()
        {
            string str = "abc 123";
            string upperStr = str.ToUpper();            // "ABC 123"
            string lowerStr = str.ToLower();            // "abc 123"
            string[] partitionStr = str.Split(' ');     // {"abc", "123"} 
            string replaceStr = str.Replace('a', 'z');  // "zbc 123" 
        }
    }
    #endregion

    #region Array 메서드
    class ArrayMethod
    {
        void Main1()
        {
            int[] arr = { 1, 2, 3, 4, 5 };
            int maxNum = arr.Max();
            int minNum = arr.Min();
            double average = arr.Average();

            Array.Sort(arr);                            // 기본 : 오름차순 정렬
            Array.Sort(arr, (a, b) => a > b ? 1 : -1);  // 내림차순 정렬
        }
    }
    #endregion

    #region Int 메서드
    class IntMethod
    {
        void Main2()
        {
            int num;
            num = int.Parse("123");
            // num = int.Parse("$@%#@$");   Error : TryParse 사용하여 형변환 체크하기.

            num = int.MaxValue; // 2147....
            num = int.MinValue; // -2147....
            Console.WriteLine(num);
        }
    }
    #endregion

    #endregion

    #region 2. 프로퍼티 (Property)

    class Property
    {
        // * 필드 내의 모든 변수에 대해 직접 요청하는 것이 아닌, "읽는 행위", "쓰는 행위"를 요청.
        // * 상수 등을 제외하고는 필드 내부 변수를 private으로 선언.
        // * 읽기, 쓰기 행위를 public으로 사용해주자.

        public class Player
        {
            private int hp;                      // 1. private 변수
            public int Hp { get { return hp; } } // 2. 읽기 전용 (상수처럼 사용가능)
            public int Hp2 { get { return hp; } set { hp = value; } } // 3. 읽기/쓰기 전용

            // 응용문
            private int MaxHp = 100;
            public event Action<int> OnChangeHp;
            public int Hp3  // 4. 쓰기 조건세팅
            {
                get
                {
                    return hp;
                }
                set
                {
                    if (value >= MaxHp) hp = 100;   // 최대 체력으로 세팅 제한
                    else hp = value;

                    OnChangeHp(hp); // HP 변경 이벤트 실행 (UI, 사운드 등등)
                }
            }

        }
        void Main()
        {
            Player player = new Player();
            // player.Hp = 50;  // Error : 읽기 전용임.
            player.Hp2 = 80;    // hp = 80;
            player.Hp3 = 500;   // hp = 100;    UI 변경.

        }
    }
    #endregion

    #region 3. 분리 (Partial)

    class Partial
    {

        // * partial class
        // * 같은 클래스를 구분해서 작업할 수 있도록.

        // 전투 담당자 소스
        public partial class Player
        {
            private int hp;
            public void Attack() { }
            public void Defence() { }
        }
        // 아이템 담당자 소스
        public partial class Player
        {
            public int itemSlotCapacity;
            public void GetItem() { }
            public void UseItem() { }
        }

        // * partial method
        // * 선언부와 구현부를 분리하여 구현.
        // * 구현부를 숨길 수 있는 기능.
        // * 함수의 기능을 보여주고 내용을 감춤.
        public partial class Monster
        {
            public partial void Attack();
        }
        public partial class Monster
        {
            public partial void Attack()
            {
                // method body
            }
        }
    }

    #endregion

    #region 4. 확장 메서드 (Extension Method)

    static class ExtensionMethod
    {
        // * 자주 쓰는 기능들을 확장 메서드로 생성.

        // 단어 수를 알고싶음. 
        // 단어 수를 제공해주는 메서드 생성
        // static 클래스 내부에 static 메서드 생성. this로 매개변수 받기.
        public static int WordCount(this string str)
        {
            return str.Split(' ').Length;
        }
        //public static void Main()
        //{
        //    string str = "abc def lf";
        //    int count1 = WordCount(str);    // 1. 기본 호출
        //    int count2 = str.WordCount();   // 2. 확장 메서드 호출
        //}
    }
    #endregion

    #region 5. 연산자 재정의 (Operator OverLoading)
    // 상황
    class Test
    {
        public struct Vector2
        {
            public int x;
            public int y;
            public Vector2(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            // ------------------------------------------------------------------------------
            // 연산자 재정의
            public static Vector2 operator +(Vector2 a, Vector2 b)
            {
                int x = a.x + b.x;
                int y = a.y + b.y;
                return new Vector2(x, y);
            }
            public static Vector2 operator *(Vector2 a, int value)
            {
                int x = a.x + value;
                int y = a.y * value;
                return new Vector2(x, y);
            }
        }
        void Main()
        {
            Vector2 aVec = new Vector2(2, 1);
            Vector2 bVec = new Vector2(5, 3);

            // Vector2 resultVec = aVec + bVec;  Error : aVec + bVec 연산을 실행할 수 없음.
            Vector2 resultVec = new Vector2(aVec.x + bVec.x, bVec.y + bVec.y);
            // Vector 연산을 계속 반복해야 한다면 매우 번거로움.

            // -----------------------------------------------------------------------------
            // 연산자 재정의 
            resultVec = aVec + bVec;    // 직관적으로 사용 가능.
            resultVec = aVec * 3;
        }
    }

    #endregion

    #region 6. Parameter
    class Parameter
    {
        // * Named Parameter
        void Profile(int id, string name, string phone) { }

        // * Optional Parameter
        // * 매개변수의 초기값을 설정할 수 있음.
        // * 마지막에 위치한 매개변수부터 설정해야함.

        void AddStudent(string name, string home = "서울", int age = 5) { }
        // void AddParent(string name, string home = "서울", int age) { }     Error : 초기값이 있는 매개변수는 뒤부터 작성


        void Main()
        {
            // * Named Parameter
            Profile(10, "호날두", "010-7777-7777");
            Profile(name: "호날두", phone: "010-7777-7777", id: 10);   // Named Parameter

            // * Optional Parameter
            AddStudent("짱구", "서울", 5);
            AddStudent("철수", "서울", 5);
            AddStudent("맹구");            // Optional Parameter
            AddStudent("유리", "구리");
            AddStudent("호날두", age: 35);  // home = "서울"
        }


        // <Params Parameter>
        // 매개변수의 갯수가 정해지지 않은 경우, 매개변수의 갯수를 유동적으로 사용하는 방법
        int Sum(params int[] values)
        {
            int sum = 0;
            for (int i = 0; i < values.Length; i++) sum += values[i];
            return sum;
        }

        void Main3()
        {
            Sum(1, 3, 5, 7, 9);
            Sum(3, 5, 7);
            Sum();
        }


        // <in Parameter>
        // 매개변수를 입력전용으로 설정
        // 함수의 처음부터 끝까지 동일한 값을 보장하게 됨 (원본 값을 변경하고 싶지 않을 때)
        int Plus(in int left, in int right)
        {
            // left = 20;      // error : 입력전용 매개변수는 변경 불가
            return left + right;
        }
        void Main4()
        {
            int result = Plus(1, 3);
            Console.WriteLine($"{result}");     // output : 4
        }

        // <out Parameter>
        // 매개변수를 출력전용으로 설정
        // 함수의 반환값 외에 추가적인 출력이 필요할 경우 사용
        void Divide(int left, int right, out int quotient, out int remainder)
        {
            quotient = left / right;
            remainder = left % right;

            // 함수의 종료전까지 out 매개변수에 값이 할당 안되는 경우 오류
        }


        void Main5()
        {
            int quotient;
            Divide(5, 3, out quotient, out int remainder);
            Console.WriteLine($"{quotient}, {remainder}");  // output : 1, 2
        }


        // <ref Parameter>
        // 매개변수를 원본참조로 전달
        // 매개변수가 값형식인 경우에도 함수를 통해 원본값을 변경하고 싶을 경우 사용
        void Swap(ref int left, ref int right)
        {
            int temp = left;
            left = right;
            right = temp;
        }

        void Main6()
        {
            int left = 10;
            int right = 20;
            Swap(ref left, ref right);
            Console.WriteLine($"{left}, {right}");      // output : 20, 10
        }
    }

    #endregion

    #region 7. Indexer
    internal class Indexer
    {
        // <인덱서 정의>
        // this[]를 속성으로 정의하여 클래스의 인스턴스에 인덱스 방식으로 접근 허용
        public class IndexerArray
        {
            private int[] array = new int[10];

            public int this[int index]
            {
                get
                {
                    if (index < 0 || index >= array.Length)
                        throw new IndexOutOfRangeException();
                    else
                        return array[index];
                }
                set
                {
                    if (index < 0 || index >= array.Length)
                        throw new IndexOutOfRangeException();
                    else
                        array[index] = value;
                }
            }
        }

        void Main1()
        {
            IndexerArray array = new IndexerArray();

            // 인덱서를 통한 인덱스 접근
            array[5] = 20;      // this[] set 접근
            int i = array[5];   // this[] get 접근
        }


        // <인덱서 자료형>
        // 인덱서는 다른 자료형 사용도 가능
        // 열거형을 통해 인덱서를 사용하는 경우도 빈번
        public class Equipment
        {
            public enum Parts { Head, Body, Feet, Hand, SIZE }

            string[] equip = new string[(int)Parts.SIZE];

            public string this[Parts type]
            {
                get
                {
                    return equip[(int)type];
                }
                set
                {
                    equip[(int)type] = value;
                }
            }
        }
        void Main2()
        {
            Equipment equipment = new Equipment();

            equipment[Equipment.Parts.Head] = "낡은 헬멧";
            equipment[Equipment.Parts.Feet] = "가죽 장화";

            Console.WriteLine($"착용하고 있는 신발 : {equipment[Equipment.Parts.Feet]}");
        }
    }
    #endregion

    #region 8. Nullable
    class Nullable
    {
        public class NullClass
        {
            public int value;
            public void Func() { }
        }

        void Main()
        {
            // * Nullable 타입
            // * 참조형은 null 을 가질 수 있지만 값형식은 null을 가질 수 없음.
            // * 값 형식에 ? 를 통해 Nullable 타입을 지원해줌.

            bool? b = null;
            int? i = 20;


            // *** 유용하게 사용할 수 있음.
            // * Nullable 조건 연산자
            NullClass instance = null;  // instance 는 null
            Console.WriteLine(instance.value);  // error 
            Console.WriteLine(instance?.value);
            instance.Func();    // error
            instance?.Func();
        }
    }
    #endregion

    #region 9. Yield
    class Yield
    {
        // * yield
        // 반복기를 통해 데이터 집합을 하나씩 리턴할 때 사용
        // 1. 반환할 데이터의 양이 커서 한꺼번에 반환하는 것보다 분할해서 반환하는 것이 효율적인 경우
        // 2. 함수가 무제한의 데이터를 리턴할 경우
        // 3. 이전단계까지의 결과에서 다음까지만의 계산이 필요한 경우
        public IEnumerable<int> GetNumber()
        {
            yield return 10;
            yield return 20;
            yield return 30;
            yield return 40;
            yield return 50;
        }

        void Main1()
        {
            IEnumerator<int> iter = GetNumber().GetEnumerator();
            iter.Reset();
            while (iter.MoveNext())
            {
                int value = iter.Current;
            }

            // foreach 반복문은 IEnumerable 인터페이스가 포함된 데이터 집합을 반복하는 방식
            foreach (int num in GetNumber())
            {
                Console.WriteLine(num);     // output : 10, 20, 30, 40, 50
            }
        }

        // <yield 형식>
        // yield return	: 반복에서 다음을 제공
        IEnumerable<int> Repeater(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return i;
            }
        }

        // yield break	: 반복에서 끝을 제공
        IEnumerable<int> UntilPlus(IEnumerable<int> numbers)
        {
            foreach (int n in numbers)
            {
                if (n > 0)
                    yield return n;
                else
                    yield break;
            }
        }

        void Main2()
        {
            foreach (int num in Repeater(5))
                Console.WriteLine(num);     // output : 0, 1, 2, 3, 4

            foreach (int num in UntilPlus(new int[5] { 1, 3, 5, -1, 4 }))
                Console.WriteLine(num);     // output : 1, 3, 5
        }
    }
    #endregion

    #region 실습

    #region 1. 확장 메서드
    // int 자료형에 확장메서드로 추가기능 붙이기
    // int value = 3;
    // bool isEven = value.isEven();

    //public static class ExtentionMethodTest
    //{
    //    public static bool IsEven(this int num)
    //    {
    //        return num % 2 == 0;
    //    }
    //}
    //class Program
    //{
    //    public static void Main()
    //    {
    //        int num = -1;
    //        while (num == -1)
    //        {
    //            try
    //            {
    //                num = int.Parse(Console.ReadLine());
    //                break;
    //            }
    //            catch
    //            {
    //                num = -1;
    //                Console.WriteLine("다시 입력하세요.");
    //            }
    //        }
    //        Console.Write($"{num}은/는 ");

    //        if (num.IsEven())
    //            Console.WriteLine("짝수 입니다.");
    //        else
    //            Console.WriteLine("홀수 입니다.");
    //    }
    //}

    #endregion

    #region 2. 매개변수 ref
    // public static void Swap<일반화>(left, right) 함수
    // 어떤 자료형이 들어오더라도 두 매개변수의 원본을 교체하는 함수

    //class RefProperty
    //{
    //    public struct Position
    //    {
    //        public int x;
    //        public int y;
    //        public Position(int x, int y)
    //        {
    //            this.x = x;
    //            this.y = y;
    //        }
    //    }

    //    public static void Swap<T>(ref T left, ref T right)
    //    {
    //        T temp = left;
    //        left = right;
    //        right = temp;
    //    }

    //    static void Main(string[] argc)
    //    {
    //        int a = 5;
    //        int b = 3;

    //        float c = 5.5f;
    //        float d = 8.8f;

    //        Position e = new Position(1, 2);
    //        Position f = new Position(5, 6);

    //        Console.WriteLine("before Swap");
    //        Console.WriteLine($"a = {a}  | b = {b}              (int)");
    //        Console.WriteLine($"c = {c}  | d = {d}          (float)");
    //        Console.WriteLine($"e = ({e.x},{e.y})  | f = ({f.x},{f.y})      (struct)");

    //        Swap<int>(ref a, ref b);
    //        Swap<float>(ref c, ref d);
    //        Swap<Position>(ref e, ref f);

    //        Console.WriteLine("\n---------------------------------------\n");
    //        Console.WriteLine("after Swap");
    //        Console.WriteLine($"a = {a}  | b = {b}              (int)");
    //        Console.WriteLine($"c = {c}  | d = {d}          (float)");
    //        Console.WriteLine($"e = ({e.x},{e.y})  | f = ({f.x},{f.y})      (struct)");

    //    }
    //}

    #endregion

    #region 3. Property 속성
    // public class Player 를 만들고
    // 체력이라고 하는 변수를 외부에서 읽을 수는 있지만, 변경할 수 없도록
    // 체력관련 프로퍼티(Hp) 구현

    //public class Player
    //{
    //    public const int MAX_HP = 100;

    //    private int ownHp;
    //    public int OwnHp
    //    {
    //        get { return ownHp; }
    //        private set
    //        {
    //            if (value <= MAX_HP && value >= 0)
    //            {
    //                ownHp = value;
    //            }
    //            else
    //            {
    //                ownHp = value < 0 ? 0 : 100;
    //            }
    //        }
    //    }
    //    public Player()
    //    {
    //        ownHp = MAX_HP;
    //    }
    //}
    //public class Program
    //{
    //    static void Main(string[] argc)
    //    {
    //        Player player = new Player();
    //        // player.OwnHp = 100;              // 수정 불가
    //        Console.WriteLine(player.OwnHp);    // 읽기 가능
    //    }
    //}

    #endregion

    #region A++. Property 이벤트
    // Player player = new Player();
    // UI ui = new UI();
    // 
    // player.Hp = 20;
    // player.Hp = 30;
    // player.Hp = 50;
    //                  // ui의 체력 수치도 같이 갱신되도록 짜보자
    public class Player
    {
        public const int MAX_HP = 100;

        private int ownHp;
        public int OwnHp
        {
            get { return ownHp; }
            private set
            {
                if (value <= MAX_HP && value >= 0)
                {
                    ownHp = value;
                }
                else
                {
                    ownHp = value < 0 ? 0 : 100;
                }
                if (OnDealWithHp != null)   // 이벤트 실행
                    OnDealWithHp(ownHp);

            }
        }

        public event Action<int> OnDealWithHp;

        public Player()
        {
            ownHp = MAX_HP;
        }

        public void Healing(int value)
        {
            OwnHp += value;
        }
        public void TakeDamage(int value)
        {
            OwnHp -= value;
        }
    }

    public class UI
    {
        private StringBuilder hpString;
        public StringBuilder HpString { get { return hpString; } }

        public UI()
        {
            hpString = new StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                hpString.Append('■');
            }
        }
        public void PlayerHpUpdate(int hp)
        {
            hpString.Clear();
            int value = hp / 10;
            for (int i = 0; i < 10; i++)
            {
                if (i <= value - 1)
                {
                    hpString.Append('■');
                }
                else
                {
                    hpString.Append('□');
                }
            }
        }
    }
    public class GameSystem
    {
        private UI ui;
        private Player player;
        private bool isRunning;

        public event Action OnRendering;
        public GameSystem()
        {
            player = new Player();
            ui = new UI();

            player.OnDealWithHp += ui.PlayerHpUpdate;
            isRunning = true;
            OnRendering += OptionRendering;
            OnRendering += UIRendering;
        }
        public void OptionRendering()
        {
            Console.WriteLine("<  1:HP회복  |  2:HP소모  >");
        }
        public void UIRendering()
        {
            Console.Write("HP : ");
            for (int i = 0; i < ui.HpString.Length; i++)
            {
                if (ui.HpString[i] == '■')
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.Write(ui.HpString[i]);
                Console.ResetColor();
            }
            Console.WriteLine();
            Console.Write("현재 체력 : ");
            Console.WriteLine(player.OwnHp);
        }
        public void InputKey()
        {
            ConsoleKey inputKey = Console.ReadKey().Key;
            switch (inputKey)
            {
                case ConsoleKey.D1:
                    player.Healing(10);
                    break;
                case ConsoleKey.D2:
                    player.TakeDamage(10);
                    break;
                default:
                    isRunning = false;
                    break;
            }
        }

        public void LoopGame()
        {
            while (isRunning)
            {
                OnRendering();
                InputKey();
                Console.Clear();
            }
        }
    }
    class Program
    {
        static void Main(string[] argc)
        {
            GameSystem gameSystem = new GameSystem();
            gameSystem.LoopGame();
        }
    }



    #endregion

    #endregion

}