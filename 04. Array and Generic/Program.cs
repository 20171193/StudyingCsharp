/********************************************************************************************
 
 * Array (배열)
  1.1차원 배열의 선언 및 초기화
    //int[] arr = new int[5];
    //int[] arr = new int[5] { 1, 2, 3, 4, 5 };
    //int[] arr = { 1, 2, 3, 4, 5 };
    //int[] arr = new int[] { 1, 2, 3, 4, 5 };

  2.다차원 배열의 선언 및 초기화
    //int[,] arr = new int[행,열] => 2차원 배열
    //int[,,] """ => 3차원 배열            

 * System.Array : 배열에 대한 다양한 기능 제공.
  1.속성
     - Length : 배열 총 요소의 개수를 반환
        ex) Array.Length
     - Rank : 배열의 차원을 반환
        ex) Array.Rank
  2.기능
     - Sort() : 배열을 정렬

 *********************************************************************************************

 * 일반화 (Generic)
 * 
 * 클래스 또는 함수가 코드에 의해 선언되고 인스턴스화될 때까지 형식의 사양을 연기하는 디자인
 * 기능을 구현한 뒤 자료형을 사용 당시에 지정해서 사용
 
 ********************************************************************************************/


namespace _04._Array_and_Generic
{
    internal class Program
    {
        class Generic
        {
            #region 박싱/언박싱
            class Boxing
            {
                void Main()
                {
                    int number = 7;
                    object obj = number;        // int 타입의 number 변수 박싱.

                    int number2 = (int)obj;     // 언박싱하여 대입.
                    // int number3 = obj;       // error : 타입을 명시해주어야함

                    object[] objArr = new object[10];   // object형 배열도 생성 가능.
                    objArr[0] = 5;     //    int형 박싱
                    objArr[3] = '5';   //   char형 박싱   
                    objArr[1] = "5번"; // string형 박싱
                    objArr[2] = 5.0f;  //  float형 박싱
                }
            }
            #endregion

            #region 배열 일반화

            // 일반화를 활용한 배열의 복사
            static void ArrayCopy<T>(T[] arr, T[] temp)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    temp[i] = arr[i];
                }
            }

            static void Main(string[] argc)
            {
                int[] a = new int[5] { 1, 2, 3, 4, 5 };
                int[] b = new int[5];

                float[] c = new float[5] { 1.2f, 2.3f, 3.4f, 4.5f, 5.6f };
                float[] d = new float[5];

                string[] e = new string[5] { "짱구", "유리", "철수", "맹구", "훈이" };
                string[] f = new string[5];

                Console.WriteLine("befor copy");
                Console.Write("int b:");
                for (int i = 0; i < 5; i++)
                {
                    Console.Write($"{b[i]} ");
                }
                Console.WriteLine();
                Console.Write("float d:");
                for (int i = 0; i < 5; i++)
                {
                    Console.Write($"{d[i]} ");
                }
                Console.WriteLine();
                Console.Write("string f:");
                for (int i = 0; i < 5; i++)
                {
                    Console.Write($"{f[i]} ");
                }
                Console.WriteLine();

                ArrayCopy<int>(a, b);
                ArrayCopy<float>(c, d);
                ArrayCopy<string>(e, f);

                Console.WriteLine("after copy");
                Console.Write("int b:");
                for (int i = 0; i < 5; i++)
                {
                    Console.Write($"{b[i]} ");
                }
                Console.WriteLine();
                Console.Write("float d:");
                for (int i = 0; i < 5; i++)
                {
                    Console.Write($"{d[i]} ");
                }
                Console.WriteLine();
                Console.Write("string f:");
                for (int i = 0; i < 5; i++)
                {
                    Console.Write($"{f[i]} ");
                }
                Console.WriteLine();
            }
            #endregion

            #region 일반화 클래스
            // <일반화 클래스>
            // 클래스에 필요한 자료형을 일반화하여 구현
            // 이후 클래스 인스턴스를 생성할 때 자료형을 지정하여 사용
            public class SafeArray<T>
            {
                T[] array;

                public SafeArray(int size)
                {
                    array = new T[size];
                }

                public void Set(int index, T value)
                {
                    if (index < 0 || index >= array.Length)
                        return;

                    array[index] = value;
                }

                public T Get(int index)
                {
                    if (index < 0 || index >= array.Length)
                        return default(T);      // default : T 자료형의 기본값

                    return array[index];
                }
            }
            #endregion

            #region 일반화 자료형 제약
            // where 구문을 사용해 
            // <일반화 자료형 제약>
            // 일반화 자료형을 선언할 때 제약조건을 선언하여, 사용 당시 쓸 수 있는 자료형을 제한
            class StructT<T> where T : struct { }           // T는 구조체만 사용 가능
            class ClassT<T> where T : class { }             // T는 클래스만 사용 가능
            class NewT<T> where T : new() { }               // T는 매개변수 없는 생성자가 있는 자료형만 사용 가능

            class ParentT<T> where T : Parent { }           // T는 Parent 파생클래스만 사용 가능
            class InterfaceT<T> where T : IComparable { }   // T는 인터페이스를 포함한 자료형만 사용 가능

            class Parent { }
            class Child : Parent { }

            void Main2()
            {
                StructT<int> structT = new StructT<int>();          // int는 구조체이므로 struct 제약조건이 있는 일반화 자료형에 사용 가능
                                                                    // ClassT<int> classT = new ClassT<int>();          // error : int는 구조체이므로 class 제약조건이 있는 일반화 자료형에 사용 불가
                NewT<int> newT = new NewT<int>();                   // int는 new int() 생성자가 있으므로 사용 가능

                ParentT<Parent> parentT = new ParentT<Parent>();    // Parent는 Parent 파생클래스이므로 사용 가능
                ParentT<Child> childT = new ParentT<Child>();       // Child는 Parent 파생클래스이므로 사용 가능
                InterfaceT<int> interT = new InterfaceT<int>();     // int는 IComparable 인터페이스를 포함하므로 사용 가능
            }


            // <일반화 제약 용도>
            // 일반화 자료형에 제약조건이 있다면 포함가능한 자료형의 기능을 사용할 수 있음
            public class BaseClass
            {
                public void Start()
                {
                    Console.WriteLine("Start");
                }
            }

            public void Main3<T>(T param) where T : BaseClass
            {
                param.Start();      // 일반화 자료형의 제약조건이 BaseClass 클래스이므로, BaseClass의 기능을 사용 가능 
            }

            #endregion
        }
    }
}