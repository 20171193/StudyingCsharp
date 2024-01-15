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
            #region 일반화

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

            #region 일반화 자료형 제약
            // where 구문을 사용해 
            
            #endregion
        }
    }
}